using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadScene: MonoBehaviour
{
    public SaveManager saveManager;
    public void LoadDataNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
       LoadDataAfterSceneLoaded();
    }
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void LoadDataAfterSceneLoaded()
    {
        StartCoroutine(LoadDataCoroutine());
    }

    private IEnumerator LoadDataCoroutine()
    {
        yield return new WaitForSeconds(2f); // Wait for a short time to ensure the scene is loaded

        // Now, you can safely call saveManager.AllLoad() after the new scene has been loaded
        saveManager.AllLoad();
    }
}