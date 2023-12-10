using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenroyDiamondkey : MonoBehaviour
{
    private void Awake()
    {
        KeyonlyDoor[] keyonlyDoors = FindObjectsOfType<KeyonlyDoor>();
        foreach (KeyonlyDoor keyonlyDoor in keyonlyDoors)
        {
            keyonlyDoor.hasKeynumber = 3;
        }
        ForceandkeyDoor[] forceandkeyDoors = FindObjectsOfType<ForceandkeyDoor>();
        foreach (ForceandkeyDoor forceandkeyDoor in forceandkeyDoors)
        {
            forceandkeyDoor.hasKeynumber = 3;
        }
    }
    private void OnDestroy() 
    {
        KeyonlyDoor[] keyonlyDoors = FindObjectsOfType<KeyonlyDoor>();
        foreach (KeyonlyDoor keyonlyDoor in keyonlyDoors)
        {
                keyonlyDoor.hasKeynumber = 0;
        }
        ForceandkeyDoor[] forceandkeyDoors = FindObjectsOfType<ForceandkeyDoor>();
        foreach (ForceandkeyDoor forceandkeyDoor in forceandkeyDoors)
        {
                forceandkeyDoor.hasKeynumber = 0;
        }
    }
}
