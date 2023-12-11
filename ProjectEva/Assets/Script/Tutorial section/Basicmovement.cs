using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class BasicMovement : MonoBehaviour
{
    public CanvasGroup introCanvasGroup;
    public string canvatag;
    public int tutorialduratuion;
    private bool canClose = false;

    private void Start()
    {
        // Find the GameObject with the specified tag
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            introCanvasGroup.alpha = 1f;
            StartCoroutine(FadeInCanvas());
        }
    }

    private void Update()
    {
        if (canClose)
        {
            introCanvasGroup.alpha = 0f; // Set alpha to 0 when closing
            canClose = false;
            Destroy(this.gameObject);
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

        // Wait for the specified duration
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
        canClose = true;
    }
}
