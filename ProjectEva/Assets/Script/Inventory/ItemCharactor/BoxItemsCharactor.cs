using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
public class BoxItemsCharactor : MonoBehaviour, IDropHandler
{
    public int numslot;
    public void Update()
    {
        var slot = GetComponentsInChildren<UIItemCharactor>();
        foreach (var item in slot)
        {
            if (item != null)
                item.numslot = numslot;
        }
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject uIitem = eventData.pointerDrag;
        UIItemCharactor uIItemCharactor = uIitem.GetComponent<UIItemCharactor>();
        
        if (uIItemCharactor != null)
        {
            uIItemCharactor.parentAfterDray = transform;
        }


    }
}
