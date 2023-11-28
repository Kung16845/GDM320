using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

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

    [Header("DataInventoryItemCharactor")]

    public string nameItem;
}