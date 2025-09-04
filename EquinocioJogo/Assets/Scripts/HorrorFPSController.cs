using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class HorrorPlayerControllerJuicy : MonoBehaviour
{
    public CharacterController controller;
    public Transform cameraPivot;
    public float walkSpeed = 2.5f;
    public float runSpeed = 4.5f;
    public float acceleration = 10f;
    public float gravity = -20f;
    public float jumpForce = 5f;
    public bool allowRun = true;

    [Header("Head Bob & Breath")]
    public float walkBobAmp = 0.035f;
    public float walkBobFreq = 8f;
    public float runBobAmp = 0.07f;
    public float runBobFreq = 13f;

    public float idleBreathAmp = 0.02f;
    public float idleBreathFreq = 1.1f;
    public float idleSwayAmp = 0.01f;
    public float idleSwayFreq = 0.6f;

    [Header("Extra Juice")]
    public float swayAmount = 0.02f;
    public float swaySpeed = 4f;
    public float sideLeanAmp = 4f; // inclinação ao andar pros lados
    public float stopBounceAmp = 0.06f;
    public float stopBounceDecay = 6f;

    [Header("Mouse Look")]
    public float sensitivity = 120f;
    public float minPitch = -80f;
    public float maxPitch = 80f;

    Vector3 velocity;
    float targetSpeed;
    float currentSpeed;
    float yaw;
    float pitch;
    float bobTimer;
    Vector3 camStartPos;
    float stopBounce;
    float leanAngle;

    public bool canMove = true;
    public static HorrorPlayerControllerJuicy instance;
    void Awake()
    {
        instance = this;
        if (controller == null) controller = GetComponent<CharacterController>();
        if (cameraPivot == null) cameraPivot = Camera.main.transform;
        camStartPos = cameraPivot.localPosition;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!canMove) return;
        Look();
        Move();
        CameraEffects();
    }

    void Look()
    {
        float mx = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float my = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;
        yaw += mx;
        pitch -= my;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
        cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    void Move()
    {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        input = Vector2.ClampMagnitude(input, 1f);

        bool running = allowRun && Input.GetKey(KeyCode.LeftShift) && input.sqrMagnitude > 0.1f;
        targetSpeed = (running ? runSpeed : walkSpeed) * input.magnitude;
        float prevSpeed = currentSpeed;
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        Vector3 dir = (transform.right * input.x + transform.forward * input.y).normalized;
        Vector3 horiz = dir * currentSpeed;

        if (controller.isGrounded && velocity.y < 0f) velocity.y = -2f;
        if (controller.isGrounded && Input.GetButtonDown("Jump")) velocity.y = jumpForce;
        velocity.y += gravity * Time.deltaTime;

        controller.Move((horiz + new Vector3(0f, velocity.y, 0f)) * Time.deltaTime);

        // bounce quando para
        if (prevSpeed > 0.1f && currentSpeed < 0.05f)
            stopBounce = stopBounceAmp;

        // lean lateral (ao andar pros lados)
        float targetLean = -input.x * sideLeanAmp;
        leanAngle = Mathf.Lerp(leanAngle, targetLean, Time.deltaTime * 5f);
    }

    void CameraEffects()
    {
        Vector3 camPos = camStartPos;
        float spdPercent = Mathf.InverseLerp(0f, runSpeed, currentSpeed);

        if (currentSpeed > 0.1f)
        {
            bobTimer += Time.deltaTime * Mathf.Lerp(walkBobFreq, runBobFreq, spdPercent);
            float amp = Mathf.Lerp(walkBobAmp, runBobAmp, spdPercent);

            camPos.y += Mathf.Sin(bobTimer) * amp;
            camPos.x += Mathf.Cos(bobTimer * 0.5f) * amp * 0.5f;
        }
        else
        {
            // idle vivo: respiração + sway lateral lento
            bobTimer += Time.deltaTime * idleBreathFreq;
            camPos.y += Mathf.Sin(bobTimer) * idleBreathAmp;
            camPos.x += Mathf.Sin(Time.time * idleSwayFreq) * idleSwayAmp;
        }

        if (stopBounce > 0f)
        {
            camPos.y += Mathf.Sin(Time.time * 20f) * stopBounce;
            stopBounce = Mathf.MoveTowards(stopBounce, 0f, stopBounceDecay * Time.deltaTime);
        }

        float swayX = -Input.GetAxis("Mouse X") * swayAmount;
        float swayY = -Input.GetAxis("Mouse Y") * swayAmount * 0.5f;
        Vector3 sway = new Vector3(swayX, swayY, 0f);

        cameraPivot.localPosition = Vector3.Lerp(cameraPivot.localPosition, camPos + sway, swaySpeed * Time.deltaTime);

        // aplicar lean na rotação (roll da câmera)
        cameraPivot.localRotation *= Quaternion.Euler(0f, 0f, leanAngle);
    }
}