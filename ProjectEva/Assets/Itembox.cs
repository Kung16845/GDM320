using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
public class Itembox : MonoBehaviour
{
    private string uiPanelTag = "Interactiontag";
    private string customTextTag = "Interactiontext";
    public GameObject sceneObject;
    public SoundManager soundManager;
    public GameObject Motherinvent;
    public GameObject invent;
    public GameObject Chest;
    public GameObject Closeequipment;
    public TextMeshProUGUI customText;
    public string custominteractiontext;
    public bool isclose;
    public bool inventisopen;
    public CanvasGroup introCanvasGroup;
    private bool firsttime = false;
    private void Start()
    {
        inventisopen = false;
        isclose = false;
        FindUIElementsByTag();
        soundManager = FindObjectOfType<SoundManager>();
        introCanvasGroup.alpha = 0f;
        introCanvasGroup = introCanvasGroup ?? GetComponent<CanvasGroup>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isclose = true;
            ShowEButton();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        { 
            isclose = false;
            HideEButton();
        }
    }
    void Update()
    {
        if(isclose && Input.GetKeyDown(KeyCode.E))
        {
            if(!inventisopen)
            {
            if (!firsttime)
            {
            introCanvasGroup.alpha = 1f;
            StartCoroutine(StartCounting());
            firsttime = true;
            }
            sceneObject.SetActive(false);
            Motherinvent.SetActive(true);
            invent.SetActive(true);
            Chest.SetActive(true);
            Closeequipment.SetActive(false);
            inventisopen = true;
            }
            else
            {
                Chest.SetActive(false);
                Closeequipment.SetActive(true);
                Motherinvent.SetActive(false);
                inventisopen = false;
            }
        }
    }
    private void ShowEButton()
    {
        sceneObject.SetActive(true);
        customText.text = custominteractiontext;
    }

    private void HideEButton()
    {
        invent.SetActive(true);
        Chest.SetActive(false);
        Closeequipment.SetActive(true);
        Motherinvent.SetActive(false);
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
    IEnumerator StartCounting()
    {
        yield return new WaitForSecondsRealtime(7f);

        // Gradually decrease alpha over 1 second
        float fadeDuration = 3f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            introCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the alpha is 0 when fading is complete
        introCanvasGroup.alpha = 0f;
    }
}
