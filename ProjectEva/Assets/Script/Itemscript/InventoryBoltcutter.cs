using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryBoltcutter : MonoBehaviour
{
    private void Awake()
    {
        KeyonlyDoor[] keyonlyDoors = FindObjectsOfType<KeyonlyDoor>();
        foreach (KeyonlyDoor keyonlyDoor in keyonlyDoors)
        {
            keyonlyDoor.hasKeynumber = 4;
        }
        ForceandkeyDoor[] forceandkeyDoors = FindObjectsOfType<ForceandkeyDoor>();
        foreach (ForceandkeyDoor forceandkeyDoor in forceandkeyDoors)
        {
            forceandkeyDoor.hasKeynumber = 4;
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
