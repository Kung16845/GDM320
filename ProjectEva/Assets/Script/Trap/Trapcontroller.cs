using UnityEngine;

public class TrapController : MonoBehaviour
{
    public NewMovementPlayer movementScript; // Reference to the player's movement script
    public GameObject trapSlider; // Reference to the GameObject containing the TrapSlider
    public bool stuck; 

    private void Start()
    {
        stuck = false;
        // Find the player's movement script in the scene
        movementScript = FindObjectOfType<NewMovementPlayer>();

        // Disable the trapSlider GameObject initially
        trapSlider.SetActive(false);

        if (movementScript == null)
        {
            Debug.LogError("No movement script found. Make sure to assign it or ensure it's in the scene.");
        }
    }

    // Function to be called when the player is hit by a trap
    public void PlayerHitByTrap()
    {
        // Stop the player's movement
        stuck = true;
        trapSlider.SetActive(true);
        movementScript.StopMoving();
    }

    public void PlayerReleaseTrap()
    {
        stuck = false;
        movementScript.ResumeMoving();
        trapSlider.SetActive(false);
    }
}
