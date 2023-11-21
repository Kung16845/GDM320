using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAlcoholbottom : MonoBehaviour
{
    public Walllamblight walllamblight;
    private void Awake()
    {
        // Find all Walllamblight objects and set their properties
        Walllamblight[] wallamblights = FindObjectsOfType<Walllamblight>();
        foreach (Walllamblight wallamblight in wallamblights)
        {
            wallamblight.fuelequip = true;
        }
    }

    private void OnDestroy()
    {
        Debug.Log("OnDestroy called");
        Walllamblight[] wallamblights = FindObjectsOfType<Walllamblight>();
        foreach (Walllamblight wallamblight in wallamblights)
        {
            wallamblight.fuelequip = false;
        }
    }
}
