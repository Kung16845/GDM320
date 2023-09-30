using UnityEngine;
using TMPro;

public class ForceDoor : MonoBehaviour
{
    public TextMeshProUGUI instructionText; // Reference to the TMPro UI object.

    private bool isPlayerNear = false;

    private void Start()
    {
        instructionText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = true;
            instructionText.gameObject.SetActive(true);
            instructionText.text = "The lock look weak maybe I can be able to break it.";
        }
        if (collision.CompareTag("Bullet"))
        {
            // Check if a bullet collided with the door.
            // Add any additional conditions here, e.g., bullet type.
            Destroy(this.gameObject); // Destroy the bullet.
            OpenDoorWithForce(); // Open the door when hit by a bullet.
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isPlayerNear = false;
            instructionText.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (isPlayerNear)
        {
            // Open the door using force (without a key).
            // Add your door opening logic here.
            OpenDoorWithForce();
        }
    }


    private void OpenDoorWithForce()
    {
        // Add your door opening logic here for force-only doors.
    }
}
