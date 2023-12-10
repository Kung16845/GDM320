using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class BasicMovement : MonoBehaviour
{
    public CanvasGroup introCanvasGroup;
    public GameObject  destroycanva;
    public int tutorialduratuion;
    private bool canClose = false;

    private void Start()
    {
        destroycanva.SetActive(true);
        introCanvasGroup.alpha = 0f;
        introCanvasGroup = introCanvasGroup ?? GetComponent<CanvasGroup>();
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
            Destroy(destroycanva);
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

        Destroy(destroycanva);
        Destroy(this.gameObject);
    }
}
