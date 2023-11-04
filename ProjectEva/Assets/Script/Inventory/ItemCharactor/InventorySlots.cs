using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class InventorySlots : MonoBehaviour, IDropHandler
{   

    public void OnDrop(PointerEventData eventData)
    {   
        if(transform.childCount == 0){
        GameObject dropped = eventData.pointerDrag;
        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();
        if(!draggableItem.imageLockitems.gameObject.activeInHierarchy)
            draggableItem.parentAfterDray = transform;
        }
    }
}
