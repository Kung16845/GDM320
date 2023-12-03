using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float gameDifficulty = 1.0f;
    public IntroPanel introPanel;
    public  GameObject intro;
    void Update()
    {
        if (!introPanel.gamePaused && Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("called");
            intro.SetActive(true);
            introPanel.gamePaused = true;
        }
    }
}
