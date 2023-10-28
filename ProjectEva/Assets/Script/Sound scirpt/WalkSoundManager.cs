using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSoundManager : MonoBehaviour
{
    private Dictionary<string, AudioClip> soundLibrary = new Dictionary<string, AudioClip>();
    private Dictionary<string, GameObject> playingSounds = new Dictionary<string, GameObject>(); // Track playing sounds by name

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
            // Stop the current sound (if any) before playing a new one with the same name
            if (playingSounds.ContainsKey(soundName) && playingSounds[soundName] != null)
            {
                Destroy(playingSounds[soundName]);
            }

            // Create a new game object for the sound as a child of the specified parent
            GameObject soundObject = new GameObject("SoundObject_" + soundName);
            soundObject.transform.SetParent(parent);

            // Add an AudioSource component to the object and play the sound
            AudioSource audioSource = soundObject.AddComponent<AudioSource>();
            audioSource.clip = soundLibrary[soundName];
            audioSource.Play();

            // Track the playing sound by name
            playingSounds[soundName] = soundObject;

            return soundObject;
        }
        else
        {
            Debug.LogWarning("Sound not found: " + soundName);
            return null;
        }
    }

    public void StopSound(string soundName)
    {
        if (playingSounds.ContainsKey(soundName) && playingSounds[soundName] != null)
        {
            Destroy(playingSounds[soundName]);
            playingSounds.Remove(soundName);
        }
    }
}
