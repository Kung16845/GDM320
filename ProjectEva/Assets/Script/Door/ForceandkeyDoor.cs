using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Components;
using TMPro;

public class ForceandkeyDoor : MonoBehaviour
{
    private bool isPlayerNear = false;
    public int hasKeynumber;
    public InventoryPresentCharactor inventoryPresentCharactor;
    public GameObject sceneObject;
    public TextMeshProUGUI customText;
    public NavMeshSurface navMeshSurface;
    public SoundManager soundManager;
    public string Keyforthisdoor;
    public int numberofkey;
    public string custominteractiontext;

    private void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShowEButton();
            isPlayerNear = true;
        }
        if (collision.CompareTag("Bullet"))
        {
            navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
            Destroy(this.gameObject); // Destroy the bullet.
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
                else if(hasKeynumber != numberofkey)
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

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && hasKeynumber == numberofkey)
        {
            navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
            Destroy(this.gameObject);
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
}
