using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  TMPro;

public class KeyonlyDoor : MonoBehaviour
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
            instructionText.text = "Must find A key Can't shoot the lock.";
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
            playerInventory.RemoveKey("Libarykey");
        }
    }

    private void OpenDoorWithForce()
    {
        // Add your door opening logic here for force-only doors.
    }
}
