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
        public float maxDistance = 10f;
        public float volume = 1.0f;
        public float spatialBlend = 1.0f; // Add a field for spatialBlend with a default value of 1.0f (3D)

        // Constructor to set default values
        public Sound(string name, AudioClip clip, float maxDistance = 10f, float volume = 1.0f, float spatialBlend = 1.0f)
        {
            this.name = name;
            this.clip = clip;
            this.maxDistance = maxDistance;
            this.volume = volume;
            this.spatialBlend = spatialBlend;
        }
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
            Transform soundTransform = soundObject.transform;
            soundTransform.SetParent(parent);

            // Add an AudioSource component to the object
            AudioSource audioSource = soundObject.AddComponent<AudioSource>();
            audioSource.clip = soundLibrary[soundName];
            audioSource.maxDistance = sounds.Find(s => s.name == soundName)?.maxDistance ?? 10f; // Set maxDistance
            audioSource.volume = sounds.Find(s => s.name == soundName)?.volume ?? 1.0f; // Set volume
            audioSource.spatialBlend = sounds.Find(s => s.name == soundName)?.spatialBlend ?? 1.0f; // Set spatialBlend

            // Play the sound
            audioSource.Play();

            // Track the playing sound by name
            playingSounds[soundName] = soundObject;

            // Update the sound's position and rotation based on the parent during each frame
            StartCoroutine(FollowParent(soundTransform, parent));

            return soundObject;
        }
        else
        {
            Debug.LogWarning("Sound not found: " + soundName);
            return null;
        }
    }
    private IEnumerator FollowParent(Transform soundTransform, Transform parent)
    {
        while (true)
        {
            // Update the position and rotation of the sound to match its parent
            soundTransform.position = parent.position;
            soundTransform.rotation = parent.rotation;

            yield return null;
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
