using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount; 
    private string uiPanelTag = "Interactiontag"; 
    private string customTextTag = "Interactiontext"; 
    public GameObject sceneObject;
    private Pistol pistol;
    public SoundManager soundManager;
    private bool canPickup;
    public InventoryPresentCharactor inventoryPresentCharactor;
    public ItemsDataCharactor itemsDataCharactor;
    public TextMeshProUGUI customText;

    public void PickupItemCharactors()
    {
        inventoryPresentCharactor.AddItemCharactors(itemsDataCharactor);
    }

    private void Awake()
    {
        canPickup = false;
        FindUIElementsByTag();
        HideEButton();
        pistol = FindObjectOfType<Pistol>();
        soundManager = FindObjectOfType<SoundManager>();
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
    }

    private void Start()
    {
        customText.text = "Press E to pick up the item.";
    }

    private void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E) && !inventoryPresentCharactor.checkIsSlotFull)
        {
            PickupItemCharactors();
            if (!inventoryPresentCharactor.checkIsSlotFull)
            {
                PickupAmmo();
            }
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

    private void PickupAmmo()
    {
        if (pistol != null)
        {
            soundManager.PlaySound("Pickupitem");
            customText.text = "";
            Destroy(this.gameObject);
        }
    }

    private void ShowEButton()
    {
        sceneObject.SetActive(true);
        customText.text = "Press E to pick up the item.";
    }

    private void HideEButton()
    {
        sceneObject.SetActive(false);
        customText.text = "";
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
