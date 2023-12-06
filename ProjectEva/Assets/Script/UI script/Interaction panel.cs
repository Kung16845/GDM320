using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Interactionpanel : MonoBehaviour
{
    private bool isPlayerNear = false;
    private string uiPanelTag = "Interactiontag"; 
    private string customTextTag = "Interactiontext";
    public GameObject sceneObject;
    public TextMeshProUGUI customText;
    public SoundManager soundManager;
    public string custominteractiontext;
    public void Awake()
    {
        FindUIElementsByTag();
    }
     private void Start()
    {
        FindUIElementsByTag();
        soundManager = FindObjectOfType<SoundManager>();;
    }
     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = true;
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
        customText.text = custominteractiontext;
    }

    private void HideEButton()
    {
        sceneObject.SetActive(false);
        customText.text = "";
    }
    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
        {
            soundManager.PlaySound("Doorlocked");
        }
    }
    private void FindUIElementsByTag()
    {
        // Find UI panel by tag
        GameObject[] sceneObjects = GameObject.FindGameObjectsWithTag(uiPanelTag);
        if (sceneObjects.Length > 0)
        {
            sceneObject = sceneObjects[0]; // Assuming there is only one UI panel with the specified tag
        }

        // Find custom text by tag
        GameObject[] customTexts = GameObject.FindGameObjectsWithTag(customTextTag);
        if (customTexts.Length > 0)
        {
            customText = customTexts[0].GetComponent<TextMeshProUGUI>(); // Assuming there is only one custom text with the specified tag
        }
    }
}
