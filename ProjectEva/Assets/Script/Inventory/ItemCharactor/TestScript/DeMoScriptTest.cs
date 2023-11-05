using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeMoScriptTest : MonoBehaviour
{   
    // public InventoryPresentCharactor inventoryPresentCharactor;
    // public ItemsDataCharactor itemsDataCharactor;
    public InventoryItemNotePresent inventoryItemNotePresent;
    public ItemDataNote itemDataNote;
    public void PickupItemNote()
    {
        inventoryItemNotePresent.AddItemsNote(itemDataNote);
    }
    // public void PickupItemCharactors()
    // {   
    //     inventoryPresentCharactor.AddItemCharactors(itemsDataCharactor);
    // }
}
