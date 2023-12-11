using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting;

[Serializable] 
public class DataSave
{   
    [Header("DataPlayer")]
    public float currentHpPlayer;
    public float currentSanityPlayer;
    public float currentSanityResistance;
    public float transformPlayerX;
    public float transformPlayerY;
    public int currentPistolAmmoinChamber;
    public int currentShotgunAmmoinChamber;
    [Header("DataEnemy")]
    public float currentHpEnemy;
    public float transformEnemyX;
    public float transformEnemyY;
    public int currentsoundValue;
    public int currentonSoundValuechange;
    public bool enemyIsActiveInScean;
    public string currentState;
    [Header("DataSystemInventory")]
    public string[] lockerItemKey;
}

[Serializable]
public class DataItemCharactor
{
    public string nameItem;
    public string nameSprite;
    public string scriptItem;
    public int numslot;
    public int count;
    public int maxCount;
    public bool isFlashLight;
    public bool isOnhand;
    public bool isLock;
    
}
[Serializable]
public class DataItemNote
{
    public string nameItem;
    public string detailsItemNote;
    public string nameSprite;
    public Category category;
}

[Serializable]
public class DataObjectLoad
{   
    public int objectID; 
    public bool isNotActiveInSceans;
    public bool isDestroy;
}
