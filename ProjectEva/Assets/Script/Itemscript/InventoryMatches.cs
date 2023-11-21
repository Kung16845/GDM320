using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMatches : MonoBehaviour
{
    private void Awake()
    {
        // Find all Walllamblight objects and set their properties
        Walllamblight[] wallamblights = FindObjectsOfType<Walllamblight>();
        foreach (Walllamblight wallamblight in wallamblights)
        {
            wallamblight.lightquip = true;
        }

        // Find all Webtrap objects and set their properties
        Webtrap[] webtraps = FindObjectsOfType<Webtrap>();
        foreach (Webtrap webtrap in webtraps)
        {
            webtrap.Canburn = true;
        }
    }

    private void OnDestroy()
    {
        // Find all Walllamblight objects and reset their properties
        Walllamblight[] wallamblights = FindObjectsOfType<Walllamblight>();
        foreach (Walllamblight wallamblight in wallamblights)
        {
            wallamblight.lightquip = false;
        }

        // Find all Webtrap objects and reset their properties
        Webtrap[] webtraps = FindObjectsOfType<Webtrap>();
        foreach (Webtrap webtrap in webtraps)
        {
            webtrap.Canburn = false;
        }
    }
}
