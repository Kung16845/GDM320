using UnityEngine;
using UnityEngine.UI;

public class Reticle : MonoBehaviour
{
    public float maxReticleSize = 50f; // Maximum reticle size when fully accurate.
    public float minReticleSize = 10f; // Minimum reticle size when not accurate.
    
    private Image reticleImage;
    private float currentAccuracy = 1.0f;

    private void Start()
    {
        reticleImage = GetComponent<Image>();
    }

    public void Update()
    {
        // Assuming you're controlling accuracy in your gun script.
        // Adjust the currentAccuracy based on your gun script's logic.
        // For example, if the player holds the right mouse button, currentAccuracy = maxAccuracy.
        // If not, currentAccuracy = minAccuracy.

        // Set the reticle size based on currentAccuracy.
        float reticleSize = Mathf.Lerp(minReticleSize, maxReticleSize, currentAccuracy);
        reticleImage.rectTransform.sizeDelta = new Vector2(reticleSize, reticleSize);
    }
}
