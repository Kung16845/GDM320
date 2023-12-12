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
        uIMenuGame.SetActive(false);
        Cursor.visible = false;
        isPaused = false;
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
