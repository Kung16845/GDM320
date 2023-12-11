using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavMeshPlus.Components;
using TMPro;
using System.Linq;

public class ForceandkeyDoor : MonoBehaviour
{
    public int PrefabID;
    private bool isPlayerNear = false;
    private string uiPanelTag = "Interactiontag";
    private string customTextTag = "Interactiontext";
    public int hasKeynumber;
    public InventoryPresentCharactor inventoryPresentCharactor;
    public GameObject sceneObject;
    public TextMeshProUGUI customText;
    public NavMeshSurface navMeshSurface;
    public SoundManager soundManager;
    public string Keyforthisdoor;
    public bool removekey;
    public int numberofkey;
    public string custominteractiontext;
    public void Awake()
    {
        FindUIElementsByTag();
        soundManager = FindObjectOfType<SoundManager>();
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
        navMeshSurface = FindAnyObjectByType<NavMeshSurface>();
    }
    private void Start()
    {
        FindUIElementsByTag();
        soundManager = FindObjectOfType<SoundManager>();
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
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

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && hasKeynumber == numberofkey)
        {
            if (removekey)
            {
                inventoryPresentCharactor.DeleteItemCharactorEquipment(Keyforthisdoor);
            }
            navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
            soundManager.PlaySound("Dooropen");
            Destroy(this.gameObject);
        }
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E) && hasKeynumber != numberofkey)
        {
            soundManager.PlaySound("Doorlocked");
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
    private void FindUIElementsByTag()
    {
        // Find UI panel by tag
        GameObject[] sceneObjects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.CompareTag(uiPanelTag)).ToArray();
        if (sceneObjects.Length > 0)
        {
            sceneObject = sceneObjects[0]; // Assuming there is only one UI panel with the specified tag
        }

        // Find custom text by tag
        TextMeshProUGUI[] customTexts = Resources.FindObjectsOfTypeAll<TextMeshProUGUI>().Where(obj => obj.CompareTag(customTextTag)).ToArray();
        if (customTexts.Length > 0)
        {
            customText = customTexts[0]; // Assuming there is only one custom text with the specified tag
        }
    }

    private void OnDestroy()
    {
        var datainScean = FindAnyObjectByType<SaveAndLoadScean>();
        var dataobj = datainScean.objectforload.FirstOrDefault(objid => objid.objectID == PrefabID);
        dataobj.isDestroy = true;
    }
}
