using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Matboxpickup : MonoBehaviour
{
    public GameObject sceneObject;
    public SoundManager soundManager;
    private bool canPickup;
    public InventoryPresentCharactor inventoryPresentCharactor;
    public ItemsDataCharactor itemsDataCharactor;
    public void PickupItemCharactors()
    {   
        inventoryPresentCharactor.AddItemCharactors(itemsDataCharactor);
    }
    private void Start()
    {
        canPickup = false;
        HideEButton();
        soundManager = FindObjectOfType<SoundManager>();
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
    }
    private void Update()
    {
        // Check if the player can pick up the ammo and 'E' is pressed.
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            PickupItemCharactors();
            PickupMatchbox();
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideEButton();
            canPickup = false;
        }
    }
    void PickupMatchbox()
    {
        soundManager.PlaySound("Matchespickup");
        Destroy(this.gameObject);
    }
    private void ShowEButton()
    {
        sceneObject.SetActive(true);
    }

    private void HideEButton()
    {
        sceneObject.SetActive(false);
    }
}
