using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventorywinefilled : MonoBehaviour
{
    public Pianoquest pianoquest;
    void Awake()
    {
        pianoquest = FindObjectOfType<Pianoquest>();
        pianoquest.isbottlewineequip = true;
    }
    void OnDestroy()
    {
        pianoquest.isbottlewineequip = false;
    }
}
