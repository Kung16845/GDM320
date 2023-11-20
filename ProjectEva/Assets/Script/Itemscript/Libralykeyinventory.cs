using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Libralykeyinventory : MonoBehaviour
{
    public KeyonlyDoor keyonlyDoor;
    private void Awake()
    {
        keyonlyDoor = FindAnyObjectByType<KeyonlyDoor>();
        keyonlyDoor.hasLibraryKey = true;
        gameObject.name = "Libralykey";
    }
    private void OnDestroy() 
    {
    // ทำงานหลังจาก Object ถูกทำลาย
        keyonlyDoor.hasLibraryKey = false;
    }
}
