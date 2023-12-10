using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
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
    public bool isclose;
    // Reference to the RandomItemGenerator script
    public RandomItemGenerator randomItemGenerator;

    private void Start()
    {
        canunlock = false;
        FindUIElementsByTag();
        soundManager = FindObjectOfType<SoundManager>();
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
    }

    private void Update()
    {
        if (canunlock && Input.GetKeyDown(KeyCode.E) && isclose)
        { 
            SpawnItem();
            inventoryPresentCharactor.DeleteItemCharactorEquipment("Inventorypicklock");
            StartCoroutine(PlayLockersound());
        }
    }

    private void SpawnItem()
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
        if (generatedSequence.Length > 0)
        {
            // Find the corresponding item prefab based on the item name
            GameObject itemPrefab = FindItemPrefab(generatedSequence[0]);

            // Spawn the item at the position of the LockerItemSpawner
            if (itemPrefab != null)
            {
                Instantiate(itemPrefab, transform.position, Quaternion.identity);

                // Remove the spawned item from the RandomItemGenerator sequence
                randomItemGenerator.RemoveItem(generatedSequence[0]);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isclose = true;
            ShowEButton();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isclose = false;
            HideEButton();
        }
    }

    private void ShowEButton()
    {
        sceneObject.SetActive(true);
        if (canunlock)
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
    IEnumerator PlayLockersound()
    {
        yield return new WaitForSeconds(0.4f);
        soundManager.PlaySound("Lockerkey"); 
        yield return new WaitForSeconds(0.8f);
        soundManager.PlaySound("Lockeropen");
        Destroy(this.gameObject);
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

    private GameObject FindItemPrefab(string itemName)
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
