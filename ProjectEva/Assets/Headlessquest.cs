using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class Headlessquest : MonoBehaviour
{
    private string uiPanelTag = "Interactiontag";
    private string customTextTag = "Interactiontext";
    public GameObject sceneObject;
    public GameObject itemPrefab;
    public SoundManager soundManager;
    public TextMeshProUGUI customText;
    public InventoryPresentCharactor inventoryPresentCharactor;
    public string custominteractiontext;
    public bool isclose;
    public bool Headwitheyeequip;
    public bool iseyeequip;
    public bool isheadequip;
    private void Start()
    {
        isclose = false;
        Headwitheyeequip = false;
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
        if(iseyeequip && isclose)
        {
            custominteractiontext = "I think I need to find its head.";
        }
        else if(isheadequip && isclose)
        {
            custominteractiontext = "I think I need to find its eye.";
        }
        else if(Headwitheyeequip && isclose)
        {
            custominteractiontext = "I can equip it.";
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
        if(isclose && Headwitheyeequip && Input.GetKeyDown(KeyCode.E))
        {
             Instantiate(itemPrefab, transform.position, Quaternion.identity);
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
