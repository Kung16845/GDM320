using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventoryfuse : MonoBehaviour
{
    // Start is called before the first frame update
    public Fusequest fusequest;
    private void Awake()
    {
        // Find all Walllamblight objects and set their properties
        Fusequest[] fusequests = FindObjectsOfType<Fusequest>();
        foreach (Fusequest fusequest in fusequests)
        {
            fusequest.isfusequip = true;
        }
    }

    private void OnDestroy()
    {
        Fusequest[] fusequests = FindObjectsOfType<Fusequest>();
        foreach (Fusequest fusequest in fusequests)
        {
            fusequest.isfusequip = false;
        }
    }
}
