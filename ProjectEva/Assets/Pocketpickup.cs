using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class Pocketpickup : MonoBehaviour
{
    // Start is called before the first frame update
    public InventoryPresentCharactor inventoryPresentCharactor;
    public GameObject sceneObject;
    public SoundManager soundManager;
    private string uiPanelTag = "Interactiontag"; 
    private string customTextTag = "Interactiontext"; 
    private bool canPickup;
    public string Soundname;
    public TextMeshProUGUI customText;
    public string custominteractiontext;
    void Awake ()
    {
        FindUIElementsByTag();
        soundManager = FindObjectOfType<SoundManager>();
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
    }
    void Start()
    {
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
        sceneObject.SetActive(false);
    }
    void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {   
            inventoryPresentCharactor.UnlockSlot();
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
