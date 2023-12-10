using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Sanity : MonoBehaviour
{
    public float maxsanity;
    public float currentsanity;
    public float SanityResistance;
    public TextMeshProUGUI sanityText;
    public CanvasGroup canvasGroup;
    public float lowSanityThreshold = 30f;

    void Start()
    {
        currentsanity = maxsanity;
    }

    void Update()
    {
        UpdateSanityText();
        if (currentsanity < lowSanityThreshold)
        {
            // Decrease the alpha of the CanvasGroup.
            canvasGroup.alpha = 0.1f; // You can adjust the alpha value as needed.
        }
        else
        {
            // Reset the alpha to full when HP is above the threshold.
            canvasGroup.alpha = 0f;
        }
    }

    public void TakeSanity(float sanityDamage)
    {
        currentsanity -= sanityDamage;
        currentsanity = Mathf.Clamp(currentsanity, 1, maxsanity);
    }

    public void HealSanity(float sanityAmount)
    {
        currentsanity += sanityAmount;
        currentsanity = Mathf.Clamp(currentsanity, 1, maxsanity);
    }

    void UpdateSanityText()
    {
        sanityText.text = "Sanity: " + currentsanity.ToString("F2");
    }
}