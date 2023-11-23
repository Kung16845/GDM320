using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeyonlyDoor : MonoBehaviour
{
    private bool isPlayerNear = false;
    public int hasKeynumber;
    public InventoryPresentCharactor inventoryPresentCharactor;
    public GameObject sceneObject;
    public TextMeshProUGUI customText;
    public SoundManager soundManager;
    public string Keyforthisdoor;
    public int numberofkey;
    public string custominteractiontext;
    private void Start()
    {
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = true;
            
            if (hasKeynumber == numberofkey)
            {
                customText.text = "I have a key.";
            }
            else
            {
                customText.text = custominteractiontext;
            }
            ShowEButton();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HideEButton();
            isPlayerNear = false;
        }
    }
    private void ShowEButton()
    {
        sceneObject.SetActive(true);
        customText.text = custominteractiontext;
    }

    private void HideEButton()
    {
        sceneObject.SetActive(false);
        customText.text = "";
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && hasKeynumber == numberofkey)
        {
            inventoryPresentCharactor.DeleteItemCharactorEquipment(Keyforthisdoor);
            soundManager.PlaySound("Dooropen");
            Destroy(this.gameObject);
        }
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && hasKeynumber == numberofkey)
        {
            soundManager.PlaySound("Doorlocked");
        }
    }
}
