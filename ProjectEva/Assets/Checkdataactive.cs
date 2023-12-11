using UnityEngine;
using TMPro;
using System.Linq;
using NavMeshPlus.Components;

public class Checkdataactive : MonoBehaviour
{
    public int PrefabID;
    public void DataCheckActive()
    {
        var dataScean = FindObjectOfType<SaveAndLoadScean>();
        var dataobj = dataScean.dataObjectInSceans.FirstOrDefault(objid => objid.objectID == PrefabID);
        dataobj.isNotActiveInSceans = false;
    }
}
