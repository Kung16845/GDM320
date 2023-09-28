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

    void Start()
    {
        currentsanity = maxsanity;
    }

    void Update()
    {
        UpdateSanityText();
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