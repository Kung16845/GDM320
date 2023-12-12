using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float gameDifficulty = 1.0f;
    public GameObject uIMenuGame;
    private bool isPaused = false;

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {   
                Cursor.visible = true;
                PauseGame();
                uIMenuGame.SetActive(true);
                isPaused = true;
            }
            else
            {
                ContinueGame();
                uIMenuGame.SetActive(false);
                isPaused = false;
                Cursor.visible = false;
            }
        }

    }
    public void PauseGame()
    {
        Time.timeScale = 0;

    }
    public void ContinueGame()
    {
        Time.timeScale = 1;

    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
