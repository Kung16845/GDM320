using UnityEngine;
using UnityEngine.UI;

public class Bandage : MonoBehaviour
{
    public float healingSpeed = 20f; // Speed at which the healing slider fills up.
    public float maxHealingValue = 100f; // Maximum value the healing slider can reach.
    public float healAmount = 70f; // Amount to heal the player when the slider reaches its maximum value.
    public Hp playerHp; // Reference to the player's HP script.
    public NewMovementPlayer movementScript;
    public Slider slider; // Reference to the Slider component.
    private bool isHealing = false; // Flag to track if healing is in progress.
    public GameObject healslider;
    public InventoryPresentCharactor inventoryPresentCharactor;
    private void Awake()
    {
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
    }
    void Start()
    {
        slider.maxValue = maxHealingValue;
        slider.value = 0f;
        playerHp = FindObjectOfType<Hp>();
        movementScript = FindObjectOfType<NewMovementPlayer>();    
        healslider.SetActive(false);
    }

    void Update()
    {   
        
        if(Input.GetKey(KeyCode.D))
            inventoryPresentCharactor.DeleteItemCharactorEquipment();
        if (Input.GetKey(KeyCode.B))
        {   
            
            healslider.SetActive(true);
            isHealing = true;
            slider.value += healingSpeed * Time.deltaTime;
            movementScript.StopMoving();
            if (slider.value >= maxHealingValue)
            {
                // Heal the player and reset the slider.
                playerHp.HealHp(healAmount);
                slider.value = 0f;
                movementScript.ResumeMoving();
                isHealing = false;
                healslider.SetActive(false);
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
