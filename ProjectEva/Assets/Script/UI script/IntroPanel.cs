using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IntroPanel : MonoBehaviour
{
    public TextMeshProUGUI infoText;
    private bool gamePaused = true;

    void Start()
    {
        // Set the panel as active and pause the game
        SetPanelActive(true);
        PauseGame();
    }

    void Update()
    {
        // Check for any key press to resume the game
        if (gamePaused && Input.anyKeyDown)
        {
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        // Pause the game by setting time scale to 0
        Time.timeScale = 0f;

        // Show a message in the TextMeshProUGUI text
        if (infoText != null)
        {
            infoText.text = "Game Paused\nPress any key to resume";
        }
    }

    private void ResumeGame()
    {
        // Resume the game by setting time scale to 1
        Time.timeScale = 1f;

        // Deactivate the panel
        SetPanelActive(false);
    }

    private void SetPanelActive(bool isActive)
    {
        // Activate or deactivate the panel and its children
        gameObject.SetActive(isActive);
    }
}
