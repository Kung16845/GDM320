using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISetDetails : MonoBehaviour
{
    public TextMeshProUGUI nameItem;
    public TextMeshProUGUI detailsItem;
    public Image image;

    public void SetPresent(UIItemNote uIItemNote)
    {
        nameItem = GameObject.Find("NameItems").GetComponent<TextMeshProUGUI>();

        if ((int)uIItemNote.category == 2 )
            detailsItem = GameObject.Find("DetailsItems").GetComponent<TextMeshProUGUI>();
        else if ((int)uIItemNote.category == 3 )
            image = GameObject.Find("PictureItem").GetComponent<Image>();

        if (nameItem != null && detailsItem != null && image != null)
        {
            nameItem = uIItemNote.nameItemNote;
            detailsItem.text = uIItemNote.detailsItemNote;
            image.sprite = uIItemNote.imageItemNote.sprite;
        }
        
    }
}
