using UnityEngine;

[System.Serializable]
public class ItemDrop
{
    public string itemName;
    [Range(0, 100)]
    public int baseDropPercentage; // Base drop percentage for the item
    public int minActionPoints; // Minimum action points for increased drop rate
    public int maxActionPoints; // Maximum action points for decreased drop rate
    [Range(0, 100)]
    public int increasePercentage; // Percentage increase when action points are above minActionPoints
    [Range(0, 100)]
    public int decreasePercentage; // Percentage decrease when action points are above maxActionPoints
}

public class ItemDropManager : MonoBehaviour
{
    public DynamicDifficultyAdjustment difficultyAdjustment;
    public ItemDrop[] itemDrops; // Items to drop with custom percentages

    public string itemNameToDrop;

    private void Start()
    {
        if (difficultyAdjustment == null)
        {
            Debug.LogError("DifficultyAdjustment reference not set in ItemDropManager script!");
        }
    }

    private void Update()
    {
        // Calculate difficulty level
        int totalActionPoints = difficultyAdjustment.CalculateTotalActionPoints();
        // Get the item to drop based on difficulty level
        itemNameToDrop = AdjustItemDrops(totalActionPoints);
        // Debug.Log(totalActionPoints);
    }

    public string AdjustItemDrops(int totalActionPoints)
    {
        // Calculate total base drop percentage
        int totalBaseDropPercentage = 0;
        foreach (var itemDrop in itemDrops)
        {
            totalBaseDropPercentage += itemDrop.baseDropPercentage;
        }

        // Calculate total drop percentage considering base and increased/decreased percentages
        int totalDropPercentage = totalBaseDropPercentage;
        foreach (var itemDrop in itemDrops)
        {
            if (totalActionPoints >= itemDrop.minActionPoints && totalActionPoints <= itemDrop.maxActionPoints)
            {
                totalDropPercentage += itemDrop.increasePercentage;
            }
            else if (totalActionPoints > itemDrop.maxActionPoints)
            {
                totalDropPercentage -= itemDrop.decreasePercentage;
            }
        }

        // Generate a random number between 0 and totalDropPercentage
        int randomNumber = Random.Range(0, totalDropPercentage + 1);

        // Determine which item to drop based on the random number and drop percentages
        int cumulativePercentage = 0;
        foreach (var itemDrop in itemDrops)
        {
            cumulativePercentage += itemDrop.baseDropPercentage;

            if (totalActionPoints >= itemDrop.minActionPoints && totalActionPoints <= itemDrop.maxActionPoints)
            {
                cumulativePercentage += itemDrop.increasePercentage;
            }
            else if (totalActionPoints > itemDrop.maxActionPoints)
            {
                cumulativePercentage -= itemDrop.decreasePercentage;
            }

            if (randomNumber <= cumulativePercentage)
            {
                // Debug.Log($"Dropping {itemDrop.itemName} with {cumulativePercentage}% chance");
                return itemDrop.itemName;
            }
        }
        return null;
    }
}
