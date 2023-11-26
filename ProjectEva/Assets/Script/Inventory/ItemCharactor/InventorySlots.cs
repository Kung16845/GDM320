using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
public class InventorySlots : MonoBehaviour, IDropHandler
{
    public GameObject equipment;
    public InventoryPresentCharactor inventoryPresentCharactor;
    public void OnDrop(PointerEventData eventData)
    {
        var slot = GetComponentInChildren<UIItemCharactor>();
        GameObject uIitem = eventData.pointerDrag;
        UIItemCharactor uIItemCharactor = uIitem.GetComponent<UIItemCharactor>();
        if (slot == null)
        {

            Vector3 newScale = new Vector3(1f, 1f, 1f);
            uIItemCharactor.GetComponent<RectTransform>().localScale = newScale;
            if (equipment != null)
            {
                if (uIItemCharactor.isOnhand && equipment.gameObject.transform == uIItemCharactor.slotEqicpmentOnHand)
                {
                    // if(inventoryPresentCharactor.transform.childCount == 0)
                    inventoryPresentCharactor.CreateItemCharactorEquipment(uIItemCharactor.scriptItem, uIItemCharactor.nameItem.text);
                    Vector3 newScaleequipment = new Vector3(1.65f, 1.65f, 1.65f);
                    uIItemCharactor.GetComponent<RectTransform>().localScale = newScaleequipment;
                    uIItemCharactor.parentAfterDray = transform;
                    Debug.Log("eqiupment Onhand");
                }
                else if (uIItemCharactor.isFlashLight && equipment.gameObject.transform == uIItemCharactor.slotEqicpmentFlashLight)
                {
                    // if(inventoryPresentCharactor.transform.childCount == 0)
                    inventoryPresentCharactor.CreateItemCharactorEquipment(uIItemCharactor.scriptItem, uIItemCharactor.nameItem.text);
                    Vector3 newScaleequipment = new Vector3(1.65f, 1.65f, 1.65f);
                    uIItemCharactor.GetComponent<RectTransform>().localScale = newScaleequipment;
                    uIItemCharactor.parentAfterDray = transform;
                    Debug.Log("eqiupment FlashLight");
                }
                Debug.Log("eqiupment not null");
            }
            else if (!uIItemCharactor.imageItemLock.gameObject.activeInHierarchy && equipment == null)
                uIItemCharactor.parentAfterDray = transform;
        }

        else if (slot != null && !slot.isLock)
        {
            slot.transform.SetParent(uIItemCharactor.parentAfterDray);
            Vector3 newScale = new Vector3(1f, 1f, 1f);
            slot.GetComponent<RectTransform>().localScale = newScale;
            if (equipment != null)
            {
                Vector3 newScaleequipment = new Vector3(1.65f, 1.65f, 1.65f);
                uIItemCharactor.GetComponent<RectTransform>().localScale = newScaleequipment;
            }
            uIItemCharactor.parentAfterDray = transform;
        }
    }
}
