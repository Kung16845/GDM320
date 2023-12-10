using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventorylamp : MonoBehaviour
{
    public OnOffLight onOffLight;
    private void Awake() 
    {   
        onOffLight = FindObjectOfType<OnOffLight>();
        onOffLight.lampopen = true;
    }
    private void OnDestroy() 
    {
        onOffLight.lampopen = false;
    }
}
