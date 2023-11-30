using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Unity.VisualScripting;
namespace Enemy_State
{
[Serializable] 
public class DataSave
{   
    [Header("DataPlayer")]
    public float currentHpPlayer;
    public float transformPlayerX;
    public float transformPlayerY;
    
    [Header("DataEnemy")]
    public float currentHpEnemy;
    public float transformEnemyX;
    public float transformEnemyY;
    public int currentsoundValue;
    public int currentonSoundValuechange;
    public string currentState;

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
}