using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Webtrap : MonoBehaviour
{
    public TrapController trapController;
    public GameObject sceneObject;
    public TextMeshProUGUI customText;
    public string custominteractiontext;
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
            customText.text = "I can burn this web.";
            ShowEButton();
            if (Input.GetKeyDown(KeyCode.E))
            {
                if(inventoryPresentCharactor.GetTotalItemCountByName("Matches") >= 2)
                {
                    Debug.Log("reduce by 1");
                inventoryPresentCharactor.ManageReduceResource("Matches");
                }
                else if(inventoryPresentCharactor.GetTotalItemCountByName("Matches") == 1)
                {
                    Debug.Log("Delete");
                inventoryPresentCharactor.DeleteItemCharactorEquipment("InventoryMatches");
                }
                Destroy(this.gameObject);
            }
        }
        else if (!Canburn && isclose)
        {
            customText.text = "Gotta find somethings to burn this.";
            ShowEButton();
        }
        else
        {
            HideEButton();
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
