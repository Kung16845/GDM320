using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sanitybottle : MonoBehaviour
{
    public float drinkSpeed = 80f; // Speed at which the healing slider fills up.
    public float maxHealingValue = 100f; // Maximum value the healing slider can reach.
    public float healAmount = 80f; // Amount to heal the player when the slider reaches its maximum value.
    public Sanity playersanity; // Reference to the player's HP script.
    public NewMovementPlayer movementScript;
    public Slider slider; // Reference to the Slider component.
    private bool isHealing = false; // Flag to track if healing is in progress.
    public GameObject healslider;
    public InventoryPresentCharactor inventoryPresentCharactor;

    private void Awake()
    {
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
        playersanity = FindObjectOfType<Sanity>();
        movementScript = FindObjectOfType<NewMovementPlayer>();    
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
                // Heal the player and reset the slider.
                playersanity.HealSanity(healAmount);
                slider.value = 0f;
                movementScript.ResumeMoving();
                isHealing = false;
                healslider.SetActive(false);
                inventoryPresentCharactor.DeleteItemCharactorEquipment("Sanitybottle");
                Destroy(this.gameObject);
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
}
