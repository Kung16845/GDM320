using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveItems : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{   
    public UIItemCharactor uIItemCharactor;

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("Begin Drag");
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        Debug.Log("Draging");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("End Drag");
    }
}
