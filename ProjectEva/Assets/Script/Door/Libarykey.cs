using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Libarykey : MonoBehaviour
{
    public keyinventory inventory;
    public SoundManager soundManager;
    public GameObject sceneObject;
    private bool canPickup;

    private void Start()
    {
        // Use GetComponentInParent to find the keyinventory script on the player or any parent GameObject.
        HideEButton();
        canPickup = false;
        inventory = GameObject.FindWithTag("inventory").GetComponent<keyinventory>();
        soundManager = FindObjectOfType<SoundManager>();
    }
    private void Update()
    {
        // Check if the player can pick up the ammo and 'E' is pressed.
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            keypickup();
        }
    }
     private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowEButton();
            canPickup = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideEButton();
            canPickup = false;
        }
    }
    void keypickup()
    {
        inventory.AddKey("Libarykey");
        soundManager.PlaySound("Pickupkey");
        Destroy(this.gameObject); // Remove the collected key from the scene.
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
