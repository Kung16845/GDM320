using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount; // The amount of ammo this pickup adds.
    public GameObject sceneObject;
    private Pistol pistol;
    public SoundManager soundManager;
    private bool canPickup;

    private void Start()
    {
        canPickup = false;
        HideEButton();
        pistol = FindObjectOfType<Pistol>();
        soundManager = FindObjectOfType<SoundManager>();
    }
    private void Update()
    {
        // Check if the player can pick up the ammo and 'E' is pressed.
        if (canPickup && Input.GetKeyDown(KeyCode.E))
        {
            PickupAmmo();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ShowEButton();
            canPickup = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            HideEButton();
            canPickup = false;
        }
    }
    void PickupAmmo()
    {
        if (pistol != null)
            {
                // Increase the player's current ammo.
                soundManager.PlaySound("Pickupitem");
                pistol.currentAmmo += ammoAmount;
                // Destroy this pickup object.
                Destroy(gameObject);
            }
    }
    private void ShowEButton()
    {
        sceneObject.SetActive(true);
    }

    private void HideEButton()
    {
        sceneObject.SetActive(false);
    }
}
