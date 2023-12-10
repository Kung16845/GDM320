using UnityEngine;
using TMPro;
using System.Linq;
public class InteractionObject : MonoBehaviour
{
    public GameObject sceneObject; // The object to be displayed in the scene.
    private string uiPanelTag = "Interactiontag"; 
    private string customTextTag = "Interactiontext";
    public GameObject panel; // The panel to be displayed when 'E' is pressed.
    public SoundManager soundManager;
    public TextMeshProUGUI customText;
    public string custominteractiontext;

    private bool objectVisible = false;
    private bool panelVisible = false;

    public void Awake()
    {
        soundManager = FindAnyObjectByType<SoundManager>();
        FindUIElementsByTag();
    }
    private void Start()
    {
        FindUIElementsByTag();
        HideEButton();
        HidePanel();
        soundManager = FindObjectOfType<SoundManager>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowEButton();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideEButton();
        }
    }

    private void Update()
    {
        if (objectVisible && Input.GetKeyDown(KeyCode.E))
        {
            if (!panelVisible)
            {
                ShowPanel();
            }
            else
            {
                HidePanel();
            }
        }
    }

    private void ShowPanel()
    {
        soundManager.PlaySound("Paperaction");
        panel.SetActive(true);
        panelVisible = true;
    }

    private void HidePanel()
    {
        panel.SetActive(false);
        panelVisible = false;
    }
    private void ShowEButton()
    {
        sceneObject.SetActive(true);
        customText.text = custominteractiontext;
        objectVisible = true;
    }

    private void HideEButton()
    {
        sceneObject.SetActive(false);
        customText.text = "";
        objectVisible = false;
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
}
