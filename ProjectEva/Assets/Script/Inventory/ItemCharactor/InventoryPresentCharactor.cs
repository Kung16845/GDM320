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
    public GameObject slotsEquipment;
    public GameObject inventorypage;
    public bool openInven;
    public UIItemCharactor uIItemCharactorPrefeb;
    public List<UIItemCharactor> uIItemListCharactor;
    public float toggleCooldown = 0.5f; // Adjust the cooldown time as needed
    private float timeSinceLastToggle = 0.5f;

    private void Start()
    {
        uIItemCharactorPrefeb.gameObject.SetActive(false);
        RefreshUIInventoryCharactor();
    }
    private void Update()
    {
        timeSinceLastToggle += Time.deltaTime;

        if (Input.GetKey(KeyCode.Tab) && timeSinceLastToggle >= toggleCooldown)
        {
            ToggleInventory();
            timeSinceLastToggle = 0f;
        }
    }
    private void ToggleInventory()
    {
        openInven = !openInven;
        inventorypage.SetActive(openInven);
    }
    public void UnlockSlot()
    {
        var slotLock = uIItemListCharactor.FirstOrDefault(slotsImage => slotsImage.isLock);
        uIItemListCharactor.Remove(slotLock);
        Destroy(slotLock.gameObject);

    }
    public void DeleteItemCharactorEquipment()
    {   
        Debug.Log("DeleteItemCharactorEquipment");
        Transform child = transform.GetChild(0);
        DestroyImmediate(child.gameObject);
        Destroy(slotsEquipment.GetComponentInChildren<UIItemCharactor>().gameObject);
    }
    public void CreateItemCharactorEquipment(string scriptItem,string nameItem)
    {
        var newEmptyObjectForItem = Instantiate(new GameObject(nameItem), this.transform);
        Type scriptType = Type.GetType(scriptItem);
        var addedComponent = newEmptyObjectForItem.AddComponent(scriptType) as Behaviour; // ให้เป็น Behaviour

        // เปิดใช้งาน Component ที่เพิ่มเข้าไป
        if (addedComponent != null)
        {
            // เปิดใช้งาน Behaviour
            addedComponent.enabled = true;
        }
    }
    public void ManageReduceResource(string nameItemReduceCharactor)
    {

        // for (int i = 0; i < slots.Count; i++)
        // {D
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
        .OrderByDescending(num => num.count).LastOrDefault();
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

        var ItemAddCharactors = uIItemListCharactor.Where(name => name.nameItem.text == itemsDataCharactor.nameItemCharactor
        && name.count != name.maxCount).
        OrderByDescending(num => num.count).FirstOrDefault();

        var slotNull = slots.FirstOrDefault(slot => slot.GetComponentInChildren<UIItemCharactor>() == null);

        if (ItemAddCharactors == null && slotNull != null)
        {
            SpawnNewItem(itemsDataCharactor, slotNull.GetComponentInChildren<InventorySlots>());
            return;
        }

        else if (ItemAddCharactors != null)
        {
            var sum = 0;
            ItemAddCharactors.count += itemsDataCharactor.count;
            ItemAddCharactors.RefrehCount();
            sum = ItemAddCharactors.count;

            if (ItemAddCharactors.count > ItemAddCharactors.maxCount)
            {
                ItemAddCharactors.count = ItemAddCharactors.maxCount;
                ItemAddCharactors.RefrehCount();
                sum -= ItemAddCharactors.maxCount;

                var newItemGo = Instantiate(uIItemCharactorPrefeb, slotNull.transform, false);

                newItemGo.SetDataUIItemCharactor(new ItemsDataCharactor
                (
                    itemsDataCharactor.nameItemCharactor
                    , itemsDataCharactor.ItemImage
                    , sum
                    , itemsDataCharactor.maxCount
                    , itemsDataCharactor.scriptItem
                    , itemsDataCharactor.isFlashLight
                    , itemsDataCharactor.isOnhand
                ));

                newItemGo.gameObject.SetActive(true);
                uIItemListCharactor.Add(newItemGo);

                return;
            }
        }
        else if (slotNull != null && ItemAddCharactors != null)
        {
            // ถ้าของเต็มให้ทำอะไรบ้างอย่าง 
            return;
        }


        // for (int i = 0; i < slots.Count; i++)
        // {
        //     InventorySlots slot = slots.ElementAt(i).GetComponent<InventorySlots>();
        //     UIItemCharactor itemInSlot = slot.GetComponentInChildren<UIItemCharactor>();
        //     if (itemInSlot != null && itemInSlot.count < itemInSlot.maxCount
        //     && itemInSlot.nameItem.text == itemsDataCharactor.nameItemCharactor)
        //     {
        //         var sum = 0;
        //         itemInSlot.count += itemsDataCharactor.count;
        //         sum = itemInSlot.count;
        //         itemInSlot.RefrehCount();
        //         if (itemInSlot.count > itemInSlot.maxCount)
        //         {
        //             itemInSlot.count = itemInSlot.maxCount;
        //             itemInSlot.RefrehCount();
        //             sum -= itemInSlot.maxCount;

        //             var slotNull = slots.FirstOrDefault(slot => slot.GetComponentInChildren<UIItemCharactor>() == null);
        //             var newItemGo = Instantiate(uIItemCharactorPrefeb, slotNull.transform, false);

        //             newItemGo.SetDataUIItemCharactor(new ItemsDataCharactor
        //             (
        //                 itemsDataCharactor.nameItemCharactor
        //                 , itemsDataCharactor.ItemImage
        //                 , sum
        //                 , itemsDataCharactor.maxCount
        //             ));

        //             newItemGo.gameObject.SetActive(true);
        //             uIItemListCharactor.Add(newItemGo);
        //         }
        //         return;

        //     }
        //     else if (itemInSlot == null)
        //     {
        //         SpawnNewItem(itemsDataCharactor, slot);
        //         return;
        //     }
        // }
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
    public string scriptItem;
    public int count;
    public int maxCount;
    public bool isFlashLight;
    public bool isOnhand;
    public ItemsDataCharactor(string nameItem, Sprite sprite, int count, int maxCount, string scriptItem, bool flashLight, bool Onhand)
    {
        this.nameItemCharactor = nameItem;
        this.ItemImage = sprite;
        this.count = count;
        this.maxCount = maxCount;
        this.scriptItem = scriptItem;
        this.isFlashLight = flashLight;
        this.isOnhand = Onhand;
    }
}
