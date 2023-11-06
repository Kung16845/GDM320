using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPresentCharactor : MonoBehaviour
{
    public ItemsDataCharactor[] itemsDataCharactors => itemsListCharactors.ToArray();
    [SerializeField] List<ItemsDataCharactor> itemsListCharactors;
    public List<GameObject> slots;
    public UIItemCharactor uIItemCharactorPrefeb;
    public List<UIItemCharactor> uIItemListCharactor;
    private void Start()
    {
        uIItemCharactorPrefeb.gameObject.SetActive(false);
        RefreshUIInventoryCharactor();
    }
    public void ManageReduceResource(string nameItemReduceCharactor)
    {   
        // var listItemReduce = new List<UIItemCharactor>();
        for (int i = 0; i < slots.Count; i++)
        {
            InventorySlots slot = slots.ElementAt(i).GetComponent<InventorySlots>();
            UIItemCharactor itemInSlot = slot.GetComponentInChildren<UIItemCharactor>();
            if (itemInSlot != null && itemInSlot.count > 0 
             && itemInSlot.nameItem.text == nameItemReduceCharactor)
            {   
                // listItemReduce.Add(itemInSlot);
                itemInSlot.count--;
                itemInSlot.RefrehCount();
                if(itemInSlot.count <= 0)
                    Destroy(itemInSlot.gameObject);
                return;
            }        
        }
        // var itemRedece = listItemReduce.OrderBy(num => num.count).First();
        // itemRedece.count--;
        
    }
    public void AddItemCharactors(ItemsDataCharactor itemsDataCharactor)
    {

        for (int i = 0; i < slots.Count; i++)
        {
            InventorySlots slot = slots.ElementAt(i).GetComponent<InventorySlots>();
            UIItemCharactor itemInSlot = slot.GetComponentInChildren<UIItemCharactor>();
            if (itemInSlot != null && itemInSlot.count < itemsDataCharactor.maxCount
            && itemInSlot.nameItem.text == itemsDataCharactor.nameItemCharactor)
            {   
                itemInSlot.count++;
                itemInSlot.RefrehCount();
                return;
            }
            else if (itemInSlot == null)
            {
                SpawnNewItem(itemsDataCharactor, slot);
                return;
            }
        }
    }
    public void SpawnNewItem(ItemsDataCharactor item, InventorySlots slot)
    {
        var newItemGo = Instantiate(uIItemCharactorPrefeb, slot.transform, false);
        newItemGo.SetDataUIItemCharactor(item);
        newItemGo.gameObject.SetActive(true);
        uIItemListCharactor.Add(newItemGo);
    }

    public void RefreshUIInventoryCharactor()
    {
        ClearALLItemCharactors();

        SetUIItemsInSlots();

        SetDataItemCharactorList(itemsDataCharactors);
    }
    public void SetUIItemsInSlots()
    {
        var newItemCharactor = new UIItemCharactor();
        for (int i = 0; i < slots.Count; i++)
        {
            if (i < itemsDataCharactors.Length)
            {
                newItemCharactor = Instantiate(uIItemCharactorPrefeb, slots.ElementAt(i).transform, false);
                newItemCharactor.gameObject.SetActive(true);
                uIItemListCharactor.Add(newItemCharactor);
            }
            else if (i > 5)
            {
                newItemCharactor = Instantiate(uIItemCharactorPrefeb, slots.ElementAt(i).transform, false);
                newItemCharactor.gameObject.SetActive(true);
                newItemCharactor.isLock = true;
                newItemCharactor.imageItemLock.gameObject.SetActive(true);
                uIItemListCharactor.Add(newItemCharactor);
            }
        }
    }
    public void SetDataItemCharactorList(ItemsDataCharactor[] uIItemCharactordatas)
    {
        int i = 0;
        foreach (var uiItem in uIItemCharactordatas)
        {
            uIItemListCharactor.ElementAt<UIItemCharactor>(i).SetDataUIItemCharactor(uiItem);
            i++;
        }
    }
    public void ClearALLItemCharactors()
    {
        foreach (var uiItem in uIItemListCharactor)
            Destroy(uiItem.gameObject);
        uIItemListCharactor.Clear();
    }
}

[CreateAssetMenuAttribute(menuName = "ScriptableObject object/ItemChractor")]
[Serializable]
public class ItemsDataCharactor : ScriptableObject
{
    public string nameItemCharactor;
    public Sprite ItemImage;
    public int count;
    public int maxCount;
}
