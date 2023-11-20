using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gunpickup : MonoBehaviour
{   
    public InventoryPresentCharactor inventoryPresentCharactor;
    public ItemsDataCharactor itemsDataCharactor;
    public InventoryItemNotePresent inventoryItemNotePresent;
    public ItemDataNote itemDataNote;

    public void PickupItemCharactors()
    {   
        inventoryPresentCharactor.AddItemCharactors(itemsDataCharactor);
    }
    private void Start()
    {
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
        inventoryItemNotePresent = FindObjectOfType<InventoryItemNotePresent>();
    }
     private void OnTriggerEnter2D(Collider2D other)
    {
        PickupItemCharactors();
    }
}
