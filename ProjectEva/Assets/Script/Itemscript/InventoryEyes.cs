using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using TMPro;

public class InventoryEyes : MonoBehaviour
{
     public float CraftingSpeed = 70f;
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
    public Headlessquest headless;

    // Serialized field for the prefab path
    [SerializeField]
    private string itemPrefabName = "Headlesswitheye";

    private void Awake()
    {
        FindUIElementsByTag();
        soundManager = FindObjectOfType<SoundManager>();
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
        headless = FindObjectOfType<Headlessquest>();
        movementScript = FindObjectOfType<NewMovementPlayer>();
        headless.iseyeequip = true;
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
    }

    void Update()
    {
        sceneObject.SetActive(true);
        if(inventoryPresentCharactor.GetTotalItemCountByName("Maindenhead") == 0)
        {
        customText.text = "I think I need to find its Head to equip eyes.";
        }
        else if(inventoryPresentCharactor.GetTotalItemCountByName("Maindenhead") == 1)
        {
        customText.text = "I can equip the eyes now to the head.";
        }
        if (Input.GetKey(KeyCode.G) && inventoryPresentCharactor.GetTotalItemCountByName("Maindenhead") == 1)
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
                inventoryPresentCharactor.ManageReduceResource("Maindenhead");
                inventoryPresentCharactor.DeleteItemCharactorEquipment("InventoryEyes");
                GameObject prefab = Resources.Load<GameObject>("Prefeb/item/" + itemPrefabName);
                Debug.Log("Load path: " + "Prefeb/item/" + itemPrefabName);

                // Instantiate the prefab
                Instantiate(prefab, transform.position, Quaternion.identity);
                Destroy(this.gameObject);
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

    private void OnDestroy()
    {
        sceneObject.SetActive(false);
        headless.iseyeequip = false;
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
