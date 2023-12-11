using UnityEngine;
using TMPro;
using System.Linq;
using NavMeshPlus.Components;

public class ForceDoor : MonoBehaviour
{
    public int PrefabID;
    public GameObject sceneObject;
    public TextMeshProUGUI customText;
    private string uiPanelTag = "Interactiontag"; 
    private string customTextTag = "Interactiontext";
    public SoundManager soundManager;
    public string custominteractiontext;
    public NavMeshSurface navMeshSurface;
    private bool isPlayerNear = false;

    public void Awake()
    {
        FindUIElementsByTag();
        soundManager = FindObjectOfType<SoundManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = true;
            customText.text = custominteractiontext;
            ShowEButton();
        }
        if (collision.CompareTag("Bullet"))
        {
            // soundManager = ;
            navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData);
            Destroy(this.gameObject); // Destroy the bullet.
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
