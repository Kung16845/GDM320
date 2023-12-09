using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Openinventoryboxfirsttime : MonoBehaviour
{
    public CanvasGroup introCanvasGroup;
    private bool firsttime;
    private void Start()
    {
        introCanvasGroup.alpha = 0f;
        introCanvasGroup = introCanvasGroup ?? GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (!firsttime)
        {
            introCanvasGroup.alpha = 1f;
            StartCoroutine(StartCounting());
            firsttime = true;
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
        Destroy(this.gameObject);
    }
}
