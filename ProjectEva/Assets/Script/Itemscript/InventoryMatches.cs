using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryMatches : MonoBehaviour
{
    public Webtrap webtrap;
    private void Awake() 
    {
        webtrap = FindAnyObjectByType<Webtrap>();
        webtrap.hasmatches = true;
    }
    private void OnDestroy() 
    {
    webtrap.hasmatches = false;
    }
}
