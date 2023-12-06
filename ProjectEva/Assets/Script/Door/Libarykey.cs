using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Libarykey : MonoBehaviour
{
    public SoundManager soundManager;
    public InventoryPresentCharactor inventoryPresentCharactor;
    public ItemsDataCharactor itemsDataCharactor;
    public GameObject sceneObject;
    public TextMeshProUGUI customText;
    public string custominteractiontext;
    private bool canPickup;

    private void Start()
    {
        // Use GetComponentInParent to find the keyinventory script on the player or any parent GameObject.
        HideEButton();
        customText.text = custominteractiontext;
        canPickup = false;
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
    }
    private void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E) && !inventoryPresentCharactor.checkIsSlotFull)
        {
            keypickup();
            customText.text = "";
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
    public void PickupItemCharactors()
    {   
        inventoryPresentCharactor.AddItemCharactors(itemsDataCharactor);
    }
    void keypickup()
    {
        soundManager.PlaySound("Pickupkey");
        PickupItemCharactors();
        Destroy(this.gameObject); // Remove the collected key from the scene.
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
