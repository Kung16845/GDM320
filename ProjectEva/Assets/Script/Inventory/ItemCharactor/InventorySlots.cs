using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class InventorySlots : MonoBehaviour, IDropHandler
{
    public GameObject equipment;
    public InventoryPresentCharactor inventoryPresentCharactor;
    public int numslot;
    private void Start()
    {
        inventoryPresentCharactor = FindAnyObjectByType<InventoryPresentCharactor>();
    }
    public void Update()
    {
        var slot = GetComponentInChildren<UIItemCharactor>();
        if (slot != null)
            slot.numslot = numslot;
    }
    public void OnDrop(PointerEventData eventData)
    {
        var slot = GetComponentInChildren<UIItemCharactor>();
        var listUIITems = inventoryPresentCharactor.uIItemListCharactor;
        var listUIITemsInbox = inventoryPresentCharactor.uIItemListCharactorInboxs;
        GameObject uIitem = eventData.pointerDrag;
        UIItemCharactor uIItemCharactor = uIitem.GetComponent<UIItemCharactor>();
        if (slot == null)
        {
            Debug.Log("1");

            if (equipment != null)
            {
                Debug.Log("1.1");
                if (uIItemCharactor.isOnhand && equipment.gameObject.transform == uIItemCharactor.slotEqicpmentOnHand)
                {

                    inventoryPresentCharactor.CreateItemCharactorEquipment(uIItemCharactor.scriptItem, uIItemCharactor.nameItem.text);
                    Vector3 newScaleequipment = new Vector3(1.65f, 1.65f, 1.65f);
                    uIItemCharactor.GetComponent<RectTransform>().localScale = newScaleequipment;
                    uIItemCharactor.parentAfterDray = transform;
                    Debug.Log("eqiupment Onhand");
                }
                else if (uIItemCharactor.isFlashLight && equipment.gameObject.transform == uIItemCharactor.slotEqicpmentFlashLight)
                {

                    inventoryPresentCharactor.CreateItemCharactorEquipment(uIItemCharactor.scriptItem, uIItemCharactor.nameItem.text);
                    Vector3 newScaleequipment = new Vector3(1.65f, 1.65f, 1.65f);
                    uIItemCharactor.GetComponent<RectTransform>().localScale = newScaleequipment;
                    uIItemCharactor.parentAfterDray = transform;
                    Debug.Log("eqiupment FlashLight");
                }

            }
            else if (!uIItemCharactor.imageItemLock.gameObject.activeInHierarchy && equipment == null)
            {
                Debug.Log("1.2");
                Vector3 newScale = new Vector3(1f, 1f, 1f);
                uIItemCharactor.GetComponent<RectTransform>().localScale = newScale;
                uIItemCharactor.parentAfterDray = transform;
                var boxItems = uIItemCharactor.boxInventory.GetComponent<BoxItemsCharactor>();
                var checkUIitem = listUIITems.FirstOrDefault(uIitem => uIitem == uIItemCharactor);
                if (uIItemCharactor.parentAfterDray.transform == this.transform && checkUIitem == null)
                    boxItems.MoveVariableFromListCharactorBoxsToListCharactor(uIItemCharactor);

            }

        }

        else if (slot != null && !slot.isLock && equipment == null)
        {

            if ((uIItemCharactor.isFlashLight || uIItemCharactor.isOnhand) &&
            (uIItemCharactor.parentBeforeDray == uIItemCharactor.slotEqicpmentOnHand ||
            uIItemCharactor.parentBeforeDray == uIItemCharactor.slotEqicpmentFlashLight))
            {

                if (slot.isOnhand && uIItemCharactor.isOnhand)
                {

                    slot.transform.SetParent(uIItemCharactor.parentAfterDray);
                    Vector3 newScale = new Vector3(1.65f, 1.65f, 1.65f);
                    slot.GetComponent<RectTransform>().localScale = newScale;
                    uIItemCharactor.parentAfterDray = transform;

                    Vector3 newScaleequipment = new Vector3(1f, 1f, 1f);
                    uIItemCharactor.GetComponent<RectTransform>().localScale = newScaleequipment;

                    inventoryPresentCharactor.CreateItemCharactorEquipment(slot.scriptItem, slot.nameItem.text);
                }
                else if (slot.isFlashLight && uIItemCharactor.isFlashLight)
                {

                    slot.transform.SetParent(uIItemCharactor.parentAfterDray);
                    Vector3 newScale = new Vector3(1.65f, 1.65f, 1.65f);

                    slot.GetComponent<RectTransform>().localScale = newScale;
                    uIItemCharactor.parentAfterDray = transform;

                    Vector3 newScaleequipment = new Vector3(1f, 1f, 1f);
                    uIItemCharactor.GetComponent<RectTransform>().localScale = newScaleequipment;

                    inventoryPresentCharactor.CreateItemCharactorEquipment(slot.scriptItem, slot.nameItem.text);
                }
            }
            else
            {
                Debug.Log("2.2");
                var boxItems = uIItemCharactor.boxInventory.GetComponent<BoxItemsCharactor>();
                uIItemCharactor.numslot = numslot;

                slot.transform.SetParent(uIItemCharactor.parentAfterDray);
                Vector3 newScale = new Vector3(1f, 1f, 1f);
                slot.GetComponent<RectTransform>().localScale = newScale;
                uIItemCharactor.parentAfterDray = transform;
                var checkUIitemInbox = listUIITemsInbox.FirstOrDefault(uIitem => uIitem == slot);
                var checkUIitem = listUIITems.FirstOrDefault(uIitem => uIitem == uIItemCharactor);

                if (uIItemCharactor.parentAfterDray.transform == this.transform
                && checkUIitemInbox == null && checkUIitem == null)
                {
                    boxItems.MoveVariableFromListCharactorBoxsToListCharactor(uIItemCharactor);
                    boxItems.MoveVariableFromlistCharactorToListCharactorBoxs(slot);
                }
            }
        }
        else if (slot != null && equipment != null && !slot.isLock)
        {

            if (slot.isOnhand && uIItemCharactor.isOnhand)
            {
                slot.transform.SetParent(uIItemCharactor.parentAfterDray);

                Vector3 newScale = new Vector3(1f, 1f, 1f);
                slot.GetComponent<RectTransform>().localScale = newScale;

                Vector3 newScaleequipment = new Vector3(1.65f, 1.65f, 1.65f);
                uIItemCharactor.GetComponent<RectTransform>().localScale = newScaleequipment;

                inventoryPresentCharactor.CreateItemCharactorEquipment(uIItemCharactor.scriptItem, uIItemCharactor.nameItem.text);
                uIItemCharactor.parentAfterDray = transform;

                Type scriptType = Type.GetType(slot.scriptItem);
                var itemInslotforDestroy = inventoryPresentCharactor.GetComponentInChildren(scriptType).gameObject;
                Debug.Log(itemInslotforDestroy);
                Destroy(itemInslotforDestroy);
            }
            else if (slot.isFlashLight && uIItemCharactor.isFlashLight)
            {
                slot.transform.SetParent(uIItemCharactor.parentAfterDray);

                Vector3 newScale = new Vector3(1f, 1f, 1f);
                slot.GetComponent<RectTransform>().localScale = newScale;

                Vector3 newScaleequipment = new Vector3(1.65f, 1.65f, 1.65f);
                uIItemCharactor.GetComponent<RectTransform>().localScale = newScaleequipment;

                inventoryPresentCharactor.CreateItemCharactorEquipment(uIItemCharactor.scriptItem, uIItemCharactor.nameItem.text);
                uIItemCharactor.parentAfterDray = transform;

                Type scriptType = Type.GetType(slot.scriptItem);
                GameObject itemInslotforDestroy = inventoryPresentCharactor.GetComponentInChildren(scriptType).gameObject;
                Destroy(itemInslotforDestroy);

            }
        }
    }
}