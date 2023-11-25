using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundtriggerEvent : MonoBehaviour
{
    public SoundManager soundManager;
    public string nameofsound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            soundManager.PlaySound(nameofsound);
            Destroy(this.gameObject);
        }
    }
}
