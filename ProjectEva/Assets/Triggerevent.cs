using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;


public class Triggerevent : MonoBehaviour
{
    public SoundManageronce2 soundManager;
    public bool canPickup;
    public GameObject Destroyspriteofficer;
    public GameObject enebleofficer;
    public GameObject enableblood;
    public GameObject enableblood1;
    public GameObject enableblood2;
    public GameObject Destroydoor; 
    public GameObject Destroysound; 
    public Transform newTransform; 
    public CanvasGroup introCanvasGroup;
    public string canvatag;
    private bool canClose = false;
    private void Awake()
    {
        canPickup = false;
        Destroydoor = GameObject.FindWithTag("Destroydoor");
        Destroyspriteofficer = GameObject.FindWithTag("policesit");
        enableblood = GameObject.FindWithTag("Bloodtranfrom");
        enableblood1 = GameObject.FindWithTag("Bloodtranfrom1");
        enableblood2 = GameObject.FindWithTag("Bloodtranfrom2");
        Destroysound = GameObject.FindWithTag("eventsound");
        enebleofficer = GameObject.FindWithTag("1"); // Corrected tag
        enebleofficer.SetActive(false);
        GameObject foundObject = GameObject.FindWithTag(canvatag);
        enableblood.SetActive(false);
        enableblood1.SetActive(false);
        enableblood2.SetActive(false);
        // Check if the object is found
        if (foundObject != null)
        {
            // Get the CanvasGroup component from the found object
            introCanvasGroup = foundObject.GetComponent<CanvasGroup>();
            introCanvasGroup.alpha = 0f;
        }
        soundManager = FindObjectOfType<SoundManageronce2>();
    }
    // private void Start()
    // {
    //     canPickup = false;
    //     introCanvasGroup.alpha = 0f;
    //     Destroydoor = GameObject.FindWithTag("Destroydoor");
    //     Destroyspriteofficer = GameObject.FindWithTag("policesit");
    //     enableblood = GameObject.FindWithTag("Bloodtranfrom");
    //     enableblood1 = GameObject.FindWithTag("Bloodtranfrom1");
    //     enableblood2 = GameObject.FindWithTag("Bloodtranfrom2");
    //     Destroysound = GameObject.FindWithTag("eventsound");
    //     enebleofficer = GameObject.FindWithTag("1"); // Corrected tag
    //     enebleofficer.SetActive(false);
    //     introCanvasGroup = introCanvasGroup ?? GetComponent<CanvasGroup>();
    //     soundManager = FindObjectOfType<SoundManageronce2>();
    // }
    private void Update()
    {
        if(canPickup && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("destroyed");
            enebleofficer.SetActive(true);
            
            enableblood.SetActive(true);
            enableblood1.SetActive(true);
            enableblood2.SetActive(true);
            Destroy(Destroydoor);
            Destroy(Destroyspriteofficer);
            StartCoroutine(Playsound());
            Destroy(this.gameObject);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;
        }
    }
    IEnumerator StartCounting()
    {
        yield return new WaitForSecondsRealtime(5f);

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

        canClose = true;
    }
    IEnumerator Playsound()
    {
        soundManager.PlaySound("Officer hurt");
        soundManager.PlaySound("Doorbroke");
        yield return new WaitForSeconds(1.5f);
        Destroy(Destroysound);
    }
}
