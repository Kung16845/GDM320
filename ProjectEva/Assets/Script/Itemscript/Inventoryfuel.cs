using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventoryfuel : MonoBehaviour
{
    public Fuelquest fuelquest;
    private void Awake()
    {
        // Find all Walllamblight objects and set their properties
        Fuelquest[] fuelquests = FindObjectsOfType<Fuelquest>();
        foreach (Fuelquest fuelquest in fuelquests)
        {
            fuelquest.isfuelquip = true;
        }
    }

    private void OnDestroy()
    {
        Fuelquest[] fuelquests = FindObjectsOfType<Fuelquest>();
        foreach (Fuelquest fuelquest in fuelquests)
        {
            fuelquest.isfuelquip = false;
        }
    }
}
