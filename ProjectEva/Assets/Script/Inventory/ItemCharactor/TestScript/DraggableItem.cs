using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image imageitems;
    public Image imageLockitems;
    [HideInInspector] public Transform parentAfterDray;

    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDray = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        imageitems.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!imageLockitems.gameObject.activeInHierarchy)
            transform.position = Input.mousePosition;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDray);
        imageitems.raycastTarget = true;
    }
}
