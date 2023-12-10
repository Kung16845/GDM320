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
    public Fusequest fusequest;
    public Fuelquest fuelquest;
    public GameObject redlight;
    public GameObject greenlight;
    public GameObject yellowlight;
    public SoundManager soundManager;
    public GameObject objecttoremove;
    public TextMeshProUGUI customText;
    public InventoryPresentCharactor inventoryPresentCharactor;
    public string custominteractiontext;
    public bool isclose;
    public bool fusehasequiped;
    public bool fuelhasequiped;
    public bool allset;
    public bool yellowon;
    private void Start()
    {
        isclose = false;
        FindUIElementsByTag();
        soundManager = FindObjectOfType<SoundManager>();
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
        greenlight.SetActive(false);
        yellowlight.SetActive(false);
        bool yellowon = false;
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
        if(allset && isclose )
        {   
            custominteractiontext = "I can turn this switch down finally I can get out of here.";
        }
        else if(!fuelhasequiped && isclose && fusehasequiped)
        {
            custominteractiontext = "I still need to find the fuel the turn this switch down.";
        }
        else if(fuelhasequiped && isclose && !fusehasequiped)
        {
            custominteractiontext = "I still need to find the fuse the turn this switch down.";
        }
        else if(!fuelquest.isfuelquip && isclose && fusequest.isfusequip)
        {
            custominteractiontext = "I can equip this fuse in the genarator room.";
        }
        else if(fuelquest.isfuelquip && isclose && !fusequest.isfusequip)
        {
            custominteractiontext = "I can refill the genarator.";
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
        if(fuelquest.isfuelquip)
        {
            fuelhasequiped = true;
        }
        if(fusequest.isfusequip)
        {
            fusehasequiped = true;
        }
        if(fuelhasequiped  && fuelhasequiped)
        {
            allset = true;
        }
        if(isclose && allset && Input.GetKeyDown(KeyCode.E)) 
        {
            yellowon = true;
            objecttoremove.SetActive(false);
            greenlight.SetActive(true);
            yellowlight.SetActive(false);
        }
        else if(allset == true && !yellowon)
        {   
            yellowlight.SetActive(true) ;
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
