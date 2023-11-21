using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAlcoholbottom : MonoBehaviour
{
    public Walllamblight wallamblight;
    private void Awake() 
    {
        wallamblight = FindAnyObjectByType<Walllamblight>();
        wallamblight.fuelequip = true;
    }
    private void OnDestroy() 
    {
        wallamblight.fuelequip = false;
    }
}