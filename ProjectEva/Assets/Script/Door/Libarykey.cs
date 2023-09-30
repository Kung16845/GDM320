using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Libarykey : MonoBehaviour
{
    public keyinventory inventory;

    private void Start()
    {
        // Use GetComponentInParent to find the keyinventory script on the player or any parent GameObject.
          inventory = GameObject.FindWithTag("inventory").GetComponent<keyinventory>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            inventory.AddKey("Libarykey");
            Destroy(this.gameObject); // Remove the collected key from the scene.
        }
    }
}
