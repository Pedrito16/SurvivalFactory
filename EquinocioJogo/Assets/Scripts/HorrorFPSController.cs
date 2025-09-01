using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class HorrorFPSController : MonoBehaviour
{
    public CharacterController controller;
    public Transform cameraPivot;
    public float walkSpeed = 2.4f;
    public float runSpeed = 4.2f;
    public float acceleration = 10f;
    public float gravity = -20f;
    public float jumpForce = 5f;
    public bool allowRun = true;

    public float headBobAmplitudeWalk = 0.025f;
    public float headBobFrequencyWalk = 9f;
    public float headBobAmplitudeRun = 0.04f;
    public float headBobFrequencyRun = 13f;

    public float idleBreathAmplitude = 0.012f;
    public float idleBreathFrequency = 1.1f;

    public float mouseSensitivity = 120f;
    public float minPitch = -80f;
    public float maxPitch = 80f;

    Vector3 velocity;
    float targetSpeed;
    float currentSpeed;
    float yaw;
    float pitch;
    float bobTimer;
    Vector3 camLocalStart;

    void Awake()
    {
        if (controller == null) controller = GetComponent<CharacterController>();
        if (cameraPivot == null) cameraPivot = Camera.main.transform;
        camLocalStart = cameraPivot.localPosition;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Look();
        Move();
        BobAndBreath();
    }

    void Look()
    {
        float mx = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        float my = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
        yaw += mx;
        pitch -= my;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        transform.rotation = Quaternion.Euler(0f, yaw, 0f);
        cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    void Move()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        input = Vector2.ClampMagnitude(input, 1f);
        bool isRunning = allowRun && Input.GetKey(KeyCode.LeftShift) && input.sqrMagnitude > 0.1f;
        targetSpeed = (isRunning ? runSpeed : walkSpeed) * input.magnitude;
        currentSpeed = Mathf.MoveTowards(currentSpeed, targetSpeed, acceleration * Time.deltaTime);

        Vector3 moveDir = (transform.right * input.x + transform.forward * input.y).normalized;
        Vector3 horizontal = moveDir * currentSpeed;

        if (controller.isGrounded && velocity.y < 0f) velocity.y = -2f;
        if (controller.isGrounded && Input.GetButtonDown("Jump")) velocity.y = jumpForce;
        velocity.y += gravity * Time.deltaTime;

        controller.Move((horizontal + new Vector3(0f, velocity.y, 0f)) * Time.deltaTime);
    }

    void BobAndBreath()
    {
        Vector3 camPos = camLocalStart;
        float speed01 = Mathf.InverseLerp(0f, runSpeed, currentSpeed);
        bool moving = currentSpeed > 0.05f;

        if (moving && controller.isGrounded)
        {
            bobTimer += Time.deltaTime * Mathf.Lerp(headBobFrequencyWalk, headBobFrequencyRun, speed01);
            float amp = Mathf.Lerp(headBobAmplitudeWalk, headBobAmplitudeRun, speed01);
            camPos.y += Mathf.Sin(bobTimer) * amp;
            camPos.x += Mathf.Cos(bobTimer * 0.5f) * amp * 0.5f;
        }
        else
        {
            bobTimer = Mathf.Lerp(bobTimer, 0f, 5f * Time.deltaTime);
            float t = Time.time * idleBreathFrequency;
            camPos.y += Mathf.Sin(t) * idleBreathAmplitude;
        }

        cameraPivot.localPosition = Vector3.Lerp(cameraPivot.localPosition, camPos, 10f * Time.deltaTime);
    }
}
