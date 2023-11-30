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
    [SerializeField] List<DataItemCharactor> listItemsDataCharactors;

    public GameObject player;
    public GameObject enemy;

    [Header("Saving")]
    [SerializeField] string savePath;
    [SerializeField] string saveInventoryPath;


    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<NewMovementPlayer>().gameObject;
        enemy = FindAnyObjectByType<EnemyNormal>().gameObject;
    }
    void ConventDataUIItemtoItemdata()
    {
        listItemsDataCharactors.Clear();
        var listUIItem = FindAnyObjectByType<InventoryPresentCharactor>().uIItemListCharactor;
        foreach (var dataUI in listUIItem)
        {
            DataItemCharactor itemData = new DataItemCharactor();

            itemData.nameItem = dataUI.nameItem.ToString();
            itemData.nameSprite = dataUI.imageItem.sprite != null
            ? dataUI.imageItem.sprite.name : "";

            itemData.scriptItem = dataUI.scriptItem;

            itemData.numslot = dataUI.numslot;
            itemData.count = dataUI.count;
            itemData.maxCount = dataUI.maxCount;

            itemData.isFlashLight = dataUI.isFlashLight;
            itemData.isOnhand = dataUI.isOnhand;
            itemData.isLock = dataUI.isLock;

            listItemsDataCharactors.Add(itemData);
        }

    }
    public void SaveDataInventory()
    {
        ConventDataUIItemtoItemdata();

        if (string.IsNullOrEmpty(saveInventoryPath))
        {
            Debug.LogError("No save path specified");
            return;
        }

        var datainventory = JsonConvert.SerializeObject(listItemsDataCharactors, Formatting.Indented); // Serialize the data with pretty print
        var dataPath = Application.dataPath;
        var targetFilePath = Path.Combine(dataPath, saveInventoryPath);

        var directoryPath = Path.GetDirectoryName(targetFilePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        File.WriteAllText(targetFilePath, datainventory); // Write the JSON string to the file
    }
    public void LoadDataInventory()
    {

        var dataPath = Application.dataPath;
        var targetFilePath = Path.Combine(dataPath, saveInventoryPath);

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
        listItemsDataCharactors = JsonConvert.DeserializeObject<List<DataItemCharactor>>(dataJson);


    }
    public void SaveData()
    {
        SaveDataPlayer();
        SaveDataEnemy();

        if (string.IsNullOrEmpty(savePath))
        {
            Debug.LogError("No save path ja");
            return;
        }

        var data = JsonConvert.SerializeObject(dataSaveandLoad);
        var dataPath = Application.dataPath;
        var targetFilePath = Path.Combine(dataPath, savePath);

        var directoryPath = Path.GetDirectoryName(targetFilePath);
        if (Directory.Exists(directoryPath) == false)
            Directory.CreateDirectory(directoryPath);

        File.WriteAllText(targetFilePath, data);
    }

    public void SaveDataPlayer()
    {
        dataSaveandLoad.currentHpPlayer = player.GetComponentInChildren<Hp>().currenthp;

        dataSaveandLoad.transformPlayerX = player.transform.position.x;
        dataSaveandLoad.transformPlayerY = player.transform.position.y;
    }
    public void SaveDataEnemy()
    {
        var enemyNormal = enemy.GetComponent<EnemyNormal>();
        dataSaveandLoad.currentHpEnemy = enemyNormal.hp;

        dataSaveandLoad.transformEnemyX = enemy.transform.position.x;
        dataSaveandLoad.transformEnemyY = enemy.transform.position.y;

        dataSaveandLoad.currentsoundValue = enemy.GetComponentInChildren<EnemyDetectSound>().currentsoundValue;
        dataSaveandLoad.currentonSoundValuechange = enemyNormal.onSoundValuechange;

        dataSaveandLoad.currentState = CheckedAndSaveStateEnemy(enemyNormal);
    }
    string CheckedAndSaveStateEnemy(Enemy enemy)
    {
        var currentState = enemy.currentState;
        if (currentState == enemy.state_Listening)
            return "state_Listening";
        else if (currentState == enemy.state_Hunting)
            return "state_Hunting";
        else if (currentState == enemy.state_Retreat)
            return "state_Retreat";
        else if (currentState == enemy.state_Searching)
            return "state_Searching";
        else if (currentState == enemy.state_SearchingSound)
            return "state_SearchingSound";
        return null;
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
        LoadDataPlayer();
        LoadDataEnemy();


    }
    public void LoadDataPlayer()
    {
        player.GetComponentInChildren<Hp>().currenthp = dataSaveandLoad.currentHpPlayer;

        Vector2 newPosition = new Vector2(dataSaveandLoad.transformPlayerX, dataSaveandLoad.transformPlayerY);
        player.transform.position = newPosition;
    }

    public void LoadDataEnemy()
    {
        var enemyNormal = enemy.GetComponent<EnemyNormal>();
        enemyNormal.hp = dataSaveandLoad.currentHpEnemy;

        Vector2 newPosition = new Vector2(dataSaveandLoad.transformEnemyX, dataSaveandLoad.transformEnemyY);
        enemy.transform.position = newPosition;

        enemy.GetComponentInChildren<EnemyDetectSound>().currentsoundValue = dataSaveandLoad.currentsoundValue;
        enemyNormal.onSoundValuechange = dataSaveandLoad.currentonSoundValuechange;

        enemy.GetComponent<EnemyNormal>().currentState = LoadStateEnemy(enemyNormal);

    }
    StateMachine LoadStateEnemy(Enemy enemy)
    {
        if (dataSaveandLoad.currentState == "state_Listening")
            return enemy.state_Listening;
        else if (dataSaveandLoad.currentState == "state_Hunting")
            return enemy.state_Hunting;
        else if (dataSaveandLoad.currentState == "state_Retreat")
            return enemy.state_Retreat;
        else if (dataSaveandLoad.currentState == "state_Searching")
            return enemy.state_Searching;
        else if (dataSaveandLoad.currentState == "state_SearchingSound")
            return enemy.state_SearchingSound;
        return null;
    }
}
