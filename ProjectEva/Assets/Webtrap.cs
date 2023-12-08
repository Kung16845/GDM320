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
                if(inventoryPresentCharactor.GetTotalItemCountByName("Matchesbox") >= 2)
                {
                inventoryPresentCharactor.ManageReduceResource("Matchesbox");
                soundManager.PlaySound("Firelit");
                }
                else if(inventoryPresentCharactor.GetTotalItemCountByName("Matchesbox") == 1)
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
