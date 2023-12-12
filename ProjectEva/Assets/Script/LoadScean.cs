using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;
using System.IO;

public class LoadScene : MonoBehaviour
{
    public bool isNewScene;
    public bool isLoadScene;
    [SerializeField] string savePlayerAndEnemyPath;

    private void Start()
    {
        // Make sure the saveManager persists between scenes
        DontDestroyOnLoad(this.gameObject);
    }

    public void LoadDataNextScene()
    {
        // Check if the save file exists and contains data
        if (CheckSaveFileExistsAndNotEmpty())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            isLoadScene = true;
        }
        else
        {
            Debug.LogError("Cannot load the next scene because the save file is empty or does not exist.");
        }
    }

    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        isNewScene = true;
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    private bool CheckSaveFileExistsAndNotEmpty()
    {
        var dataPath = Application.dataPath;
        var targetFilePath = Path.Combine(dataPath, savePlayerAndEnemyPath);

        if (File.Exists(targetFilePath))
        {
            var dataJson = File.ReadAllText(targetFilePath);
            return !string.IsNullOrEmpty(dataJson);
        }

        return false;
    }
}
