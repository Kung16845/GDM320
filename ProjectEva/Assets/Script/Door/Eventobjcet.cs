using UnityEngine;
using TMPro;
using System.Linq;
using NavMeshPlus.Components;

public class Eventobjcet : MonoBehaviour
{
    public int PrefabID;
    private void OnDestroy()
    {
        var datainScean = FindAnyObjectByType<SaveAndLoadScean>();
        var dataobj = datainScean.objectforload.FirstOrDefault(objid => objid.objectID == PrefabID);
        dataobj.isDestroy = true;
    }
}
