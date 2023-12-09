using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using System.Linq;

public class Triggerevent : MonoBehaviour
{
    public SoundManager soundManager;
    public bool canPickup;
    public GameObject Destroyspriteofficer;
    public GameObject enebleofficer;
    public GameObject enableblood;
    public GameObject Destroydoor; 
    public CanvasGroup introCanvasGroup;
    private bool canClose = false;
    private void Awake()
    {
        canPickup = false;
        introCanvasGroup.alpha = 0f;
        introCanvasGroup = introCanvasGroup ?? GetComponent<CanvasGroup>();
        soundManager = FindObjectOfType<SoundManager>();
    }
    private void Start()
    {
        canPickup = false;
        introCanvasGroup.alpha = 0f;
        introCanvasGroup = introCanvasGroup ?? GetComponent<CanvasGroup>();
        soundManager = FindObjectOfType<SoundManager>();
    }
    private void Update()
    {
        if(canPickup && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("destroyed");
            Destroy(Destroyspriteofficer.gameObject);
            Destroy(Destroydoor.gameObject);
            enebleofficer.SetActive(true);
            enableblood.SetActive(true);
            
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
    // IEnumerator Playsound()
    // {
    //     soundManager.PlaySound();
    //     yield return new WaitForSeconds(1);
    //     soundManager.PlaySound();
    //     yield return new WaitForSeconds(1);

    // }
}
