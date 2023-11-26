using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Enemy_State;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;


public class SaveManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] DataSave dataSaveandLoad;   

    public GameObject player;
    public GameObject enemy;
    
    [Header("Saving")]
    [SerializeField] string savePath;


    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<NewMovementPlayer>().gameObject;
        enemy = FindAnyObjectByType<EnemyNormal>().gameObject;
    }
    public void SaveData()
    {   
        if (string.IsNullOrEmpty(savePath))
        {
            Debug.LogError("No save path ja");
            return;
        }

        var scoreJson = JsonConvert.SerializeObject(dataSaveandLoad);
        var dataPath = Application.dataPath;
        var targetFilePath = Path.Combine(dataPath, savePath);

        var directoryPath = Path.GetDirectoryName(targetFilePath);
        if (Directory.Exists(directoryPath) == false)
            Directory.CreateDirectory(directoryPath);

        File.WriteAllText(targetFilePath, scoreJson);
    }
    public void SaveDataPlayer()
    {   
        dataSaveandLoad.currentHpPlayer = player.GetComponentInChildren<Hp>().currenthp;
        
        dataSaveandLoad.transformPlayerX = player.transform.position.x;
        dataSaveandLoad.transformPlayerY = player.transform.position.y;
    }
    public void SaveDataEnemy()
    {
        dataSaveandLoad.currentHpEnemy = enemy.GetComponent<EnemyNormal>().hp;

        dataSaveandLoad.transformEnemyX = enemy.transform.position.x;
        dataSaveandLoad.transformEnemyY = enemy.transform.position.y;
    }
    public void LoadData()
    {
        var dataPath = Application.dataPath;
        var targetFilePath = Path.Combine(dataPath, savePath);

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
        dataSaveandLoad = JsonConvert.DeserializeObject<DataSave>(dataJson);

       
    }
    public void LoadDataPlayer()
    {
        player.GetComponentInChildren<Hp>().currenthp = dataSaveandLoad.currentHpPlayer;

        Vector2 newPosition = new Vector2(dataSaveandLoad.transformPlayerX, dataSaveandLoad.transformPlayerY);
        player.transform.position = newPosition;
    }

    public void LoadDataEnemy()
    {
        enemy.GetComponent<EnemyNormal>().hp = dataSaveandLoad.currentHpEnemy;

        Vector2 newPosition = new Vector2(dataSaveandLoad.transformEnemyX,dataSaveandLoad.transformEnemyY);
        enemy.transform.position = newPosition;

        
    }
}
