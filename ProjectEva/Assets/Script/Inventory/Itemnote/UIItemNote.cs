using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemNote : MonoBehaviour
{
    public TextMeshProUGUI nameItemNote;
    public string detailsItemNote;
    public Image imageItemNote;
    public void SetDataUIItemNote(ItemDataNote itemDataNote)
    {
        nameItemNote.text = itemDataNote.nameItemNote;
        detailsItemNote = itemDataNote.detailsItemNote;
        imageItemNote.sprite = itemDataNote.image;
    }
    
}
