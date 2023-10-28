using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSound : MonoBehaviour
{
    public AudioSource footstepAudioSource; // Reference to the audio source for footstep sound

    private void Start()
    {
        // Get the AudioSource component from the same GameObject
        footstepAudioSource = GetComponent<AudioSource>();
    }

    public void PlayFootstepSound()
    {
        // Play the footstep sound
        if (!footstepAudioSource.isPlaying)
        {
            footstepAudioSource.Play();
        }
    }

    public void StopFootstepSound()
    {
        // Stop the footstep sound
        footstepAudioSource.Stop();
    }
}
