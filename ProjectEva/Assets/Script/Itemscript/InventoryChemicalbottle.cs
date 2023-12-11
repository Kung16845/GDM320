using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using TMPro;

public class InventoryChemicalbottle : MonoBehaviour
{
    public float drinkSpeed = 80f; // Speed at which the healing slider fills up.
    public float maxHealingValue = 100f; // Maximum value the healing slider can reach.
    public float healAmount = 10f; // Amount to heal the player when the slider reaches its maximum value.
    public Sanity playersanity; // Reference to the player's HP script.
    public NewMovementPlayer movementScript;
    public Slider slider; // Reference to the Slider component.
    private string uiPanelTag = "Interactiontag";
    private string customTextTag = "Interactiontext";
    public GameObject sceneObject;
    public SoundManager soundManager;
    public TextMeshProUGUI customText;
    public string custominteractiontext;
    private bool isHealing = false; // Flag to track if healing is in progress.
    public GameObject healslider;
    public InventoryPresentCharactor inventoryPresentCharactor;


    private void Awake()
    {
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
        playersanity = FindObjectOfType<Sanity>();
        movementScript = FindObjectOfType<NewMovementPlayer>();    
        FindUIElementsByTag();
        customText.text = "I can drink the dull he pain in my head or save it for later.";
        Object[] allObjects = GameObject.FindObjectsOfType(typeof(GameObject), true);
        foreach (GameObject obj in allObjects)
        {
            // ตรวจสอบว่า Object นี้ตรงกับเงื่อนไขที่คุณต้องการหรือไม่
            if (obj.tag == "Sliderbar")
            {
                healslider = obj;
                slider = obj.GetComponent<Slider>();
            }
        }
        slider = healslider.GetComponent<Slider>();
        slider.maxValue = maxHealingValue;
        slider.value = 0f; 
    }
    void Update()
    {   
        if (Input.GetKey(KeyCode.B))
        {   
            
            healslider.SetActive(true);
            isHealing = true;
            slider.value += drinkSpeed * Time.deltaTime;
            movementScript.StopMoving();
            if (slider.value >= maxHealingValue)
            {   
                playersanity.HealSanity(healAmount);
                slider.value = 0f;
                movementScript.ResumeMoving();
                isHealing = false;
                healslider.SetActive(false);
                if(inventoryPresentCharactor.GetTotalItemCountByName("Chemicalbottle") == 1)
                {
                Debug.Log(inventoryPresentCharactor.GetTotalItemCountByName("Chemicalbottle"));
                inventoryPresentCharactor.DeleteItemCharactorEquipment("InventoryChemicalbottle");
                }
                else if(inventoryPresentCharactor.GetTotalItemCountByName("Chemicalbottle") >= 2)
                {
                inventoryPresentCharactor.ManageReduceResource("Chemicalbottle");
                }
            }
        }
        else
        {
            if (isHealing)
            {
                // Cancel healing if the button is released before the slider reaches its maximum value.
                slider.value = 0f;
                isHealing = false;
                healslider.SetActive(false);
                movementScript.ResumeMoving(); // Resume player movement.
            }
        }
        
    }
    private void OnDestroy()
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
