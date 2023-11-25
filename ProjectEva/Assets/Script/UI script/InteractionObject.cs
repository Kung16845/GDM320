using UnityEngine;
using TMPro;
public class InteractionObject : MonoBehaviour
{
    public GameObject sceneObject; // The object to be displayed in the scene.
    public GameObject panel; // The panel to be displayed when 'E' is pressed.
    public SoundManager soundManager;
    public TextMeshProUGUI customText;
    public string custominteractiontext;

    private bool objectVisible = false;
    private bool panelVisible = false;

    private void Start()
    {
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
}
