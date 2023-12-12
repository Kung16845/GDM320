using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class ReloadSceneOnZeroHP : MonoBehaviour
{
    public NewMovementPlayer PlayerMovement;
    public SaveManager saveAndLoadScean;
    public GameObject uIDie;
    public SaveManager saveManager;
    void Start()
    {
        uIDie.SetActive(false);
    }
    public void DeadUI()
    {
        uIDie.SetActive(true);
        Cursor.visible = true;
    }
    public void loadcurrentscene()
    {
        // saveManager.LoadScene(saveManager.GetActiveScene().buildIndex -1); 
    }
}
