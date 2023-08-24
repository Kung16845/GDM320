using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    public float damageInterval; // เวลาต่อครั้งที่จะทำดาเมจ (วินาที)
    public float damageAmount; // จำนวนดาเมจที่จะทำต่อครั้ง
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
            player.GetComponent<HpAndSanity>().TakeSanity(damageAmount);
            yield return new WaitForSeconds(damageInterval);
        }
    }
}
