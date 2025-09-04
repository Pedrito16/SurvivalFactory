using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepSounds;
    public float stepRate = 0.5f;
    private float stepTimer;

    private CharacterController controller;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        stepTimer = stepRate;
    }

    void Update()
    {
        if (controller.isGrounded && controller.velocity.magnitude > 0.1f)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = stepRate;
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
            AudioClip clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
            audioSource.PlayOneShot(clip);
        }
    }
}
