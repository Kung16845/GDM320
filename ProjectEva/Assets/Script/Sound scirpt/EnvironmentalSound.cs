using UnityEngine;

public class EnvironmentalSound : MonoBehaviour
{
    public SoundManager soundManager; // Reference to the SoundManager script.
    public string environmentalSoundName = "Ambient"; // Name of the environmental sound in the SoundManager.
    private AudioSource audioSource;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();

        // Find and assign the SoundManager script in the scene.
        soundManager = FindObjectOfType<SoundManager>();

        if (soundManager == null)
        {
            Debug.LogError("SoundManager not found in the scene. Make sure to add it to a GameObject.");
        }
        else
        {
            // Check if the environmental sound exists in the SoundManager.
            if (soundManager.HasSound(environmentalSoundName))
            {
                // Get the AudioClip for the environmental sound.
                AudioClip environmentalSoundClip = soundManager.GetSound(environmentalSoundName);

                // Set the audio source properties.
                audioSource.clip = environmentalSoundClip;
                audioSource.loop = true;

                // Play the environmental sound.
                audioSource.Play();
            }
            else
            {
                Debug.LogError("The environmental sound " + environmentalSoundName + " is not found in the SoundManager.");
            }
        }
    }
}
