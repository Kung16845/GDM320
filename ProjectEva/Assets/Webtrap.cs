using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Webtrap : MonoBehaviour
{
    public TrapController trapController;
    public GameObject sceneObject;
    public bool Canburn;
    public bool isclose;
    public InventoryPresentCharactor inventoryPresentCharactor;
    void Start() 
    {
        trapController = FindObjectOfType<TrapController>();
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
        Canburn = false;
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Leg"))
        {
            isclose = true;
            trapController.HitbyWebtrap();
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Leg"))
        {   
            isclose = false;
            trapController.PlayerReleaseTrap();
        }
    }
    void Update()
    {
        if(Canburn && isclose)
        {
            ShowEButton();
            if (Input.GetKeyDown(KeyCode.E))
            {
                inventoryPresentCharactor.DeleteItemCharactorEquipment();
                Destroy(this.gameObject);
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
