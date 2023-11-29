using UnityEngine;
using System.Linq;
[System.Serializable]
    public class ItemDrop
    {
        public string itemName;
        public int priority; // Priority of the item (lower number means higher priority)
        public DynamicDifficultyAdjustment.ItemType itemType; // ItemType of the item
        [Range(0, 100)]
        public int baseDropPercentage; // Base drop percentage for the item
        [Range(0, 100)]
        public int increasePercentage; // Percentage increase when points are above minPoints
        [Range(0, 100)]
        public int decreasePercentage; // Percentage decrease when points are above maxPoints
        public int minPoints; // Minimum points for increased drop rate
        public int maxPoints; // Maximum points for decreased drop rate
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
        // Calculate difficulty levels for action, medicine, and utility points
        int totalActionPoints = difficultyAdjustment.CalculateTotalActionPoints();
        int totalMedicinePoints = difficultyAdjustment.CalculateTotalMedicinePoints();
        int totalUtilityPoints = difficultyAdjustment.CalculateTotalUtilityPoints();

        // Get the item to drop based on the lowest difficulty level
        itemNameToDrop = AdjustItemDrops(totalActionPoints, totalMedicinePoints, totalUtilityPoints);
    }

    public string AdjustItemDrops(int actionPoints, int medicinePoints, int utilityPoints)
    {
        // Determine the type with the lowest points
        DynamicDifficultyAdjustment.ItemType lowestType = GetLowestType(actionPoints, medicinePoints, utilityPoints);

        // Filter itemDrops based on the lowest point type
        var filteredItemDrops = itemDrops.Where(d => d.itemType == lowestType).ToArray();

        // Calculate total base drop percentage for the lowest point type
        int totalBaseDropPercentage = filteredItemDrops.Sum(d => d.baseDropPercentage);

        // Calculate total drop percentage considering base and increased/decreased percentages for the lowest point type
        int totalDropPercentage = totalBaseDropPercentage;

        foreach (var itemDrop in filteredItemDrops)
        {
            int totalPoints = 0;
            int increasePercentage = itemDrop.increasePercentage;
            int decreasePercentage = itemDrop.decreasePercentage;

            switch (itemDrop.itemType)
            {
                case DynamicDifficultyAdjustment.ItemType.Action:
                    totalPoints = actionPoints;
                    break;
                case DynamicDifficultyAdjustment.ItemType.Medicine:
                    totalPoints = medicinePoints;
                    break;
                case DynamicDifficultyAdjustment.ItemType.Utility:
                    totalPoints = utilityPoints;
                    break;
                // Add more cases for other item types as needed
            }

            if (totalPoints >= itemDrop.minPoints && totalPoints <= itemDrop.maxPoints)
            {
                totalDropPercentage += increasePercentage;
            }
            else if (totalPoints > itemDrop.maxPoints)
            {
                totalDropPercentage -= decreasePercentage;
            }
        }

        // Generate a random number between 0 and totalDropPercentage
        int randomNumber = Random.Range(0, totalDropPercentage + 1);

        // Determine which item to drop based on the random number and drop percentages
        int cumulativePercentage = 0;

        foreach (var itemDrop in filteredItemDrops)
        {
            cumulativePercentage += itemDrop.baseDropPercentage;

            int totalPoints = 0;
            int increasePercentage = itemDrop.increasePercentage;
            int decreasePercentage = itemDrop.decreasePercentage;

            switch (itemDrop.itemType)
            {
                case DynamicDifficultyAdjustment.ItemType.Action:
                    totalPoints = actionPoints;
                    break;
                case DynamicDifficultyAdjustment.ItemType.Medicine:
                    totalPoints = medicinePoints;
                    break;
                case DynamicDifficultyAdjustment.ItemType.Utility:
                    totalPoints = utilityPoints;
                    break;
                // Add more cases for other item types as needed
            }

            if (totalPoints >= itemDrop.minPoints && totalPoints <= itemDrop.maxPoints)
            {
                totalDropPercentage += increasePercentage;
            }
            else if (totalPoints > itemDrop.maxPoints)
            {
                totalDropPercentage -= decreasePercentage;
            }

            if (randomNumber <= cumulativePercentage)
            {
                return itemDrop.itemName;
            }
        }

        // Fallback: Choose the item with the highest baseDropPercentage if none are chosen
        return filteredItemDrops.OrderByDescending(d => d.baseDropPercentage).FirstOrDefault()?.itemName;
    }

    private DynamicDifficultyAdjustment.ItemType GetLowestType(int actionPoints, int medicinePoints, int utilityPoints)
    {
        if (actionPoints <= medicinePoints && actionPoints <= utilityPoints)
        {
            return DynamicDifficultyAdjustment.ItemType.Action;
        }
        else if (medicinePoints <= actionPoints && medicinePoints <= utilityPoints)
        {
            return DynamicDifficultyAdjustment.ItemType.Medicine;
        }
        else
        {
            return DynamicDifficultyAdjustment.ItemType.Utility;
        }
    }
}
