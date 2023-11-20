using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Webtrap : MonoBehaviour
{
    public TrapController trapController;
    public GameObject sceneObject;
    public bool hasmatches;
    public InventoryPresentCharactor inventoryPresentCharactor;
    void Start() 
    {
        trapController = FindObjectOfType<TrapController>();
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
        hasmatches = false;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Leg"))
        {
            trapController.HitbyWebtrap();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Leg"))
        {
            trapController.PlayerReleaseTrap();
        }
    }
    void Update()
    {
        if(hasmatches)
        {
            ShowEButton();
            if (Input.GetKeyDown(KeyCode.E))
            {
                Destroy(this.gameObject);
                inventoryPresentCharactor.ManageReduceResource("Matches");
            }
        }
        else
        {
            HideEButton();
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
