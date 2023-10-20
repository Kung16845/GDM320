using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private Dictionary<string, AudioClip> soundLibrary = new Dictionary<string, AudioClip>();
    
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

    public void PlaySound(string soundName)
    {
        if (soundLibrary.ContainsKey(soundName))
        {
            AudioSource.PlayClipAtPoint(soundLibrary[soundName], transform.position);
        }
        else
        {
            Debug.LogWarning("Sound not found: " + soundName);
        }
    }
    public bool HasSound(string soundName)
    {
        return soundLibrary.ContainsKey(soundName);
    }

    public AudioClip GetSound(string soundName)
    {
        if (soundLibrary.ContainsKey(soundName))
        {
            return soundLibrary[soundName];
        }
        else
        {
            Debug.LogWarning("Sound not found: " + soundName);
            return null;
        }
    }
}
