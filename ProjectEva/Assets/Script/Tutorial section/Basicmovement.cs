using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class BasicMovement : MonoBehaviour
{
    public CanvasGroup introCanvasGroup;
    private bool canClose = false;

    private void Start()
    {
        introCanvasGroup.alpha = 0f;
        introCanvasGroup = introCanvasGroup ?? GetComponent<CanvasGroup>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            introCanvasGroup.alpha = 1f;
            StartCoroutine(StartCounting());
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

    IEnumerator StartCounting()
    {
        yield return new WaitForSecondsRealtime(10f);

        // Gradually decrease alpha over 1 second
        float fadeDuration = 4f;
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
}
