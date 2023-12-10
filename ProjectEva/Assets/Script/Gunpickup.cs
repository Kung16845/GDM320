using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Gunpickup : MonoBehaviour
{   
    public InventoryPresentCharactor inventoryPresentCharactor;
    public ItemsDataCharactor itemsDataCharactor;
    public InventoryItemNotePresent inventoryItemNotePresent;
    public ItemDataNote itemDataNote;
    public GameObject sceneObject;
    public SoundManager soundManager;
    public TextMeshProUGUI customText;
    public string custominteractiontext;

    private bool canPickup = false;

    public void PickupItemCharactors()
    {   
        inventoryPresentCharactor.AddItemCharactors(itemsDataCharactor);
    }
    private void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        HideEButton();
        customText.text = custominteractiontext;
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
        inventoryItemNotePresent = FindObjectOfType<InventoryItemNotePresent>();
    }
    private void Update()
    {
        // Check if the player can pick up the ammo and 'E' is pressed.
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            soundManager.PlaySound("GrabItem");
            PickupAmmo();
            PickupItemCharactors();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowEButton();
            canPickup = true;
        }
    }
    void PickupAmmo()
    {
        soundManager.PlaySound("Pickupitem");
        customText.text = "";
        Destroy(this.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideEButton();
            canPickup = false;
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
}
