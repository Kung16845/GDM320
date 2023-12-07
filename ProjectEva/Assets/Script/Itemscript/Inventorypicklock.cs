using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventorypicklock : MonoBehaviour
{
    private void Awake()
    {
        LockerItemSpawner[] LockerItemSpawners = FindObjectsOfType<LockerItemSpawner>();
        foreach (LockerItemSpawner LockerItemSpawner in LockerItemSpawners)
        {
            LockerItemSpawner.canunlock = true;
        }
    }
    private void OnDestroy() 
    {
        LockerItemSpawner[] LockerItemSpawners = FindObjectsOfType<LockerItemSpawner>();
        foreach (LockerItemSpawner LockerItemSpawner in LockerItemSpawners)
        {
            LockerItemSpawner.canunlock = false;
        }
    }
}
