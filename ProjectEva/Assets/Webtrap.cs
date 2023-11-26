using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Webtrap : MonoBehaviour
{
    public TrapController trapController;
    public GameObject sceneObject;
    public TextMeshProUGUI customText;
    private string uiPanelTag = "Interactiontag"; 
    private string customTextTag = "Interactiontext"; 
    public string custominteractiontext;
    public SoundManager soundManager;
    public bool Canburn;
    public bool isclose;
    public InventoryPresentCharactor inventoryPresentCharactor;
    
    private void Awake()
    {
        FindUIElementsByTag();
        HideEButton();
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
        trapController = FindObjectOfType<TrapController>();
        soundManager = FindObjectOfType<SoundManager>();
        Canburn = false;
    }

    void Start() 
    {
        FindUIElementsByTag();
        HideEButton();
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
        trapController = FindObjectOfType<TrapController>();
        Canburn = false;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Leg"))
        {
            isclose = true;
            trapController.HitbyWebtrap();
            ShowEButton();
            if(Canburn && isclose)
            {
                customText.text = "I can burn this web.";
            }
            else if (!Canburn && isclose)
            {
                customText.text = "Gotta find somethings to burn this.";
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Leg"))
        {   
            isclose = false;
            HideEButton();
            trapController.PlayerReleaseTrap();
        }
    }
    void Update()
    {
        if(Canburn && isclose)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(inventoryPresentCharactor.GetTotalItemCountByName("Matches") >= 2)
                {
                inventoryPresentCharactor.ManageReduceResource("Matches");
                soundManager.PlaySound("Firelit");
                }
                else if(inventoryPresentCharactor.GetTotalItemCountByName("Matches") == 1)
                {
                inventoryPresentCharactor.DeleteItemCharactorEquipment("InventoryMatches");
                soundManager.PlaySound("Firelit");
                }
                Destroy(this.gameObject);
            }
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
