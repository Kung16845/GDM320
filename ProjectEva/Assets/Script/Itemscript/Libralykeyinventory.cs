using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Libralykeyinventory : MonoBehaviour
{
    private void Awake()
    {
        KeyonlyDoor[] keyonlyDoors = FindObjectsOfType<KeyonlyDoor>();
        foreach (KeyonlyDoor keyonlyDoor in keyonlyDoors)
        {
            keyonlyDoor.hasKeynumber = 1;
        }
        ForceandkeyDoor[] forceandkeyDoors = FindObjectsOfType<ForceandkeyDoor>();
        foreach (ForceandkeyDoor forceandkeyDoor in forceandkeyDoors)
        {
            forceandkeyDoor.hasKeynumber = 1;
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
