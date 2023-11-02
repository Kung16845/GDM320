using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPresentCharactor : MonoBehaviour
{
    public ItemsDataCharactor[] itemsDataCharactors => itemsListCharactors.ToArray();
    [SerializeField] List<ItemsDataCharactor> itemsListCharactors;
    public GameObject slotprefeb;
    public UIItemCharactor uIItemCharactorPrefeb;
    public List<UIItemCharactor> uIItemListCharactor;
    private void Start()
    {
        uIItemCharactorPrefeb.gameObject.SetActive(false);
        RefreshUIInventoryCharactor();
    }
    public void RefreshUIInventoryCharactor()
    {   
        ClearALLItemCharactors();
        
        for (int i = 1; i <= 12; i++)
        {
            var newItemCharactor = Instantiate(uIItemCharactorPrefeb, uIItemCharactorPrefeb.transform.parent, false);

            newItemCharactor.gameObject.SetActive(true);
            uIItemListCharactor.Add(newItemCharactor);
            if (i > 6)
            {   
                newItemCharactor.isLock = true;
                newItemCharactor.imageItemLock.gameObject.SetActive(true);
            }
        }

        SetDataItemCharactorList(itemsDataCharactors);
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

[Serializable]
public class ItemsDataCharactor
{
    public string nameItemCharactor;
    public Sprite ItemImage;

}
