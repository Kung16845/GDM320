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
    [HideInInspector] public Transform parentAfterDray;
    public Transform parentBeforeDray;
    public Transform slotEqicpmentOnHand;
    public Transform slotEqicpmentFlashLight;

    [Header("UI")]
    public TextMeshProUGUI nameItem;
    public TextMeshProUGUI countItemText;
    public Image imageItem;
    public Image imageItemLock;
    public string scriptItem;
    public int count;
    public int maxCount;
    public bool isLock;
    public bool isFlashLight;
    public bool isOnhand;

    public object Add { get; internal set; }
    private void Awake()
    {   
        if(isFlashLight)
            slotEqicpmentFlashLight = GameObject.Find("BgFlashLight").transform;
        if(isOnhand)
            slotEqicpmentOnHand = GameObject.Find("BgOnHand").transform;
        
    }
    public void SetDataUIItemCharactor(ItemsDataCharactor itemsDataCharactor)
    {
        nameItem.text = itemsDataCharactor.nameItemCharactor;
        count = itemsDataCharactor.count;
        maxCount = itemsDataCharactor.maxCount;
        scriptItem = itemsDataCharactor.scriptItem;
        isFlashLight = itemsDataCharactor.isFlashLight;
        isOnhand = itemsDataCharactor.isOnhand;
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
