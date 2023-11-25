using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Pocketpickup : MonoBehaviour
{
    // Start is called before the first frame update
    public InventoryPresentCharactor inventoryPresentCharactor;
    public GameObject sceneObject;
    public SoundManager soundManager;
    private bool canPickup;
    public string Soundname;
    public TextMeshProUGUI customText;
    public string custominteractiontext;
    void Start()
    {
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
        sceneObject.SetActive(false);
    }
    void Update()
    {
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {   
            inventoryPresentCharactor.UnlockSlot();
            soundManager.PlaySound(Soundname);
            customText.text = "";
            Destroy(this.gameObject);
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

}
