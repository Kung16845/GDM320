using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSoundManager : MonoBehaviour
{
     private Dictionary<string, AudioClip> soundLibrary = new Dictionary<string, AudioClip>();
    private GameObject currentSoundObject; // Track the object for the currently playing sound

    [System.Serializable] 
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }

    public List<Sound> sounds;

    void Awake()
    {
        // Populate the sound library
        foreach (Sound sound in sounds)
        {
            soundLibrary[sound.name] = sound.clip;
        }
    }

    public GameObject PlaySound(string soundName, Transform parent)
    {
    if (soundLibrary.ContainsKey(soundName))
    {
        // Stop the current sound (if any) before playing a new one
        if (currentSoundObject != null)
        {
            Destroy(currentSoundObject);
        }

        // Create a new game object for the sound as a child of the specified parent
        currentSoundObject = new GameObject("SoundObject_" + soundName);
        currentSoundObject.transform.SetParent(parent);

        // Add an AudioSource component to the object and play the sound
        AudioSource audioSource = currentSoundObject.AddComponent<AudioSource>();
        audioSource.clip = soundLibrary[soundName];
        audioSource.Play();

        return currentSoundObject;
    }
    else
    {
        Debug.LogWarning("Sound not found: " + soundName);
        return null;
    }
    }


    public void StopSound()
    {
        if (currentSoundObject != null)
        {
            Destroy(currentSoundObject);
        }
    }
}