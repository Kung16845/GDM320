using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Fuelquest : MonoBehaviour
{
    private string uiPanelTag = "Interactiontag";
    private string customTextTag = "Interactiontext";
    public GameObject sceneObject;
    public SoundManager soundManager;
    public TextMeshProUGUI customText;
    public InventoryPresentCharactor inventoryPresentCharactor;
    public string custominteractiontext;
    public bool isclose;
    public bool isfuelquip;
    private void Start()
    {
        isclose = false;
        isfuelquip = false;
        FindUIElementsByTag();
        soundManager = FindObjectOfType<SoundManager>();
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isclose = true;
            ShowEButton();
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(isfuelquip && isclose)
        {
            custominteractiontext = "I can refuel this genarator.";
        }
        else if(!isfuelquip && isclose)
        {
            custominteractiontext = "Need fuel the get this running so i can open the door.";
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
    void Update()
    {
        if(isclose && isfuelquip && Input.GetKeyDown(KeyCode.E))
        {
            inventoryPresentCharactor.DeleteItemCharactorEquipment("Inventoryfuel");
            soundManager.PlaySound("Pour fuel");
             Destroy(this.gameObject);
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
