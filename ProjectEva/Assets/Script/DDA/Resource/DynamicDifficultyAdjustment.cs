using UnityEngine;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class DynamicDifficultyAdjustment : MonoBehaviour
{
    public InventoryPresentCharactor inventory;

    [System.Serializable]
    public enum ItemType
    {
        Action,
        Medicine,
        Utility
    }

    [System.Serializable]
    public class ItemDifficulty
    {
        public string itemName;
        public ItemType itemType; // Add the ItemType field
        public int actionPoints;
        public int medicinePoints;
        public int utilityPoints;
    }

    public List<ItemDifficulty> itemsToTrack = new List<ItemDifficulty>();
    public int minActionPointsForHighDifficulty = 15;
    public int maxActionPointsForLowDifficulty = 5;

    private void Start()
    {
        if (inventory == null)
        {
            Debug.LogError("Inventory reference not set in DynamicDifficultyAdjustment script!");
        }
    }

    private void Update()
    {
        int totalActionPoints = CalculateTotalActionPoints();
        int totalMedicinePoints = CalculateTotalMedicinePoints();
        int totalUtilityPoints = CalculateTotalUtilityPoints();

        // AdjustDifficulty(totalActionPoints);
    }

    public int CalculateTotalActionPoints()
    {
        int totalActionPoints = 0;

        foreach (var item in itemsToTrack)
        {
            int itemCount = inventory.GetTotalItemCountByName(item.itemName);
            totalActionPoints += itemCount * item.actionPoints;
        }

        return totalActionPoints;
    }

    public int CalculateTotalMedicinePoints()
    {
        int totalMedicinePoints = 0;

        foreach (var item in itemsToTrack)
        {
            int itemCount = inventory.GetTotalItemCountByName(item.itemName);
            totalMedicinePoints += itemCount * item.medicinePoints;
        }

        return totalMedicinePoints;
    }

    public int CalculateTotalUtilityPoints()
    {
        int totalUtilityPoints = 0;

        foreach (var item in itemsToTrack)
        {
            int itemCount = inventory.GetTotalItemCountByName(item.itemName);
            totalUtilityPoints += itemCount * item.utilityPoints;
        }

        return totalUtilityPoints;
    }

    // private void AdjustDifficulty(int totalActionPoints)
    // {
    //     if (totalActionPoints >= minActionPointsForHighDifficulty)
    //     {
            
    //     }
    //     else if (totalActionPoints <= maxActionPointsForLowDifficulty)
    //     {
    //     }
    //     else
    //     {
    //     }
    // }
}

#if UNITY_EDITOR
[CustomEditor(typeof(DynamicDifficultyAdjustment))]
public class DynamicDifficultyAdjustmentEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DynamicDifficultyAdjustment script = (DynamicDifficultyAdjustment)target;

        // Draw the default inspector
        DrawDefaultInspector();

        // Draw custom inspector for itemsToTrack list
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Customize Points for Each Item", EditorStyles.boldLabel);

        for (int i = 0; i < script.itemsToTrack.Count; i++)
        {
            script.itemsToTrack[i].itemName = EditorGUILayout.TextField("Item Name", script.itemsToTrack[i].itemName);
            script.itemsToTrack[i].itemType = (DynamicDifficultyAdjustment.ItemType)EditorGUILayout.EnumPopup("Item Type", script.itemsToTrack[i].itemType);
            script.itemsToTrack[i].actionPoints = EditorGUILayout.IntField("Action Points", script.itemsToTrack[i].actionPoints);
            script.itemsToTrack[i].medicinePoints = EditorGUILayout.IntField("Medicine Points", script.itemsToTrack[i].medicinePoints);
            script.itemsToTrack[i].utilityPoints = EditorGUILayout.IntField("Utility Points", script.itemsToTrack[i].utilityPoints);
            EditorGUILayout.Space();
        }
    }
}
#endif
