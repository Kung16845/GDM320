using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScean : MonoBehaviour
{
    public List<GameObject> doorObjects; // This should be assigned in the Inspector
    public Transform positioncreate;
    private void Start()
    {
        foreach (var objectsdoor in doorObjects)
        {
            if (objectsdoor != null)
            {
                GameObject instantiatedTilemap = Instantiate(objectsdoor, positioncreate);
                // instantiatedTilemap.transform.position = new Vector3(0f, 0f, 0f);
            }
            else
            {
                Debug.LogError("doorObject is not assigned in the Inspector!");
            }
        }
    }
}
