using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;
public class Pickupnote : MonoBehaviour
{
   public SoundManager soundManager;
    private bool canPickup;
    private string uiPanelTag = "Interactiontag"; 
    private string customTextTag = "Interactiontext"; 
    public string Soundname;
    public GameObject sceneObject;
    public TextMeshProUGUI customText;
    public string custominteractiontext;
    public InventoryItemNotePresent inventoryItemNotePresent;
    public ItemDataNote itemDataNote;
    public void PickupItemNote()
    {
        inventoryItemNotePresent.AddItemsNote(itemDataNote);
    }
    private void Awake()
    {
        canPickup = false;
        FindUIElementsByTag();
        HideEButton();
        soundManager = FindObjectOfType<SoundManager>();
        inventoryItemNotePresent = FindObjectOfType<InventoryItemNotePresent>();
    }
    private void Start()
    {
        canPickup = false;
        HideEButton();
        soundManager = FindObjectOfType<SoundManager>();
        inventoryItemNotePresent = FindObjectOfType<InventoryItemNotePresent>();
    }
    private void Update()
    {
        // Check if the player can pick up the ammo and 'E' is pressed.
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            PickupItemNote();
            soundManager.PlaySound(Soundname);
            customText.text = "";
            Destroy(this.gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowEButton();
            customText.text = custominteractiontext;
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