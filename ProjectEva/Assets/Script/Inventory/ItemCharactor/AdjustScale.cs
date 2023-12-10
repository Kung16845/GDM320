using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class AdjustScale : MonoBehaviour
{
    public GameObject childObject;

    void Start()
    {
        // เพิ่ม LayoutElement และ ContentSizeFitter component ที่ child object
        LayoutElement layoutElement = childObject.AddComponent<LayoutElement>();
        ContentSizeFitter contentSizeFitter = childObject.AddComponent<ContentSizeFitter>();

        // กำหนดค่าใน LayoutElement
        layoutElement.minWidth = 0;
        layoutElement.minHeight = 0;

        // กำหนดค่าใน ContentSizeFitter
        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.PreferredSize;
        contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.PreferredSize;
    }
}
