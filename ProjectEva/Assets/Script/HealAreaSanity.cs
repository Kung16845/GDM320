using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAreaSanity : MonoBehaviour
{
    public float HealInterval ; // เวลาต่อครั้งที่จะทำฟื้นฟู (วินาที)
    public float HealAmount ; // จำนวนฟื้นฟูที่จะทำต่อครั้ง
    bool isStay = false;
    private void OnTriggerEnter2D(Collider2D player)
    {
        isStay = true;
        if (player.GetComponent<HpAndSanity>() != null) 
        {
            StartCoroutine(DamageRoutine(player.gameObject));
        } 
    }
    private void OnTriggerExit2D(Collider2D player)
    {
        isStay = false;
        if (player.GetComponent<HpAndSanity>() != null)
        {
            StopCoroutine(DamageRoutine(player.gameObject));
        }
    }
    private System.Collections.IEnumerator DamageRoutine(GameObject player)
    {
        while (isStay)
        {
            player.GetComponent<HpAndSanity>().HealSanity(HealAmount);
            yield return new WaitForSeconds(HealInterval);
        }
    }

}