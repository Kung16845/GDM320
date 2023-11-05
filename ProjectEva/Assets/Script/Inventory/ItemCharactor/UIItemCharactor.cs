using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UIItemCharactor : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [HideInInspector] public Transform parentAfterDray;
    
    [Header("UI")]
    public TextMeshProUGUI nameItem;
    public TextMeshProUGUI countItemText;
    public Image imageItem;
    public Image imageItemLock;
    public int count;
    public int maxCount;
    public bool isLock;
    public bool isFull;
    public void SetDataUIItemCharactor(ItemsDataCharactor itemsDataCharactor)
    {
        nameItem.text = itemsDataCharactor.nameItemCharactor;
        count = itemsDataCharactor.count;
        // maxCount = itemsDataCharactor.maxCount;
        RefrehCount();
        imageItem.sprite = itemsDataCharactor.ItemImage;
    }
    public void RefrehCount()
    {
        countItemText.text = "X " + count.ToString();
        bool textActive = count > 1;
        countItemText.gameObject.SetActive(textActive);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        parentAfterDray = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        imageItem.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!imageItemLock.gameObject.activeInHierarchy)
            transform.position = Input.mousePosition;

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(parentAfterDray);
        imageItem.raycastTarget = true;
    }
}
