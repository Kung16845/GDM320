using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Enemy_State;
using NavMeshPlus.Components;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Networking;
using UnityEngine.U2D;
public class SaveAndLoadScean : MonoBehaviour
{
    [Header("DataObject")]
    [SerializeField] public List<DataObjectLoad> dataObjectInSceans;
    public List<ObjectPrefabLoad> objectforload;

    [Header("Saving")]
    [SerializeField] string saveDataObjects;
    public NavMeshSurface navMeshSurface;
    public void LoadObjectToStartScean()
    {

        foreach (var objInScean in objectforload)
        {
            if (!objInScean.isDestroy)
            {
                var newObject = Instantiate(objInScean.objectinScean, this.transform);

                var newDataObj = new DataObjectLoad();

                newDataObj.objectID = objInScean.objectID;
                newDataObj.isDestroy = objInScean.isDestroy;

                if (!newObject.activeInHierarchy) // เช็คกับดัก 
                    newDataObj.isNotActiveInSceans = true;

                dataObjectInSceans.Add(newDataObj);
            }
        }
        navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
    }
    public void SaveObjectIsDestroy()
    {
        foreach (var objInScean in objectforload)
        {
            if (objInScean.isDestroy)
            {
                var dataObj = dataObjectInSceans.FirstOrDefault(objId => objId.objectID == objInScean.objectID);
                dataObj.isDestroy = true;
            }
        }
    }
    public void SaveDataObjectINScean()
    {
        SaveObjectIsDestroy();

        if (string.IsNullOrEmpty(saveDataObjects))
        {
            Debug.LogError("No save path specified");
            return;
        }

        var dataObjectInSceans = JsonConvert.SerializeObject(this.dataObjectInSceans, Formatting.Indented); // Serialize the data with pretty print
        var dataPath = Application.dataPath;
        var targetFilePath = Path.Combine(dataPath, saveDataObjects);

        var directoryPath = Path.GetDirectoryName(targetFilePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        File.WriteAllText(targetFilePath, dataObjectInSceans);
    }
    public void LoadDataObjectINScean()
    {
        dataObjectInSceans.Clear();
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        var dataPath = Application.dataPath;
        var targetFilePath = Path.Combine(dataPath, saveDataObjects);

        var directoryPath = Path.GetDirectoryName(targetFilePath);

        if (Directory.Exists(directoryPath) == false)
        {
            Debug.LogError("No save folder at provided path");
            return;
        }

        if (File.Exists(targetFilePath) == false)
        {
            Debug.LogError("No save file at provided path");
            return;
        }

        var dataJson = File.ReadAllText(targetFilePath);
        dataObjectInSceans = JsonConvert.DeserializeObject<List<DataObjectLoad>>(dataJson);
        LoadSceanFromDataJson();
    }
    public void LoadSceanFromDataJson()
    {
        foreach (var dataobj in dataObjectInSceans)
        {
            if (!dataobj.isDestroy)
            {
                var objInScean = objectforload.FirstOrDefault(objid => objid.objectID == dataobj.objectID);
                var loadObj = Instantiate(objInScean.objectinScean, this.transform);
                if (!dataobj.isNotActiveInSceans)
                {
                    loadObj.SetActive(true);
                }
            }
        }
    }
}
[Serializable]
public class ObjectPrefabLoad
{
    public int objectID;
    public bool isDestroy;
    public string nameObjects;
    public GameObject objectinScean;
}