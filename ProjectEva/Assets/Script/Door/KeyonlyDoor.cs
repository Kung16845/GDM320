using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyonlyDoor : MonoBehaviour
{
    public TextMeshProUGUI instructionText;
    private bool isPlayerNear = false;
    public bool hasLibraryKey = false;
    public InventoryPresentCharactor inventoryPresentCharactor;
    private void Start()
    {
        instructionText.gameObject.SetActive(false);
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = true;
            
            if (hasLibraryKey)
            {
                instructionText.text = "I have a key.";
            }
            else
            {
                instructionText.text = "Must find a key. Can't shoot the lock.";
            }

            instructionText.gameObject.SetActive(true);
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
            inventoryPresentCharactor.ManageReduceResource("Libralykey");
            Destroy(this.gameObject);
        }
    }
}
