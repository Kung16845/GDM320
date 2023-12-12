using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Enemy_State;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.U2D;
using UnityEngine.SceneManagement;
public class SaveManager : MonoBehaviour
{
    [Header("DataPlayer")]
    [SerializeField] DataSave dataSaveandLoadPlayerAndEnemy;
    public GameObject player;
    public GameObject enemy;

    [Header("DataItemCharactor")]
    [SerializeField] List<DataItemCharactor> listDataItemsCharactors;
    public List<GameObject> allslot;
    public SpriteAtlas spriteAtlas;

    [Header("DataItemNote")]
    [SerializeField] List<DataItemNote> listDataItemsNotes;


    [Header("Saving")]
    [SerializeField] string savePlayerAndEnemyPath;
    [SerializeField] string saveInventoryItemsChractorPath;
    [SerializeField] string saveInventoryItemsNotePath;

    public SaveAndLoadScean saveAndLoadScean;
    public LoadScene loadScene;
    // Start is called before the first frame update
    void Start()
    {
        player = FindAnyObjectByType<NewMovementPlayer>().gameObject;
        saveAndLoadScean = FindObjectOfType<SaveAndLoadScean>();
        loadScene = FindObjectOfType<LoadScene>();
        FindInactiveEnemyNormals();
        
        if(loadScene.isNewScene)
            saveAndLoadScean.LoadObjectToStartScean();
        else if(loadScene.isLoadScene)
            AllLoad();
        
        saveAndLoadScean.navMeshSurface.UpdateNavMesh(saveAndLoadScean.navMeshSurface.navMeshData);
    }
    private void FindInactiveEnemyNormals()
    {
        // หาทุก Object ที่มี script EnemyNormal ในฉาก
        EnemyNormal[] enemyNormals = GameObject.FindObjectsOfType<EnemyNormal>(true);

        foreach (EnemyNormal enemyNormal in enemyNormals)
        {
            // ตรวจสอบว่า Object นี้ไม่ได้ Active
            if (!enemyNormal.gameObject.activeSelf)
            {
                this.enemy = enemyNormal.gameObject;

            }
        }
    }
    public void AllSave()
    {
        SaveDataPlayerAndEnemy();
        SaveDataInventoryItemsNote();
        SaveDataInventoryItemsChractor();
        saveAndLoadScean.SaveDataObjectINScean();
    }
    public void AllLoadDead()
    {   
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex );  
        Debug.Log("Load Data SceanDie");
        Invoke("AllLoad",2f); 
    }
    public void AllLoad()
    {   
        Debug.Log("Load Data Scean");
        LoadDataPlayerAndEnemy();
        LoadDataInventoryItemNote();
        LoadDataInventoryItemsChractor();
        saveAndLoadScean.LoadDataObjectINScean();
    }
    void ConventUIItemsNoteToDataItemsNotes()
    {
        listDataItemsNotes.Clear();
        var inventoryItemNotePresent = FindAnyObjectByType<InventoryItemNotePresent>();
        var listdataItemNote = inventoryItemNotePresent.itemsListNotes;

        foreach (var dataNote in listdataItemNote)
        {
            DataItemNote dataItem = new DataItemNote();

            dataItem.nameItem = dataNote.nameItemNote;
            dataItem.detailsItemNote = dataNote.detailsItemNote;

            dataItem.nameSprite = dataNote.image != null ? dataNote.image.name : " ";

            dataItem.category = dataNote.type;

            listDataItemsNotes.Add(dataItem);
        }
    }
    public void SaveDataInventoryItemsNote()
    {
        ConventUIItemsNoteToDataItemsNotes();

        if (string.IsNullOrEmpty(saveInventoryItemsNotePath))
        {
            Debug.LogError("No save path specified");
            return;
        }

        var datainventory = JsonConvert.SerializeObject(listDataItemsNotes, Formatting.Indented); // Serialize the data with pretty print
        var dataPath = Application.dataPath;
        var targetFilePath = Path.Combine(dataPath, saveInventoryItemsNotePath);

        var directoryPath = Path.GetDirectoryName(targetFilePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        File.WriteAllText(targetFilePath, datainventory);
    }
    public void LoadDataInventoryItemNote()
    {

        var dataPath = Application.dataPath;
        var targetFilePath = Path.Combine(dataPath, saveInventoryItemsNotePath);

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
        listDataItemsNotes = JsonConvert.DeserializeObject<List<DataItemNote>>(dataJson);

        UpLoadAndCreateDataInventoryItemNote();

    }
    public void UpLoadAndCreateDataInventoryItemNote()
    {

        var inventoryItemNote = FindAnyObjectByType<InventoryItemNotePresent>();
        var dataListItemNote = inventoryItemNote.itemsListNotes;
        dataListItemNote.Clear();

        for (int i = 0; i < listDataItemsNotes.Count; i++)
        {
            var dataItemNote = listDataItemsNotes.ElementAt<DataItemNote>(i);

            var newDataItemsNote = new ItemDataNote();

            newDataItemsNote.nameItemNote = dataItemNote.nameItem;
            newDataItemsNote.detailsItemNote = dataItemNote.detailsItemNote;

            newDataItemsNote.image = GetSpriteByName(dataItemNote.nameSprite);

            newDataItemsNote.type = dataItemNote.category;

            dataListItemNote.Add(newDataItemsNote);
        }

        // inventoryItemNote.RefreshUIInventoryItenNote();
    }
    void ConventDataUIItemCharactortoDataItemCharactor()
    {
        listDataItemsCharactors.Clear();
        var listUIItem = FindAnyObjectByType<InventoryPresentCharactor>().uIItemListCharactor;
        foreach (var dataUI in listUIItem)
        {
            DataItemCharactor itemData = new DataItemCharactor();

            itemData.nameItem = dataUI.nameItem.text;
            itemData.nameSprite = dataUI.imageItem.sprite != null
            ? dataUI.imageItem.sprite.name : "";

            itemData.scriptItem = dataUI.scriptItem;

            itemData.numslot = dataUI.numslot;
            itemData.count = dataUI.count;
            itemData.maxCount = dataUI.maxCount;

            itemData.isFlashLight = dataUI.isFlashLight;
            itemData.isOnhand = dataUI.isOnhand;
            itemData.isLock = dataUI.isLock;

            listDataItemsCharactors.Add(itemData);
        }

    }

    public void SaveDataInventoryItemsChractor()
    {
        ConventDataUIItemCharactortoDataItemCharactor();

        if (string.IsNullOrEmpty(saveInventoryItemsChractorPath))
        {
            Debug.LogError("No save path specified");
            return;
        }

        var datainventory = JsonConvert.SerializeObject(listDataItemsCharactors, Formatting.Indented); // Serialize the data with pretty print
        var dataPath = Application.dataPath;
        var targetFilePath = Path.Combine(dataPath, saveInventoryItemsChractorPath);

        var directoryPath = Path.GetDirectoryName(targetFilePath);
        if (!Directory.Exists(directoryPath))
        {
            Directory.CreateDirectory(directoryPath);
        }

        File.WriteAllText(targetFilePath, datainventory); // Write the JSON string to the file
    }

    public void LoadDataInventoryItemsChractor()
    {
        ClearUIandGameObjectInventoryItemCharactor();

        var dataPath = Application.dataPath;
        var targetFilePath = Path.Combine(dataPath, saveInventoryItemsChractorPath);

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
        listDataItemsCharactors = JsonConvert.DeserializeObject<List<DataItemCharactor>>(dataJson);

        UpLoadAndCreateDataInventoryItemCharactor();
    }
    public void ClearUIandGameObjectInventoryItemCharactor()
    {
        var inventoryPresentCharactor = FindAnyObjectByType<InventoryPresentCharactor>();
        var listUIItem = inventoryPresentCharactor.uIItemListCharactor;
        listUIItem.Clear();
        foreach (var itemInSlot in allslot)
        {
            var uiItemComponent = itemInSlot.GetComponentInChildren<UIItemCharactor>();
            if (uiItemComponent != null)
            {
                Destroy(uiItemComponent.gameObject);
            }
            if (itemInSlot.GetComponent<BoxItemsCharactor>() != null)
            {
                foreach (Transform child in itemInSlot.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }
   public void UpLoadAndCreateDataInventoryItemCharactor()
{
    var inventoryPresentCharactor = FindAnyObjectByType<InventoryPresentCharactor>();

    for (int i = 0; i < listDataItemsCharactors.Count; i++)
    {
        var itemDataCharactor = listDataItemsCharactors.ElementAt<DataItemCharactor>(i);

        // Use FirstOrDefault to find the slotItem
        var slotItem = allslot.FirstOrDefault(
            slot => slot.GetComponent<InventorySlots>()?.numslot == itemDataCharactor.numslot ||
                    slot.GetComponent<BoxItemsCharactor>()?.numslot == itemDataCharactor.numslot);

        // Check if slotItem is null before accessing its properties
        if (slotItem != null)
        {
            var newItemCharactor = Instantiate(inventoryPresentCharactor.uIItemCharactorPrefeb, slotItem.transform, false);
            var newItem = newItemCharactor.GetComponent<UIItemCharactor>();

            newItem.nameItem.text = itemDataCharactor.nameItem;
            newItem.scriptItem = itemDataCharactor.scriptItem;

            newItem.numslot = itemDataCharactor.numslot;
            newItem.count = itemDataCharactor.count;
            newItem.maxCount = itemDataCharactor.maxCount;
            newItem.RefrehCount();

            newItem.isFlashLight = itemDataCharactor.isFlashLight;
            newItem.isOnhand = itemDataCharactor.isOnhand;
            newItem.isLock = itemDataCharactor.isLock;

            if (newItem.isLock)
                newItem.imageItemLock.gameObject.SetActive(true);
            else
                newItem.imageItem.sprite = GetSpriteByName(itemDataCharactor.nameSprite);

            newItem.gameObject.SetActive(true);

            if (newItem.numslot == 12 || newItem.numslot == 13)
            {
                inventoryPresentCharactor.CreateItemCharactorEquipment(newItem.scriptItem, newItem.nameItem.text);
                Vector3 newScaleequipment = new Vector3(1.65f, 1.65f, 1.65f);
                newItem.GetComponent<RectTransform>().localScale = newScaleequipment;
            }

            inventoryPresentCharactor.uIItemListCharactor.Add(newItem);
        }
    }
}

    public string CheckReMoveClone(string nameSpriteItem)
    {
        string originalString = nameSpriteItem;
        string termToRemove = "(Clone)";

        if (originalString.Contains(termToRemove))
        {
            int index = originalString.IndexOf(termToRemove);
            string modifiedString = originalString.Substring(0, index) + originalString.Substring(index + termToRemove.Length);

            nameSpriteItem = modifiedString;
        }
        return nameSpriteItem;
    }
    public Sprite GetSpriteByName(string spriteName)
    {
        spriteName = CheckReMoveClone(spriteName);
        if (spriteAtlas != null && spriteAtlas.GetSprite(spriteName) != null)
        {
            return spriteAtlas.GetSprite(spriteName);
        }
        else
        {
            Debug.LogError("Sprite not found in SpriteAtlas: " + spriteName);
            return null;
        }
    }
    public void SaveDataPlayerAndEnemy()
    {
        SaveDataPlayer();
        SaveDataEnemy();

        if (string.IsNullOrEmpty(savePlayerAndEnemyPath))
        {
            Debug.LogError("No save path ja");
            return;
        }

        var data = JsonConvert.SerializeObject(dataSaveandLoadPlayerAndEnemy);
        var dataPath = Application.dataPath;
        var targetFilePath = Path.Combine(dataPath, savePlayerAndEnemyPath);

        var directoryPath = Path.GetDirectoryName(targetFilePath);
        if (Directory.Exists(directoryPath) == false)
            Directory.CreateDirectory(directoryPath);

        File.WriteAllText(targetFilePath, data);
    }

    public void SaveDataPlayer()
    {

        dataSaveandLoadPlayerAndEnemy.currentHpPlayer = player.GetComponentInChildren<Hp>().currenthp;

        var playerSanity = player.GetComponentInChildren<Sanity>();
        dataSaveandLoadPlayerAndEnemy.currentSanityPlayer = playerSanity.currentsanity;
        dataSaveandLoadPlayerAndEnemy.currentSanityResistance = playerSanity.SanityResistance;

        dataSaveandLoadPlayerAndEnemy.currentPistolAmmoinChamber = player.GetComponentInChildren<Pistol>().ammoInChamber;
        dataSaveandLoadPlayerAndEnemy.currentShotgunAmmoinChamber = player.GetComponentInChildren<Shotgun>().ammoInChamber;

        dataSaveandLoadPlayerAndEnemy.transformPlayerX = player.transform.position.x;
        dataSaveandLoadPlayerAndEnemy.transformPlayerY = player.transform.position.y;

        var lockerArray = FindObjectOfType<RandomItemGenerator>();

        dataSaveandLoadPlayerAndEnemy.lockerItemKey = lockerArray.generatedSequence;
    }
    public void SaveDataEnemy()
    {
        var enemyNormal = enemy.GetComponent<EnemyNormal>();
        dataSaveandLoadPlayerAndEnemy.currentHpEnemy = enemyNormal.hp;

        dataSaveandLoadPlayerAndEnemy.transformEnemyX = enemy.transform.position.x;
        dataSaveandLoadPlayerAndEnemy.transformEnemyY = enemy.transform.position.y;

        dataSaveandLoadPlayerAndEnemy.currentsoundValue = enemy.GetComponentInChildren<EnemyDetectSound>().currentsoundValue;
        dataSaveandLoadPlayerAndEnemy.currentonSoundValuechange = enemyNormal.onSoundValuechange;

        dataSaveandLoadPlayerAndEnemy.currentState = CheckedAndSaveStateEnemy(enemyNormal);

        if (enemy.activeInHierarchy)
            dataSaveandLoadPlayerAndEnemy.enemyIsActiveInScean = true;
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

    public void LoadDataPlayerAndEnemy()
    {

        var dataPath = Application.dataPath;
        var targetFilePath = Path.Combine(dataPath, savePlayerAndEnemyPath);

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
        dataSaveandLoadPlayerAndEnemy = JsonConvert.DeserializeObject<DataSave>(dataJson);
        LoadDataPlayer();
        LoadDataEnemy();


    }
    public void LoadDataPlayer()
    {

        player.GetComponentInChildren<Hp>().currenthp = dataSaveandLoadPlayerAndEnemy.currentHpPlayer;

        player.GetComponentInChildren<Pistol>().ammoInChamber = dataSaveandLoadPlayerAndEnemy.currentPistolAmmoinChamber;
        player.GetComponentInChildren<Shotgun>().ammoInChamber = dataSaveandLoadPlayerAndEnemy.currentShotgunAmmoinChamber;

        Vector2 newPosition = new Vector2(dataSaveandLoadPlayerAndEnemy.transformPlayerX, dataSaveandLoadPlayerAndEnemy.transformPlayerY);
        player.transform.position = newPosition;


        var lockerArray = FindObjectOfType<RandomItemGenerator>();

        lockerArray.generatedSequence = dataSaveandLoadPlayerAndEnemy.lockerItemKey;
    }

    public void LoadDataEnemy()
    {
        var enemyNormal = enemy.GetComponent<EnemyNormal>();
        enemyNormal.hp = dataSaveandLoadPlayerAndEnemy.currentHpEnemy;

        Vector2 newPosition = new Vector2(dataSaveandLoadPlayerAndEnemy.transformEnemyX, dataSaveandLoadPlayerAndEnemy.transformEnemyY);
        enemy.transform.position = newPosition;

        enemy.GetComponentInChildren<EnemyDetectSound>().currentsoundValue = dataSaveandLoadPlayerAndEnemy.currentsoundValue;
        enemyNormal.onSoundValuechange = dataSaveandLoadPlayerAndEnemy.currentonSoundValuechange;

        enemy.GetComponent<EnemyNormal>().currentState = LoadStateEnemy(enemyNormal);

        if (dataSaveandLoadPlayerAndEnemy.enemyIsActiveInScean)
            enemy.SetActive(true);
        else
            enemy.SetActive(false);

    }
    StateMachine LoadStateEnemy(Enemy enemy)
    {
        if (dataSaveandLoadPlayerAndEnemy.currentState == "state_Listening")
            return enemy.state_Listening;
        else if (dataSaveandLoadPlayerAndEnemy.currentState == "state_Hunting")
            return enemy.state_Hunting;
        else if (dataSaveandLoadPlayerAndEnemy.currentState == "state_Retreat")
            return enemy.state_Retreat;
        else if (dataSaveandLoadPlayerAndEnemy.currentState == "state_Searching")
            return enemy.state_Searching;
        else if (dataSaveandLoadPlayerAndEnemy.currentState == "state_SearchingSound")
            return enemy.state_SearchingSound;
        return null;
    }
}
