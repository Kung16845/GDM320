using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WalllampDuration : MonoBehaviour
{
    public bool lightup;
    public Light2D wallLampLight;
    public DamageArea damageArea;
    public float maxIntensity = 2.37f; // Adjustable maximum intensity
    public float duration = 240.0f; // Duration in seconds (4 minutes)

    public void Start()
    {
        lightup = false;
    }
    void Update()
    {
        if (lightup)
        {
            // If lightup is true, increase the intensity back to the maximum
            wallLampLight.intensity += Time.deltaTime; // Adjust this value based on how quickly you want the light to increase
            wallLampLight.intensity = Mathf.Min(maxIntensity, wallLampLight.intensity); // Clamp the intensity to the maximum

            // Reduce the duration
            duration -= Time.deltaTime;

            // If the duration is less than or equal to 0, turn off the light
            if (duration <= 0.0f)
            {
                lightup = false;
                duration = 0.0f; // Ensure that the duration doesn't go below 0
            }
        }
        else
        {
            // If lightup is false, decrease the intensity of the light to 0
            wallLampLight.intensity -= Time.deltaTime; // Adjust this value based on how quickly you want the light to fade
            wallLampLight.intensity = Mathf.Max(0f, wallLampLight.intensity); // Clamp the intensity to be non-negative
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && lightup)
        {
            damageArea.cantakedamage = false;
            damageArea.damageAmount = 0;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && lightup)
        {
            damageArea.cantakedamage = true;
            damageArea.damageAmount = 0.33f;
        }
    }
}
