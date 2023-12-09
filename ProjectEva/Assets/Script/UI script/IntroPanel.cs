using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;


public class IntroPanel : MonoBehaviour
{
    public TextMeshProUGUI infoText;
    public bool gamePaused = true;
    private bool canclose = false;

    void Start()
    {
        // Set the panel as active and pause the game
        SetPanelActive(true);
        StartCoroutine(StartCounting());
        PauseGame();
    }

    void Update()
    {
        // Check for any key press to resume the game
        if (gamePaused && Input.anyKeyDown && canclose)
        {
            ResumeGame();
            gamePaused = false;
        }
    }

    private void PauseGame()
    {
        // Pause the game by setting time scale to 0
        Time.timeScale = 0f;
    }
    private IEnumerator StartCounting()
    {
        yield return new WaitForSecondsRealtime(3f);
        canclose = true;
        
    }
    private void ResumeGame()
    {
        // Resume the game by setting time scale to 1
        Time.timeScale = 1f;

        // Deactivate the panel
        SetPanelActive(false);
    }

    public void SetPanelActive(bool isActive)
    {
        // Activate or deactivate the panel and its children
        gameObject.SetActive(isActive);
    }
}
