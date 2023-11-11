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
        if (slot == null)
        {   
            GameObject uIitem = eventData.pointerDrag;
            UIItemCharactor uIItemCharactor = uIitem.GetComponent<UIItemCharactor>();
            Vector3 newScale = new Vector3(1f, 1f, 1f);
            uIItemCharactor.GetComponent<RectTransform>().localScale = newScale;
            if (equipment != null)
            {
                if (uIItemCharactor.isOnhand)
                {   
                    if(inventoryPresentCharactor.transform.childCount == 0)
                        inventoryPresentCharactor.CreateItemCharactorEquipment(uIItemCharactor.scriptItem);
                    Vector3 newScaleequipment = new Vector3(1.65f, 1.65f, 1.65f);
                    uIItemCharactor.GetComponent<RectTransform>().localScale = newScaleequipment;
                    uIItemCharactor.parentAfterDray = transform;
                    Debug.Log("eqiupment Onhand");
                }
                Debug.Log("eqiupment not null");
            }
            
            if (!uIItemCharactor.imageItemLock.gameObject.activeInHierarchy && equipment == null)
                uIItemCharactor.parentAfterDray = transform;
            
            
        }
    }
}
