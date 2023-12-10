using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Healingsection : MonoBehaviour
{
    public CanvasGroup introCanvasGroup;
    public Hp hp;
    private bool firsttime = false;

    private void Start()
    {
        introCanvasGroup.alpha = 0f;
        hp = FindAnyObjectByType<Hp>();
        introCanvasGroup = introCanvasGroup ?? GetComponent<CanvasGroup>();
    }

    private void Update()
    {
        if (!firsttime && (hp.currenthp <= 30))
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
