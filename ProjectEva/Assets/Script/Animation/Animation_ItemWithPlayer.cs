using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;


public class Animation_ItemWithPlayer : MonoBehaviour
{
    public Animation_PlayerMovement PlayerAnim;
    SpriteRenderer currentItem;
    private void Start()
    {
        currentItem = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        

    }

    public void changItemOnHandTo(string itemToChange)
    {
        var sp = Resources.Load("Assets / Resources / " + itemToChange + ".asset");

        currentItem.sprite = sp.GetComponent<ItemsDataCharactor>().ItemImage;
    }
}
