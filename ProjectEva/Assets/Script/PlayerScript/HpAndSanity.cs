using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class HpAndSanity : MonoBehaviour
{
    public float maxhp;
    public float currenthp;
    public float maxsanity;
    public float currentsanity;
    public float SanityResistance;
    public GameObject player;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI sanityText;
    // Start is called before the first frame update
    void Start()
    {
        currenthp = maxhp;
        currentsanity = maxsanity;
        UpdateHealthAndSanityTextUI();
    }

    // Update is called once per frame
    void Update()
    {   
        
    }
    public void TakeDamage(float damage)
    {  
        currenthp -= damage * RatioDamageAndSanity(currentsanity);
        UpdateHealthAndSanityTextUI();

        if (currenthp < 0)
        {
            currenthp = 0;
            UpdateHealthAndSanityTextUI();
            player.SetActive(false);
        }
    }
    public void TakeSanity(float sanitydamage)
    {
        currentsanity -= sanitydamage;
        UpdateHealthAndSanityTextUI();
        if (currentsanity <= 0)
        {   
            currentsanity = 1;
            UpdateHealthAndSanityTextUI();
        }
    }

    public void HealSanity(float sanitydamage)
    {
        currentsanity += sanitydamage;
        if (currentsanity > maxsanity)
        {
            currentsanity = maxsanity;
        }
    }
    public void HealHp(float HealHp)
    {
        currenthp += HealHp;
        if (currenthp > maxhp)
        {
            currenthp = maxhp;
        }
    }

    void UpdateHealthAndSanityTextUI()
    {
        healthText.text = "HP: " + currenthp.ToString("F2");
        sanityText.text = "Sanity: " + currentsanity.ToString("F2");
    }
    public float RatioDamageAndSanity(float sanity)
    {
        if (sanity >= 20)
            return 1.0f;
        else if (sanity > 1 && sanity < 20)
            return 1.1f;
        else
            return 1000.0f;
    }

}
