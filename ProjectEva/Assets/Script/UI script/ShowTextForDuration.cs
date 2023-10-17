using UnityEngine;
using TMPro;

public class ShowTextForDuration : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // Reference to the TMP UI element in the Inspector.
    public float displayDuration = 1.0f; // Customizable display duration in seconds.

    private float displayTimer; // Timer to keep track of display time.
    private bool isDisplaying; // Flag to indicate if the text is currently displayed.

    void Start()
    {
        // Initialize the timer and set the text to inactive.
        displayTimer = 0f;
        isDisplaying = false;

        // You can optionally hide the text at the start.
        textMeshPro.gameObject.SetActive(false);

        // Call a method to start displaying the text.
        StartDisplayingText();
    }

    void Update()
    {
        if (isDisplaying)
        {
            // Update the timer while the text is displayed.
            displayTimer += Time.deltaTime;

            // Check if it's time to hide the text.
            if (displayTimer >= displayDuration)
            {
                StopDisplayingText();
            }
        }
    }

    void StartDisplayingText()
    {
        // Set the text to active and reset the timer.
        textMeshPro.gameObject.SetActive(true);
        displayTimer = 0f;
        isDisplaying = true;
    }

    void StopDisplayingText()
    {
        // Set the text to inactive and reset the timer.
        textMeshPro.gameObject.SetActive(false);
        displayTimer = 0f;
        isDisplaying = false;
    }
}
