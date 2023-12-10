using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using JetBrains.Annotations;
using System;
public class UIItemCharactor : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform parentAfterDray; //[HideInInspector]
    public Transform parentBeforeDray;
    public Transform slotEqicpmentOnHand;
    public Transform slotEqicpmentFlashLight;
    public Transform boxInventory;

    [Header("UI")]
    public TextMeshProUGUI nameItem;
    public TextMeshProUGUI countItemText;
    public Image imageItem;
    public Image imageItemLock;
    public string scriptItem;
    public int count;
    public int maxCount; 
    public int numslot;
    public bool isLock;
    public bool isFlashLight;
    public bool isOnhand;

    public object Add { get; internal set; }
    private void Awake()
    {   
        
        if(isFlashLight)
            slotEqicpmentFlashLight = FindTranfromsInventorySlot(12);
        if(isOnhand)
            slotEqicpmentOnHand = FindTranfromsInventorySlot(13);
        FindboxInventory();
    }
    public Transform FindTranfromsInventorySlot(int numslot)
    {   
        var allslots = GameObject.FindObjectsOfType<InventorySlots>(true);
        foreach (var slot in allslots)
        {
            if(slot.numslot == numslot)
                return slot.transform;
        }
        return null;
    } 
    public void FindboxInventory()
    {
        var boxes = GameObject.FindObjectsOfType<BoxItemsCharactor>(true);

        foreach(var box in boxes)
        {
            
            boxInventory = box.transform;
            
        }
    }
    public void SetDataUIItemCharactor(ItemsDataCharactor itemsDataCharactor)
    {
        nameItem.text = itemsDataCharactor.nameItemCharactor;
        count = itemsDataCharactor.count;
        maxCount = itemsDataCharactor.maxCount;
        scriptItem = itemsDataCharactor.scriptItem;
        isFlashLight = itemsDataCharactor.isFlashLight;
        isOnhand = itemsDataCharactor.isOnhand;
        isLock = itemsDataCharactor.isLock;
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
        parentBeforeDray = parentAfterDray.transform;

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
        if ((parentBeforeDray == slotEqicpmentOnHand || parentBeforeDray == slotEqicpmentFlashLight) && parentBeforeDray != parentAfterDray)
        {
            Type scriptType = Type.GetType(scriptItem) ;
            var objectitem = FindObjectOfType<InventoryPresentCharactor>();
            Destroy(objectitem.GetComponentInChildren(scriptType).gameObject);
        }
        transform.SetParent(parentAfterDray);
        
        imageItem.raycastTarget = true;
    }
}
