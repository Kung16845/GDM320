using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
public class BoxItemsCharactor : MonoBehaviour, IDropHandler
{
    public int numslot;
    public float toggleCooldown = 1f; // Adjust the cooldown time as needed
    private float timeSinceLastToggle = 0f;
    public void Update()
    {
        // var slot = GetComponentsInChildren<UIItemCharactor>();
        // if (timeSinceLastToggle >= toggleCooldown)
        // {
        //     foreach (var item in slot)
        //     {
        //         if (item != null)
        //             item.numslot = numslot;
        //     }
        //     timeSinceLastToggle = 0f;
        // }
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
