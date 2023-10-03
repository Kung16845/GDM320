using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int ammoAmount; // The amount of ammo this pickup adds.
    private Pistol pistol;

    private void Start()
    {
        pistol = FindObjectOfType<Pistol>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Pickup the ammo");
            if (pistol != null)
            {
                // Increase the player's current ammo.
                pistol.currentAmmo += ammoAmount;

                // Destroy this pickup object.
                Destroy(gameObject);
            }
        }
    }
}
