using UnityEngine;

public class SanityScaleController : MonoBehaviour
{
    // Reference to the HpAndSanity script.
    private Hp hp;
    private Sanity sanity;

    // Scaling factors for different attributes.
    private float speedScale = 1.0f;
    private float accuracyScale = 1.0f;
    private float damageScale = 1.0f;

    void Start()
    {
        // Find and store a reference to the HpAndSanity script.
        hp = FindObjectOfType<Hp>();
        sanity = FindObjectOfType<Sanity>();
    }

    void Update()
    {
        // Update scaling factors based on sanity.  
        UpdateScalingFactors();
    }

    void UpdateScalingFactors()
    {
        // Get the current sanity value from the HpAndSanity script.
        float currentSanity = sanity.currentsanity;

        // Calculate scaling factors based on sanity.
        speedScale = GetSpeedScale(currentSanity);
        accuracyScale = GetAccuracyScale(currentSanity);
        damageScale = GetDamageScale(currentSanity);
    }

    // Functions to get scaling factors based on sanity.
    private float GetSpeedScale(float sanity)
    {

        if (sanity > 50)
            return 1f;
        else if (sanity <= 50 && sanity > 20)
            return 1.05f;
        else if (sanity <= 20 || sanity > 1)
            return 1.1f;
        else if (sanity == 1)
            return 1.2f;
        else
            return 1f;
    }

    private float GetAccuracyScale(float sanity)
    {
        if (sanity >= 50 && sanity < 70)
        {
        return 0.9f;
        }
        else if (sanity < 50 && sanity >= 20)
        {
        return 0.8f;
        }
        else if (sanity < 20 && sanity > 1)
        {
        return 0.6f;
        }
        else if (sanity == 1) // Use '==' for equality comparison.
        {   
        return 0.6f;
        }
        else
        {
        return 1.0f;
        }

    }

    private float GetDamageScale(float sanity)
    {
        // Implement your logic here.
        // Example logic:
        if (sanity >= 20)
            return 1.0f;
        else if (sanity > 1 && sanity < 20)
            return 1.1f;
        else
            return 1000.0f;
    }

    // Functions to access scaling factors from other scripts.
    public float GetSpeedScale()
    {
        return speedScale;
    }

    public float GetAccuracyScale()
    {
        return accuracyScale;
    }

    public float GetDamageScale()
    {
        return damageScale;
    }
}
