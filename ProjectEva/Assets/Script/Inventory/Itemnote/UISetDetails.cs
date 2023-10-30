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
        nameItem = uIItemNote.nameItemNote;
        detailsItem.text = uIItemNote.detailsItemNote;
        image.sprite = uIItemNote.imageItemNote.sprite;
    }
}
