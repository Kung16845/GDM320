using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class ReloadSceneOnZeroHP : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public float countdownDuration = 5f;
    private bool isReloading = false;

    private void Start()
    {
        countdownText.text = string.Empty;
    }

    public void StartReloadScene()
    {
        if (!isReloading)
        {
            isReloading = true;
            StartCoroutine(ReloadSceneCoroutine());
        }
    }


    IEnumerator ReloadSceneCoroutine()
    {
        float timeLeft = countdownDuration;

        while (timeLeft > 0)
        {
            countdownText.text = "You Died " + timeLeft.ToString("F0") ;
            yield return new WaitForSeconds(1f);
            timeLeft -= 1f;
        }

        // Reload the scene
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
