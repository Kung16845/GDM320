using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public float damageInterval; // เวลาต่อครั้งที่จะทำดาเมจ (วินาที)
    public float damageAmount; // จำนวนดาเมจที่จะทำต่อครั้ง
    private bool isTakingDamage = false;
    public HpAndSanity hpAndSanity = new HpAndSanity();

    public GameManager gameManager = new GameManager();

    void Start ()
    {
        Debug.Log(hpAndSanity.SanityResistance);
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            if(other.GetComponent<OnOffLight>().checkLight == false && !isTakingDamage)
            {
                isTakingDamage= true;
                StartCoroutine(DamageRoutine(other.gameObject)); 
            }
            else if(other.GetComponent<OnOffLight>().checkLight == true && isTakingDamage) 
            {
                isTakingDamage = false; 
                StopCoroutine(DamageRoutine(other.gameObject));
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other) 
    {
        StopCoroutine(DamageRoutine(other.gameObject));
    }
    private System.Collections.IEnumerator DamageRoutine(GameObject player)
    {
        while (isTakingDamage)
        {
            player.GetComponent<HpAndSanity>().TakeSanity(damageAmount * gameManager.gamedificulty - hpAndSanity.SanityResistance); 
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
