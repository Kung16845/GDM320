using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Webtrap : MonoBehaviour
{
    public TrapController trapController;
    void Start()
    {
        trapController = FindObjectOfType<TrapController>();
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Leg"))
        {
            trapController.HitbyWebtrap();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Leg"))
        {
            trapController.PlayerReleaseTrap();
        }
    }
}
