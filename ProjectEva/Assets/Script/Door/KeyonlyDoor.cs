using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using NavMeshPlus.Components;

public class KeyonlyDoor : MonoBehaviour
{
    private bool isPlayerNear = false;
    public int hasKeynumber;
    public InventoryPresentCharactor inventoryPresentCharactor;
    public GameObject sceneObject;
    public TextMeshProUGUI customText;
    public SoundManager soundManager;
    public string Keyforthisdoor;
    public NavMeshSurface navMeshSurface;
    public int numberofkey;
    public string custominteractiontext;
    private void Start()
    {
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (isPlayerNear)
            {
                if (hasKeynumber == numberofkey)
                {
                    customText.text = "I have a key for this.";
                }
                else if (hasKeynumber != numberofkey)
                {
                    customText.text = custominteractiontext;
                }
                ShowEButton();
            }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            HideEButton();
            isPlayerNear = false;
        }
    }
    private void ShowEButton()
    {
        sceneObject.SetActive(true);
    }

    private void HideEButton()
    {
        sceneObject.SetActive(false);
        customText.text = "";
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && hasKeynumber == numberofkey)
        {
            inventoryPresentCharactor.DeleteItemCharactorEquipment(Keyforthisdoor);
            soundManager.PlaySound("Dooropen");
            navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
            Destroy(this.gameObject);
        }
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && hasKeynumber == numberofkey)
        {
            soundManager.PlaySound("Doorlocked");
        }
    }
}
