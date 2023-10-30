using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemCharactor : MonoBehaviour
{
    public TextMeshProUGUI nameItem;
    public Image imageItem;
    public Image imageItemLock;
    public bool isLock;
    public bool isFull;
    public void SetDataUIItemCharactor(ItemsDataCharactor itemsDataCharactor)
    {
        nameItem.text = itemsDataCharactor.nameItemCharactor;
        imageItem.sprite = itemsDataCharactor.ItemImage;
        
    }
}
