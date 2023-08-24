using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HpAndSanity : MonoBehaviour
{
    public float hp;
    public float currunthp;
    
    public float sanity;
    public float curruntsanity;

    public float SanityResistance;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI sanityText;
    // Start is called before the first frame update
    void Start()
    {
        currunthp = hp;
        curruntsanity = sanity;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHealthText();
        UpdateSanityText();
    }
    public void TakeDamage(float damage)
    {
        if(currunthp > 0)
        {
            currunthp -= damage; 
        }     
        else
        {
            this.gameObject.SetActive(false);
        }
    }
    public void TakeSanity(float sanitydamage)
    {
        curruntsanity -= sanitydamage; 
        if(curruntsanity <= 0) 
        {
           curruntsanity = 1;
        }
    }
    public void HealSanity(float sanitydamage)
    {
        curruntsanity += sanitydamage; 
        if(curruntsanity > sanity)
        {
            curruntsanity = sanity;
        }
    }
    void UpdateHealthText()
    {
        healthText.text = "HP: " + currunthp.ToString();
    }
    void UpdateSanityText()
    {
        sanityText.text = "Sanity: " + curruntsanity.ToString();
    }
}
