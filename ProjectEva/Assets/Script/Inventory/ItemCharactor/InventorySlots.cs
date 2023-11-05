using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class InventorySlots : MonoBehaviour, IDropHandler
{   
    
    public void OnDrop(PointerEventData eventData)
    {   
        if(transform.childCount == 0){
        GameObject uIitem = eventData.pointerDrag;
        UIItemCharactor uIItemCharactor = uIitem.GetComponent<UIItemCharactor>();
        if(!uIItemCharactor.imageItemLock.gameObject.activeInHierarchy)
            uIItemCharactor.parentAfterDray = transform;
        }
    }
}
