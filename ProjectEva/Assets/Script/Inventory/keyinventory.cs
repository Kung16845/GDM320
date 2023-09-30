using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyinventory : MonoBehaviour
{
    private List<string> keys; // List to store collected keys.

    void Start()
    {
        keys = new List<string>();
    }

    // Method to add a key to the inventory.
    public void AddKey(string keyName)
    {
        keys.Add(keyName);
        Debug.Log("Collected key: " + keyName);
    }

    // Method to check if a specific key is in the inventory.
    public bool HasKey(string keyName)
    {
        return keys.Contains(keyName);
    }

    // Method to remove a key from the inventory.
    public void RemoveKey(string keyName)
    {
        if (keys.Contains(keyName))
        {
            keys.Remove(keyName);
            Debug.Log("Used key: " + keyName);
        }
        else
        {
            Debug.LogWarning("Key not found in inventory: " + keyName);
        }
    }
}
