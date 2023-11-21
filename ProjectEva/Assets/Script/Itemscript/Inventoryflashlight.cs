using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventoryflashlight : MonoBehaviour
{   
    public OnOffLight onOffLight;
    private void Awake() 
    {   
        onOffLight = FindObjectOfType<OnOffLight>();
        onOffLight.canopen = true;
    }
    private void OnDestroy() 
    {
        onOffLight.canopen = false;
    }
}
