using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ForceandkeyDoor : MonoBehaviour
{
   public TextMeshProUGUI instructionText;
   public keyinventory playerInventory; // Reference to the TMPro UI object.

    private bool isPlayerNear = false;

    private void Start()
    {
        instructionText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = true;
            instructionText.gameObject.SetActive(true);
            instructionText.text = "It's lock Must find A key or maybe...";
        }
        if (collision.CompareTag("Bullet"))
        {
            // Check if a bullet collided with the door.
            // Add any additional conditions here, e.g., bullet type.
            Destroy(this.gameObject); // Destroy the bullet.
            OpenDoorWithForce(); // Open the door when hit by a bullet.
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = false;
            instructionText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && playerInventory.HasKey("Libarykey"))
        {
            Destroy(this.gameObject);
             // Remove the used key from the inventory.
        }
    }

    private void OpenDoorWithForce()
    {
        // Add your door opening logic here for force-only doors.
    }
}
