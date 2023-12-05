using UnityEngine;
using System.Linq;

public class RandomItemGenerator : MonoBehaviour
{
    public string[] itemNames; // Array of possible item names
    public int sequenceLength = 5; // Length of the sequence
    public bool allowRepeats = false; // Allow repeating items in the sequence

    public string[] generatedSequence; // Store the generated sequence

    void Start()
    {
        GenerateRandomSequence();
    }

    void GenerateRandomSequence()
    {
        if (itemNames.Length == 0 || sequenceLength <= 0)
        {
            Debug.LogError("Please set valid values for itemNames and sequenceLength in the Inspector.");
            return;
        }

        generatedSequence = new string[sequenceLength];

        for (int i = 0; i < sequenceLength; i++)
        {
            if (i == 0 || i == 1) // For the first and second items
            {
                // Randomly select any item
                generatedSequence[i] = itemNames[Random.Range(0, itemNames.Length)];
            }
            else if ((i == 2) && (generatedSequence[0] == "Diamond" || generatedSequence[0] == "Bolt cutter" || generatedSequence[0] == "Maiden" ||
                                  generatedSequence[1] == "Diamond" || generatedSequence[1] == "Bolt cutter" || generatedSequence[1] == "Maiden"))
            {
                // If the first or second item is one of the specified three, randomly select any remaining item
                string[] remainingItems = itemNames.Except(new string[] { "Diamond", "Bolt cutter", "Maiden" }).ToArray();
                generatedSequence[i] = remainingItems[Random.Range(0, remainingItems.Length)];
            }
            else
            {
                // Randomly select any item
                generatedSequence[i] = itemNames[Random.Range(0, itemNames.Length)];
            }

            // If repeats are not allowed, ensure the current item is unique in the sequence
            if (!allowRepeats)
            {
                while (ArrayContains(generatedSequence, generatedSequence[i], i))
                {
                    generatedSequence[i] = itemNames[Random.Range(0, itemNames.Length)];
                }
            }
        }

        // Print the generated sequence
        for (int i = 0; i < generatedSequence.Length; i++)
        {
            Debug.Log((i + 1) + ". " + generatedSequence[i]);
        }
    }

    bool ArrayContains(string[] array, string value, int endIndex)
    {
        for (int i = 0; i < endIndex; i++)
        {
            if (array[i] == value)
            {
                return true;
            }
        }
        return false;
    }
        public string[] GetGeneratedSequence()
    {
        return generatedSequence;
    }
}
