using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class Hp : MonoBehaviour
{
    public float maxhp;
    public float currenthp;
    public GameObject player;
    public TextMeshProUGUI healthText;
    private SanityScaleController sanityScaleController;

    void Start()
    {
        currenthp = maxhp;
        sanityScaleController = FindObjectOfType<SanityScaleController>();
    }

    void Update()
    {
        UpdateHealthText();
    }

    public void TakeDamage(float damage)
    {
        if (currenthp > 0)
        {
            currenthp -= damage * sanityScaleController.GetDamageScale();
            currenthp = Mathf.Clamp(currenthp, 0, maxhp);
        }
        else
        {
            player.SetActive(false);
        }
    }

    public void HealHp(float healAmount)
    {
        currenthp += healAmount;
        currenthp = Mathf.Clamp(currenthp, 0, maxhp);
    }

    void UpdateHealthText()
    {
        healthText.text = "HP: " + currenthp.ToString("F2");
    }
}
