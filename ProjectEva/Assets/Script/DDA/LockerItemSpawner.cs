using UnityEngine;
using TMPro;

public class LockerItemSpawner : MonoBehaviour
{
    [System.Serializable]
    public class ItemPrefabInfo
    {
        public string customName;
        public GameObject prefab;
    }

    public ItemPrefabInfo[] itemPrefabs; // Array of item prefabs to spawn
    private string uiPanelTag = "Interactiontag";
    private string customTextTag = "Interactiontext";
    public GameObject sceneObject;
    public SoundManager soundManager;
    public TextMeshProUGUI customText;
    public InventoryPresentCharactor inventoryPresentCharactor;
    public bool canunlock;
    public string custominteractiontext;

    // Reference to the RandomItemGenerator script
    public RandomItemGenerator randomItemGenerator;

    private int currentItemIndex = 0; // Track the current item index in the generated sequence

    void Start()
    {
        canunlock = false;
        FindUIElementsByTag();
        soundManager = FindObjectOfType<SoundManager>();
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
    }

    void Update()
    {
        if (canunlock && Input.GetKeyDown(KeyCode.E))
        {
            SpawnNextItem();
            inventoryPresentCharactor.DeleteItemCharactorEquipment("Inventorypicklock");
            Destroy(this.gameObject);
        }
    }

    void SpawnNextItem()
    {
        // Check if the RandomItemGenerator script is assigned
        if (randomItemGenerator == null)
        {
            Debug.LogError("Please assign the RandomItemGenerator script in the Inspector.");
            return;
        }

        // Get the generated sequence from the RandomItemGenerator script
        string[] generatedSequence = randomItemGenerator.GetGeneratedSequence();

        // Check if there are more items to spawn
        if (currentItemIndex < generatedSequence.Length)
        {
            // Find the corresponding item prefab based on the item name
            GameObject itemPrefab = FindItemPrefab(generatedSequence[currentItemIndex]);

            // Spawn the item at the position of the LockerItemSpawner
            if (itemPrefab != null)
            {
                Instantiate(itemPrefab, transform.position, Quaternion.identity);
            }

            currentItemIndex++;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowEButton();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideEButton();
        }
    }

    private void ShowEButton()
    {
        sceneObject.SetActive(true);
        if(canunlock)
        {
            customText.text = "I can unlock this.";
        }
        else
        {
        customText.text = custominteractiontext;
        }
    }

    private void HideEButton()
    {
        sceneObject.SetActive(false);
        customText.text = "";
    }

    private void FindUIElementsByTag()
    {
        // Find UI panel by tag
        GameObject[] sceneObjects = GameObject.FindGameObjectsWithTag(uiPanelTag);
        if (sceneObjects.Length > 0)
        {
            sceneObject = sceneObjects[0]; // Assuming there is only one UI panel with the specified tag
        }

        // Find custom text by tag
        GameObject[] customTexts = GameObject.FindGameObjectsWithTag(customTextTag);
        if (customTexts.Length > 0)
        {
            customText = customTexts[0].GetComponent<TextMeshProUGUI>(); // Assuming there is only one custom text with the specified tag
        }
    }

    GameObject FindItemPrefab(string itemName)
    {
        // Find the item prefab based on the custom name
        foreach (ItemPrefabInfo prefabInfo in itemPrefabs)
        {
            if (prefabInfo.customName == itemName)
            {
                return prefabInfo.prefab;
            }
        }

        Debug.LogWarning("Prefab not found for item: " + itemName);
        return null;
    }
}
