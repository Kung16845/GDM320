using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventorypocketflashing : MonoBehaviour
{
    public OnOffLight onOffLight;
    private void Awake() 
    {   
        onOffLight = FindObjectOfType<OnOffLight>();
        onOffLight.pocketcanopen = true;
    }
    private void OnDestroy() 
    {
        onOffLight.pocketcanopen = false;
    }
}
