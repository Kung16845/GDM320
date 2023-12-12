using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene : MonoBehaviour
{
    public bool isNewScean;
    public bool isLoadScean;
    private void Start()
    {
        // Make sure the saveManager persists between scenes
        DontDestroyOnLoad(this.gameObject);
    }
    public void LoadDataNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        isLoadScean = true;
        
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        isNewScean = true;
    }
    public void ExitGame()
    {
        Application.Quit();
    }

}