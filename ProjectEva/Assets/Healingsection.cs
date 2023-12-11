using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Healingsection : MonoBehaviour
{
    public CanvasGroup introCanvasGroup;
    public GameObject destroycanva;
    public Hp hp;
    public int tutorialduratuion;
    public string canvatag;
    private bool firsttime = false;

    private void Start()
    {
        hp = FindObjectOfType<Hp>();
        GameObject foundObject = GameObject.FindWithTag(canvatag);

        // Check if the object is found
        if (foundObject != null)
        {
            // Get the CanvasGroup component from the found object
            introCanvasGroup = foundObject.GetComponent<CanvasGroup>();
            introCanvasGroup.alpha = 0f;
        }
        else
        {
            Debug.LogError("Object with tag not found.");
        }
    }

    private void Update()
    {
        if (!firsttime && (hp.currenthp <= 30))
        {
            introCanvasGroup.alpha = 1f;
            StartCoroutine(FadeInCanvas());
            firsttime = true;
        }
    }

    IEnumerator FadeInCanvas()
    {
        float fadeDuration = 3f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            introCanvasGroup.alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the alpha is 1 when fading is complete
        introCanvasGroup.alpha = 1f;

        // Wait for 7 seconds
        yield return new WaitForSecondsRealtime(tutorialduratuion);

        // Gradually decrease alpha over 1 second
        elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            introCanvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the alpha is 0 when fading is complete
        introCanvasGroup.alpha = 0f;
        Destroy(this.gameObject);
    }
}
