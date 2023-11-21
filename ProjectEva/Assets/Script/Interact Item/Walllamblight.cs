using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walllamblight : MonoBehaviour
{
    public GameObject sceneObject;
    public bool lightquip;
    public bool fuelequip;
    public bool fuelrefill;
    public WalllampDuration walllampDuration;
    public bool isclose;
    public InventoryPresentCharactor inventoryPresentCharactor;
    public SoundManager soundManager;
    private void Start()
    {
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
            ShowEButton();
            isclose = true;
        }
    }
    private void lightthelamp()
    {
        inventoryPresentCharactor.ManageReduceResource("Matches");
        walllampDuration.lightup = true;
    }
    private void Refillfuel()
    {
        fuelrefill = true;
        inventoryPresentCharactor.ManageReduceResource("Alcoholbottom");
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideEButton();
            isclose = false;
        }
    }
    private void ShowEButton()
    {
        sceneObject.SetActive(true);
    }

    private void HideEButton()
    {
        sceneObject.SetActive(false);
    }
}

