using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HpAndSanity : MonoBehaviour
{
    public float hp;
    public float currenthp;
    public float sanity;
    public float currentsanity;
    public float SanityResistance;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI sanityText;
    // Start is called before the first frame update
    void Start()
    {
        currenthp = hp;
        currentsanity = sanity;
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthText();
        UpdateSanityText();
    }
    public void TakeDamage(float damage)
    {
        if(currenthp > 0)
        {
            currenthp -= damage * RatioDamageAndSanity(currentsanity); 
        }     
        else
        {
            this.gameObject.SetActive(false);
        }
    }
    public void TakeSanity(float sanitydamage)
    {
        currentsanity -= sanitydamage; 
        if(currentsanity <= 0) 
        {
           currentsanity = 1;
        }
    }
    public void HealSanity(float sanitydamage)
    {
        currentsanity += sanitydamage; 
        if(currentsanity > sanity)
        {
            currentsanity = sanity;
        }
    }
    void UpdateHealthText()
    {
        healthText.text = "HP: " + currenthp.ToString("F2");
    }
    void UpdateSanityText()
    {
        sanityText.text = "Sanity: " + currentsanity.ToString("F2");
    }
    public float RatioDamageAndSanity(float sanity)
    {
        if(sanity >= 20)
            return 1.0f;
        else if(sanity > 1 && sanity < 20)
            return 1.1f;
        else 
            return 1000.0f;
    }
}
