using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class BoxItemsCharactor : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        GameObject uIitem = eventData.pointerDrag;
        UIItemCharactor uIItemCharactor = uIitem.GetComponent<UIItemCharactor>();
        uIItemCharactor.parentAfterDray = transform;
        
        MoveVariableFromlistCharactorToListCharactorBoxs(uIItemCharactor);

        
    }
    
    public void MoveVariableFromlistCharactorToListCharactorBoxs(UIItemCharactor variableToMove)
    {
        
        var inventoryItemsCharactor = FindAnyObjectByType<InventoryPresentCharactor>();
        // ลบตัวแปรจาก List 1
        inventoryItemsCharactor.uIItemListCharactor.Remove(variableToMove);

        // เพิ่มตัวแปรลงใน List 2
        inventoryItemsCharactor.uIItemListCharactorInboxs.Add(variableToMove);

    }
    
    public void MoveVariableFromListCharactorBoxsToListCharactor(UIItemCharactor variableToMove)
    {
         var inventoryItemsCharactor = FindAnyObjectByType<InventoryPresentCharactor>();
        // ลบตัวแปรจาก List 1
        inventoryItemsCharactor.uIItemListCharactorInboxs.Remove(variableToMove);

        // เพิ่มตัวแปรลงใน List 2
        inventoryItemsCharactor.uIItemListCharactor.Add(variableToMove);
    }
}
