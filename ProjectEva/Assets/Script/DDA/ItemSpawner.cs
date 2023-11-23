using UnityEngine;

[System.Serializable]
public class ItemPrefab
{
    public string itemName;
    public GameObject itemPrefab;
    public Transform customSpawnTransform; // Added variable for custom transform
}

public class ItemSpawner : MonoBehaviour
{
    public ItemDropManager itemDropManager;
    public ItemPrefab[] itemPrefabs;
    public GameObject sceneObject;
    public SoundManager soundManager;
    private bool canPickup;
    public string Soundname;

    void Start()
    {
        canPickup = false;
    }

    void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            soundManager.PlaySound(Soundname);
            SpawnItems();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowEButton();
            canPickup = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideEButton();
            canPickup = false;
        }
    }

    private void ShowEButton()
    {
        sceneObject.SetActive(true);
    }

    private void HideEButton()
    {
        sceneObject.SetActive(false);
    }

    public void SpawnItems()
    {
        if (itemDropManager == null)
        {
            Debug.LogError("ItemDropManager reference not set in ItemSpawner script!");
            return;
        }

        // Calculate difficulty level
        int totalActionPoints = itemDropManager.difficultyAdjustment.CalculateTotalActionPoints();

        // Get the item to drop based on difficulty level
        string itemNameToDrop = itemDropManager.AdjustItemDrops(totalActionPoints);

        // Spawn the corresponding prefab for the dropped item
        SpawnPrefab(itemNameToDrop);
    }

    // Method to spawn a prefab based on the item name
    private void SpawnPrefab(string itemName)
    {
        // Find the corresponding prefab for the item name
        ItemPrefab itemPrefab = System.Array.Find(itemPrefabs, prefab => prefab.itemName == itemName);

        // If the prefab is found and not null, instantiate it at the specified transform position
        if (itemPrefab != null && itemPrefab.itemPrefab != null)
        {
            // Use the custom transform if provided, otherwise use the current transform
            Transform spawnTransform = itemPrefab.customSpawnTransform != null ? itemPrefab.customSpawnTransform : transform;

            Instantiate(itemPrefab.itemPrefab, spawnTransform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning($"Prefab not found or is null for item name: {itemName}");
        }
    }
}
