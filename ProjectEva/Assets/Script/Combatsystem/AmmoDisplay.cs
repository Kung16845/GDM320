using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoDisplay : MonoBehaviour
{
    public TextMeshProUGUI ammoText; // Reference to the UI text element where you want to display the ammo count.
    public Pistol playerPistol; // Reference to the Pistol script of the player.
    public float uiDisappearTime = 5.0f; // Time in seconds before the UI disappears.
    public float uiDisappearSpeed = 1.0f; // Speed at which the UI becomes transparent when disappearing.
    public InventoryPresentCharactor inventoryPresentCharactor;
    private float lastActionTime; // Time of the last shot or reload action by the player.
    private bool isUIVisible = true; // Flag to track the visibility of the UI.
    private CanvasGroup canvasGroup; // Reference to the CanvasGroup component for UI fading.
    public int currentAmmo;

    void Start()
    {
        lastActionTime = Time.time; // Initialize the last action time.
        canvasGroup = GetComponent<CanvasGroup>(); // Get the CanvasGroup component.
    }

    void Update()
    {
        currentAmmo = inventoryPresentCharactor.GetTotalItemCountByName("Pistol Ammo");
        if (ammoText != null && playerPistol != null)
        {
            // Update the text to display the player's ammo count.
            ammoText.text = "Ammo: " + playerPistol.ammoInChamber + " / " + currentAmmo;

            // Check if the UI should disappear.
            if (Time.time - lastActionTime >= uiDisappearTime && isUIVisible)
            {
                // Start fading out the UI.
                FadeOutUI();
            }

            // Check if the player is aiming (right mouse button) or reloading to make the UI reappear.
            if (Input.GetMouseButton(1) || playerPistol.isReloading)
            {
                // Instantly make the UI appear.
                MakeUIVisible();
            }
            // Check if the player shoots (left mouse button) to make the UI reappear.
            else if (Input.GetMouseButtonDown(0))
            {
                // Instantly make the UI appear.
                MakeUIVisible();
            }
        }
    }

    void FadeOutUI()
    {
        if (canvasGroup != null)
        {
            // Calculate the new alpha value based on the uiDisappearSpeed.
            float newAlpha = Mathf.Clamp01(canvasGroup.alpha - uiDisappearSpeed * Time.deltaTime);

            // Update the CanvasGroup's alpha.
            canvasGroup.alpha = newAlpha;

            // If the alpha reaches 0, disable the UI text and set the UI visibility flag to false.
            if (canvasGroup.alpha == 0)
            {
                ammoText.enabled = false;
                isUIVisible = false;
            }
        }
    }

    void MakeUIVisible()
    {
        if (canvasGroup != null)
        {
            // Make the UI text instantly visible.
            ammoText.enabled = true;
            isUIVisible = true;
            
            // Set the CanvasGroup's alpha to 1 (fully visible).
            canvasGroup.alpha = 1;
        }
    }
}
