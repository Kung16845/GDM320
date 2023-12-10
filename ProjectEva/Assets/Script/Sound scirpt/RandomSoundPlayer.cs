using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSoundPlayer : MonoBehaviour
{
    public AudioClip[] sounds; // An array of audio clips to choose from
    public float minTime = 2.0f; // Minimum time between sound plays
    public float maxTime = 5.0f; // Maximum time between sound plays

    private AudioSource audioSource;
    private float nextSoundTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Set the initial time for playing the first sound
        nextSoundTime = Time.time + Random.Range(minTime, maxTime);
    }

    void Update()
    {
        // Check if it's time to play a sound
        if (Time.time >= nextSoundTime)
        {
            PlayRandomSound();
            // Set the next time to play a sound
            nextSoundTime = Time.time + Random.Range(minTime, maxTime);
        }
    }

    void PlayRandomSound()
    {
        if (sounds.Length == 0)
        {
            Debug.LogWarning("No audio clips assigned to the sounds array.");
            return;
        }

        // Choose a random sound from the array
        int randomIndex = Random.Range(0, sounds.Length);
        AudioClip randomSound = sounds[randomIndex];

        // Play the selected sound
        audioSource.PlayOneShot(randomSound);
    }
}
