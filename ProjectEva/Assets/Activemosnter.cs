using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Activemosnter : MonoBehaviour
{
    public GameObject activemonster;
    public int PrefabID;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            activemonster.SetActive(true);
            Destroy(this.gameObject);
        }
    }
    public void DataCheckActive()
    {
        var dataScean = FindObjectOfType<SaveAndLoadScean>();
        var dataobj = dataScean.dataObjectInSceans.FirstOrDefault(objid => objid.objectID == PrefabID);
        dataobj.isNotActiveInSceans = false;
    }
}
