using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Hp : MonoBehaviour
{
    public float maxhp;
    public float currenthp;
    public GameObject player;
    public TextMeshProUGUI healthText;
    public CanvasGroup canvasGroup; // Reference to the CanvasGroup component.
    public float lowHpThreshold = 30f; // HP threshold for triggering the alpha change.
    public ReloadSceneOnZeroHP reloadScript;

    private SanityScaleController sanityScaleController;
    private float timeRemaining = 3f; // Time remaining before reloading the scene.
    private bool isReloading = false;

    void Start()
    {
        currenthp = maxhp;
        sanityScaleController = FindObjectOfType<SanityScaleController>();
        canvasGroup.alpha = 0f; // Assuming this script is attached to the same GameObject as the CanvasGroup.
    }
    
    void Update()
    {
        UpdateHealthText();

        // Check if the current HP is below the low HP threshold.
        if (currenthp < lowHpThreshold)
        {
            // Decrease the alpha of the CanvasGroup.
            canvasGroup.alpha = 0.1f; // You can adjust the alpha value as needed.
        }
        else
        {
            // Reset the alpha to full when HP is above the threshold.
            canvasGroup.alpha = 0f;
        }

        if (currenthp <= 0f && !isReloading)
        {
            isReloading = true;
            reloadScript.StartReloadScene(); // Call the function in the ReloadSceneOnZeroHP script.
        }
    }

    public void TakeDamage(float damage)
    {
        if (currenthp > 0)
        {
            currenthp -= damage * sanityScaleController.GetDamageScale();
            // currenthp = Mathf.Clamp(currenthp, 0, maxhp);
        }
        else
        {
            // Call the StartReloadScene function when HP reaches 0.
            reloadScript.StartReloadScene();
        }
    }

    public void HealHp(float healAmount)
    {
        currenthp += healAmount;
        currenthp = Mathf.Clamp(currenthp, 0, maxhp);
    }
    
    void UpdateHealthText()
    {
        healthText.text = "HP: " + currenthp.ToString("F2");
    }

    void StartReloadSceneTimer()
    {
        InvokeRepeating("UpdateTimer", 1f, 1f);
    }

    void UpdateTimer()
    {
        timeRemaining -= 1f;

        if (timeRemaining <= 0f)
        {
            // Reload the scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
