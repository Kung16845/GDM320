using UnityEngine;
using TMPro;

public class ShowPanelForDuration : MonoBehaviour
{
    public GameObject panel; // Reference to the panel GameObject in the Inspector.
    
    private bool isDisplaying; // Flag to indicate if the panel is currently displayed.

    void Start()
    {
        isDisplaying = false;
        panel.SetActive(false);
        StartDisplayingPanel();
    }

    void Update()
    {
        if (isDisplaying)
        {
            // Check for a mouse click to continue.
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StopDisplayingPanel();
            }
        }
        else
        {
            // Resume normal scene operations here.
        }
    }

    void StartDisplayingPanel()
    {
        // Pause scene by setting Time.timeScale to 0.
        Time.timeScale = 0;
        
        // Set the panel to active.
        panel.SetActive(true);
        isDisplaying = true;
    }

    void StopDisplayingPanel()
    {
        // Resume scene by setting Time.timeScale to 1.
        Time.timeScale = 1;
        
        // Set the panel to inactive.
        panel.SetActive(false);
        isDisplaying = false;
    }
}
