using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentalSoundManager : MonoBehaviour
{
    public AudioClip mainSoundClip; // The main sound clip that continuously plays
    public AudioClip[] secondarySoundClips; // Array of secondary sound clips
    public float secondarySoundDelay = 10.0f; // Delay before playing the secondary sound
    public bool playSecondarySoundRepeatedly = true; // Whether to play the secondary sound repeatedly
    public float minSecondarySoundInterval = 60.0f; // Minimum interval for repeated secondary sound plays
    public float maxSecondarySoundInterval = 120.0f; // Maximum interval for repeated secondary sound plays

    private AudioSource mainSoundSource; // AudioSource for the main sound
    private AudioSource secondarySoundSource; // AudioSource for the secondary sound
    private float timeSinceLastSecondarySound = 0.0f;

    void Start()
    {
        // Create the main sound source
        mainSoundSource = gameObject.AddComponent<AudioSource>();
        mainSoundSource.clip = mainSoundClip;
        mainSoundSource.loop = true;
        mainSoundSource.Play();

        // Create the secondary sound source
        secondarySoundSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (playSecondarySoundRepeatedly)
        {
            // Check if it's time to play the secondary sound repeatedly
            timeSinceLastSecondarySound += Time.deltaTime;
            if (timeSinceLastSecondarySound >= Random.Range(minSecondarySoundInterval, maxSecondarySoundInterval))
            {
                PlaySecondarySound();
            }
        }
    }

    public void PlaySecondarySound()
    {
        if (secondarySoundSource != null && secondarySoundClips.Length > 0)
        {
            if (secondarySoundSource.isPlaying)
            {
                secondarySoundSource.Stop();
            }

            int randomIndex = Random.Range(0, secondarySoundClips.Length);
            secondarySoundSource.clip = secondarySoundClips[randomIndex];
            secondarySoundSource.Play();
            timeSinceLastSecondarySound = 0.0f; // Reset the timer
        }
    }
}
