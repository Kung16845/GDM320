using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class Walllamblight : MonoBehaviour
{
    public GameObject sceneObject;
    public TextMeshProUGUI customText;
    private string custominteractiontext;
    private string uiPanelTag = "Interactiontag";
    private string customTextTag = "Interactiontext";
    public bool lightquip;
    public bool fuelequip;
    public bool fuelrefill;
    public WalllampDuration walllampDuration;
    public bool isclose;
    public InventoryPresentCharactor inventoryPresentCharactor;
    public SoundManager soundManager;
    private void Start()
    {
        FindUIElementsByTag();
        lightquip = false;
        fuelequip = false;
        HideEButton();
        soundManager = FindObjectOfType<SoundManager>();
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
    }
    private void Update()
    {
        // Check if the player can pick up the ammo and 'E' is pressed.
        if (isclose && Input.GetKeyDown(KeyCode.E) && fuelequip)
        {
            Refillfuel();
        }
        if (isclose && Input.GetKeyDown(KeyCode.E) && lightquip && fuelrefill)
        {
            lightthelamp();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isclose = true;
        }
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (isclose && walllampDuration.lightup)
            {
                HideEButton();
            }
            else if (isclose)
            {
                ShowEButton();

                if (!fuelequip && !lightquip && !fuelrefill)
                {
                    customText.text = "need alcholhal and fire to lit this up.";
                }
                else if (fuelequip && !lightquip && !fuelrefill)
                {
                    customText.text = "Press E to refill the fuel.";
                }
                else if (!fuelequip && lightquip && !fuelrefill)
                {
                    customText.text = "Still need to find fuel.";
                }
                else if (!fuelequip && !lightquip && fuelrefill)
                {
                    customText.text = "Still need to find a source of lighter.";
                }
                else if (!fuelequip && lightquip && fuelrefill)
                {
                    customText.text = "Press E to light the lamp.";
                }
            }
        }

    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideEButton();
            isclose = false;
        }
    }
    private void lightthelamp()
    {
        if(inventoryPresentCharactor.GetTotalItemCountByName("Matchesbox") >= 2)
        {
            inventoryPresentCharactor.ManageReduceResource("Matchesbox");
        }
        else if(inventoryPresentCharactor.GetTotalItemCountByName("Matchesbox") == 1)
        {
            inventoryPresentCharactor.DeleteItemCharactorEquipment("InventoryMatches");
        }
        soundManager.PlaySound("Firelit");
        walllampDuration.lightup = true;
        fuelrefill = false;
    }
    private void Refillfuel()
    {
        inventoryPresentCharactor.DeleteItemCharactorEquipment("InventoryAlcoholbottom");
        fuelrefill = true;
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

