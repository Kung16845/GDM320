using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemNotePresent : MonoBehaviour
{
    public ItemDataNote[] itemsDataNotes => itemsListNotes.ToArray();
    [SerializeField] List<ItemDataNote> itemsListNotes;
    public UIItemNote uIItemNotePrefeb;
    public List<UIItemNote> uIItemListNotes;
    public int currentCategory;
    private void Start() 
    {
        uIItemNotePrefeb.gameObject.SetActive(false);
        currentCategory = 1;
        RefreshUIInventoryItenNote();
    }
    public void ChangeCategory(int n)
    {
        currentCategory = n;
        RefreshUIInventoryItenNote();
    }
    public void RefreshUIInventoryItenNote()
    {   
        ClearALLItemCharactors();

        var itemsCurrentCategory = GetCategory((Category)currentCategory);

        var uiItemNoteDatas = new List<ItemDataNote>();

        foreach (var items in itemsCurrentCategory)
        {
            uiItemNoteDatas.Add(items);
        }

        SetItemNoteList(uiItemNoteDatas.ToArray());
    }
    public void SetItemNoteList(ItemDataNote[] itemDataNotes)
    {
        foreach(var uiItemNote in itemDataNotes)
        {
            var newItemNoteUI = Instantiate(uIItemNotePrefeb,uIItemNotePrefeb.transform.parent,false);

            newItemNoteUI.gameObject.SetActive(true);
            uIItemListNotes.Add(newItemNoteUI);
            newItemNoteUI.SetDataUIItemNote(uiItemNote);
        }
    }
    public ItemDataNote[] GetCategory(Category category)
    {
        var dataListCategory = new List<ItemDataNote>();
        foreach (var item in itemsListNotes)
        {
            if(item.type == category)
                dataListCategory.Add(item);
        }

        return dataListCategory.ToArray();
    }
    public void ClearALLItemCharactors()
    {
        foreach (var uiItem in uIItemListNotes)
            Destroy(uiItem.gameObject);
        uIItemListNotes.Clear();
    }
}

[Serializable]
public class ItemDataNote
{
    public string nameItemNote;
    public string detailsItemNote;
    public Sprite image;
    public Category type;
}
public enum Category
{
    Category1 = 1,
    Category2,
    Category3
}