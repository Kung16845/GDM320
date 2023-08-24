using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealAreaSanity : MonoBehaviour
{
    public float HealInterval ; // เวลาต่อครั้งที่จะทำฟื้นฟู (วินาที)
    public float HealAmount ; // จำนวนฟื้นฟูที่จะทำต่อครั้ง
    bool isStay = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        isStay = true;
        if (other.CompareTag("Player")) // ตรวจสอบว่าที่เข้ามาในพื้นที่คือ Player หรือไม่
        {
            StartCoroutine(DamageRoutine(other.gameObject));
        } 
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        isStay = false;
        if (other.CompareTag("Player"))
        {
            StopCoroutine(DamageRoutine(other.gameObject));
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