using UnityEngine;
using TMPro;
using System.Linq;
[System.Serializable]
public class ItemPrefab
{
    public string itemName;
    public GameObject itemPrefab;
}

public class ItemSpawner : MonoBehaviour
{
    public ItemDropManager itemDropManager;
    public ItemPrefab[] itemPrefabs;
    public GameObject sceneObject;
    public SoundManager soundManager;
    private string uiPanelTag = "Interactiontag"; 
    private string customTextTag = "Interactiontext"; 
    private bool canPickup;
    public string Soundname;
    public TextMeshProUGUI customText;
    public string custominteractiontext;

    public void Awake()
    {
        FindUIElementsByTag();
        soundManager = FindObjectOfType<SoundManager>();
    }
    void Start()
    {
        canPickup = false;
        itemDropManager = FindObjectOfType<ItemDropManager>();
        soundManager = FindObjectOfType<SoundManager>();
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
        customText.text = custominteractiontext;
    }

    private void HideEButton()
    {
        sceneObject.SetActive(false);
        customText.text = "";
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
        int totalMedicinePoints = itemDropManager.difficultyAdjustment.CalculateTotalMedicinePoints();
        int totalUtilityPoints = itemDropManager.difficultyAdjustment.CalculateTotalUtilityPoints();


        // Get the item to drop based on difficulty level
        string itemNameToDrop = itemDropManager.AdjustItemDrops(totalActionPoints, totalMedicinePoints, totalUtilityPoints);

        // Spawn the corresponding prefab for the dropped item
        if(itemNameToDrop == "Null")
        {
            Destroy(this.gameObject);
            customText.text = "";
        }
        else
        {
        SpawnPrefab(itemNameToDrop);
        customText.text = "";
        Destroy(this.gameObject);
        }
    }

    // Method to spawn a prefab based on the item name
    private void SpawnPrefab(string itemName)
    {
        // Find the corresponding prefab for the item name
        ItemPrefab itemPrefab = System.Array.Find(itemPrefabs, prefab => prefab.itemName == itemName);

        // If the prefab is found and not null, instantiate it at the current transform position
        if (itemPrefab != null && itemPrefab.itemPrefab != null)
        {
            Instantiate(itemPrefab.itemPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Debug.LogWarning($"Prefab not found or is null for item name: {itemName}");
        }
    }
    private void FindUIElementsByTag()
    {
        // Find UI panel by tag
        GameObject[] sceneObjects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.CompareTag(uiPanelTag)).ToArray();
        if (sceneObjects.Length > 0)
        {
            sceneObject = sceneObjects[0]; // Assuming there is only one UI panel with the specified tag
        }

        // Find custom text by tag
        TextMeshProUGUI[] customTexts = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>().Where(obj => obj.CompareTag(customTextTag)).ToArray();
        if (customTexts.Length > 0)
        {
            customText = customTexts[0]; // Assuming there is only one custom text with the specified tag
        }
    }
}
