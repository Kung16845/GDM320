using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dinnerkeyinventory : MonoBehaviour
{
    private void Awake()
    {
        KeyonlyDoor[] keyonlyDoors = FindObjectsOfType<KeyonlyDoor>();
        foreach (KeyonlyDoor keyonlyDoor in keyonlyDoors)
        {
            keyonlyDoor.hasKeynumber = 2;
        }
        ForceandkeyDoor[] forceandkeyDoors = FindObjectsOfType<ForceandkeyDoor>();
        foreach (ForceandkeyDoor forceandkeyDoor in forceandkeyDoors)
        {
            forceandkeyDoor.hasKeynumber = 2;
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
