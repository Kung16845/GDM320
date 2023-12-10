using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ReloadSceneOnTrigger2D : MonoBehaviour
{
    public GameObject panel;
    public TextMeshProUGUI messageText;

    void Start()
    {
        // Ensure the panel is initially inactive
        SetPanelActive(false);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the triggering object is the player (customize as needed)
        if (other.CompareTag("Player"))
        {
            // Show the panel with the reload option

            SetPanelActive(true);

            // Customize the message text
            Time.timeScale = 0f;
            messageText.text = "You've reached the end of demo.!";
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // Check if the triggering object is the player (customize as needed)
        if (other.CompareTag("Player"))
        {
            // Hide the panel when the player exits the trigger zone
            SetPanelActive(false);
        }
    }

    void SetPanelActive(bool isActive)
    {
        // Activate or deactivate the panel and its children
        panel.SetActive(isActive);
    }
}
