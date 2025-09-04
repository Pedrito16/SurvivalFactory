using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepSounds;

    public float walkStepRate = 0.65f;
    public float runStepRate = 0.35f;

    private float stepTimer;
    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        stepTimer = walkStepRate;
    }

    void Update()
    {
        if (controller.isGrounded && controller.velocity.magnitude > 0.1f)
        {
            bool isRunning = Input.GetKey(KeyCode.LeftShift);

            float currentRate = isRunning ? runStepRate : walkStepRate;

            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = currentRate;
            }
        }
        else
        {
            stepTimer = 0f;
        }
    }

    void PlayFootstep()
    {
        if (footstepSounds.Length > 0)
        {
            audioSource.pitch = Random.Range(0.40f, 0.55f);
            AudioClip clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
            audioSource.PlayOneShot(clip);
        }
    }
}
