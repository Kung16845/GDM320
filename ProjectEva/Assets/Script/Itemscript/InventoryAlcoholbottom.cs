using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using UnityEngine.UI;

public class InventoryAlcoholbottom : MonoBehaviour
{
    public Walllamblight walllamblight;
    public float CraftingSpeed = 50f;
    public float CraftingValue = 100f;
    public NewMovementPlayer movementScript;
    private string uiPanelTag = "Interactiontag";
    private string customTextTag = "Interactiontext";
    public GameObject sceneObject;
    public SoundManager soundManager;
    public TextMeshProUGUI customText;
    public string custominteractiontext;
    public Slider slider;
    public GameObject healslider;
    public bool iscrafting;
    public InventoryPresentCharactor inventoryPresentCharactor;
    [SerializeField]
    private string itemPrefabName = "Bottlesanity";

    private void Awake()
    {
        FindUIElementsByTag();
        soundManager = FindObjectOfType<SoundManager>();
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
        movementScript = FindObjectOfType<NewMovementPlayer>();
        customText.text = "If I combine this with chemical bottle I can craft a stronger chemical bottle or I save this the lit fire to stall that thing.";
        Object[] allObjects = GameObject.FindObjectsOfType(typeof(GameObject), true);
        foreach (GameObject obj in allObjects)
        {
            if (obj.tag == "Sliderbar")
            {
                healslider = obj;
                slider = obj.GetComponent<Slider>();
            }
        }
        slider = healslider.GetComponent<Slider>();
        slider.maxValue = CraftingValue;
        slider.value = 0f;
        // Find all Walllamblight objects and set their properties
        Walllamblight[] wallamblights = FindObjectsOfType<Walllamblight>();
        foreach (Walllamblight wallamblight in wallamblights)
        {
            wallamblight.fuelequip = true;
        }
    }
    void Update()
    {
        sceneObject.SetActive(true);
        if(inventoryPresentCharactor.GetTotalItemCountByName("Chemicalbottle") >= 1)
        {
        customText.text = "I can combine this with Chemicalbottle to craft Stronger chemical bottle.";
        }
        if (Input.GetKey(KeyCode.G) && inventoryPresentCharactor.GetTotalItemCountByName("Chemicalbottle") >= 1)
        {
            healslider.SetActive(true);
            iscrafting = true;
            slider.value += CraftingSpeed * Time.deltaTime;
            movementScript.StopMoving();
            if (slider.value >= CraftingValue)
            {
                slider.value = 0f;
                movementScript.ResumeMoving();
                iscrafting = false;
                healslider.SetActive(false);
                inventoryPresentCharactor.ManageReduceResource("Chemicalbottle");
                if(inventoryPresentCharactor.GetTotalItemCountByName("AlcholhalBottle") == 1)
                {
                inventoryPresentCharactor.DeleteItemCharactorEquipment("InventoryAlcoholbottom");
                }
                else if(inventoryPresentCharactor.GetTotalItemCountByName("AlcholhalBottle") >= 2)
                {
                inventoryPresentCharactor.ManageReduceResource("AlcholhalBottle");
                }
                GameObject prefab = Resources.Load<GameObject>("Prefeb/item/" + itemPrefabName);
                Debug.Log("Load path: " + "Prefeb/item/" + itemPrefabName);

                // Instantiate the prefab
                Instantiate(prefab, transform.position, Quaternion.identity);
            }
        }
        else
        {
            if (iscrafting)
            {
                slider.value = 0f;
                iscrafting = false;
                healslider.SetActive(false);
                movementScript.ResumeMoving();
            }
        }
    }
    void OnDestroy()
    {
        Walllamblight[] wallamblights = FindObjectsOfType<Walllamblight>();
        foreach (Walllamblight wallamblight in wallamblights)
        {
            wallamblight.fuelequip = false;
        }
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
