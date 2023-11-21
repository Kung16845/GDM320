using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMatches : MonoBehaviour
{
    public Webtrap webtrap;
    public Walllamblight wallamblight;
    private void Awake() 
    {
        wallamblight = FindAnyObjectByType<Walllamblight>();
        webtrap = FindAnyObjectByType<Webtrap>();
        webtrap.Canburn = true;
        wallamblight.lightquip = true;
    }
    private void OnDestroy() 
    {
        wallamblight.lightquip = false;
        webtrap.Canburn = false;
    }
}
