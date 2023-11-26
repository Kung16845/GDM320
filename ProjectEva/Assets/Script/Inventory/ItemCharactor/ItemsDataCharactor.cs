using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    // public void Initialize(string nameItem, Sprite sprite, int countItem, int maxCountItem, string scriptItemCharactor, bool flashLight, bool Onhand)
    // {
    //     nameItemCharactor = nameItem;
    //     ItemImage = sprite;
    //     count = countItem;
    //     maxCount = maxCountItem;
    //     scriptItem = scriptItemCharactor;
    //     isFlashLight = flashLight;
    //     isOnhand = Onhand;
    // }
}
