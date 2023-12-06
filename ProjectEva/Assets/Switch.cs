using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Switch: MonoBehaviour
{
    private string uiPanelTag = "Interactiontag";
    private string customTextTag = "Interactiontext";
    public GameObject sceneObject;
    public GameObject itemPrefab;
    public Fusequest fusequest;
    public Fuelquest fuelquest;
    public GameObject redlight;
    public GameObject greenlight;
    public GameObject yellowlight;
    public SoundManager soundManager;
    public TextMeshProUGUI customText;
    public InventoryPresentCharactor inventoryPresentCharactor;
    public string custominteractiontext;
    public bool isclose;
    public bool allset;
    private void Start()
    {
        isclose = false;
        FindUIElementsByTag();
        soundManager = FindObjectOfType<SoundManager>();
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
        greenlight.SetActive(false);
        yellowlight.SetActive(false);
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
        if(fuelquest.isfuelquip && isclose && fusequest.isfusequip)
        {   
            custominteractiontext = "I can turn this switch down finally I can get out of here.";
        }
        else if(!fuelquest.isfuelquip && isclose && fusequest.isfusequip)
        {
            custominteractiontext = "I still need to find the fuel the turn this switch down.";
        }
        else if(fuelquest.isfuelquip && isclose && !fusequest.isfusequip)
        {
            custominteractiontext = "I still need to find the fuse the turn this switch down.";
        }
        else if(!fuelquest.isfuelquip && isclose && !fusequest.isfusequip)
        {
            custominteractiontext = "I need to find the fuel and fuse the turn this switch down.";
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
        if(fuelquest.isfuelquip && fusequest.isfusequip)
        {
            allset = true;
        }
        if(isclose && allset && Input.GetKeyDown(KeyCode.E)) 
        {
            greenlight.SetActive(true);
            yellowlight.SetActive(false);
        }
        else if(allset == true)
        {   
            yellowlight.SetActive(true);
            redlight.SetActive(false);
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
}
