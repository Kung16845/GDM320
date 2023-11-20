using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Libralykeyinventory : MonoBehaviour
{
    public KeyonlyDoor keyonlyDoor;
    public ForceandkeyDoor forceandkeyDoor;
    private void Awake()
    {
        keyonlyDoor = FindAnyObjectByType<KeyonlyDoor>();
        forceandkeyDoor = FindAnyObjectByType<ForceandkeyDoor>();
        forceandkeyDoor.hasLibraryKey = true;
        keyonlyDoor.hasLibraryKey = true;
        gameObject.name = "Libralykey";
    }
    private void OnDestroy() 
    {
    // ทำงานหลังจาก Object ถูกทำลาย
        forceandkeyDoor.hasLibraryKey = false;
        keyonlyDoor.hasLibraryKey = false;
    }
}
