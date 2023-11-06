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
    public void UnlockSlot()
    {
        var slotLock = uIItemListCharactor.FirstOrDefault(slotsImage => slotsImage.imageItemLock.gameObject.activeInHierarchy);
        Destroy(slotLock.gameObject);
        uIItemListCharactor.Remove(slotLock);
    }
    public void ManageReduceResource(string nameItemReduceCharactor)
    {

        // for (int i = 0; i < slots.Count; i++)
        // {
        //     InventorySlots slot = slots.ElementAt(i).GetComponent<InventorySlots>();
        //     UIItemCharactor itemInSlot = slot.GetComponentInChildren<UIItemCharactor>();
        //     if (itemInSlot != null && itemInSlot.count > 0 
        //      && itemInSlot.nameItem.text == nameItemReduceCharactor)
        //     {   
        //         itemInSlot.count--;
        //         itemInSlot.RefrehCount();
        //         if(itemInSlot.count <= 0)
        //             Destroy(itemInSlot.gameObject);
        //         return;
        //     }        
        // }
        var ItemReduceCharactor = uIItemListCharactor.Where(name => name.nameItem.text == nameItemReduceCharactor)
        .OrderBy(num => num.count).First();
        ItemReduceCharactor.count--;
        ItemReduceCharactor.RefrehCount();
        if (ItemReduceCharactor.count <= 0)
        {
            Destroy(ItemReduceCharactor.gameObject);
            uIItemListCharactor.Remove(ItemReduceCharactor);
        }
    }
    public void AddItemCharactors(ItemsDataCharactor itemsDataCharactor)
    {

        for (int i = 0; i < slots.Count; i++)
        {
            InventorySlots slot = slots.ElementAt(i).GetComponent<InventorySlots>();
            UIItemCharactor itemInSlot = slot.GetComponentInChildren<UIItemCharactor>();
            if (itemInSlot != null && itemInSlot.count < itemInSlot.maxCount
            && itemInSlot.nameItem.text == itemsDataCharactor.nameItemCharactor)
            {
                var sum = 0;
                itemInSlot.count += itemsDataCharactor.count;
                sum = itemInSlot.count;
                itemInSlot.RefrehCount();
                if (itemInSlot.count > itemInSlot.maxCount)
                {
                    itemInSlot.count = itemInSlot.maxCount;
                    itemInSlot.RefrehCount();
                    sum -= itemInSlot.maxCount;

                    var slotNull = slots.FirstOrDefault(slot => slot.GetComponentInChildren<UIItemCharactor>() == null);
                    var newItemGo = Instantiate(uIItemCharactorPrefeb, slotNull.transform, false);
                    
                    newItemGo.SetDataUIItemCharactor(new ItemsDataCharactor
                    (
                        itemsDataCharactor.nameItemCharactor
                        , itemsDataCharactor.ItemImage
                        , sum
                        , itemsDataCharactor.maxCount
                    ));

                    newItemGo.gameObject.SetActive(true);
                    uIItemListCharactor.Add(newItemGo);
                }
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

    public ItemsDataCharactor(string nameItem, Sprite sprite, int count, int maxCount)
    {
        this.nameItemCharactor = nameItem;
        this.ItemImage = sprite;
        this.count = count;
        this.maxCount = maxCount;
    }
}
