using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ForceandkeyDoor : MonoBehaviour
{
   public TextMeshProUGUI instructionText;
   public keyinventory playerInventory; // Reference to the TMPro UI object.
    public bool hasLibraryKey = false;
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
        if (collision.CompareTag("Player") && hasLibraryKey)
        {
            isPlayerNear = true;
            instructionText.gameObject.SetActive(true);
            instructionText.text = "I have a key for this.";
        }
        if (collision.CompareTag("Bullet"))
        {
            Destroy(this.gameObject); // Destroy the bullet.
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
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && hasLibraryKey)
        {
            Destroy(this.gameObject);
        }
    }
}
