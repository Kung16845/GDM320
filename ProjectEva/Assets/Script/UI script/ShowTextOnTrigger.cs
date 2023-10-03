using UnityEngine;
using TMPro;

public class ShowTextOnTrigger : MonoBehaviour
{
    public TextMeshProUGUI instructionText; // Reference to the TMP UI element in the Inspector.
    public string triggerMessage = "Press E to interact"; // Customizable trigger message.

    private bool isPlayerInRange = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            instructionText.text = triggerMessage;
            instructionText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            instructionText.gameObject.SetActive(false);
        }
    }

    // void Update()
    // {
    //     if (isPlayerInRange)
    //     {
    //         // Optionally, you can add logic here to respond to player input.
    //         // For example, check for input to interact with an object.
    //         // If the player interacts, you can execute the interaction logic here.
    //         // Example: if (Input.GetKeyDown(KeyCode.E)) { /* Your interaction code here */ }
    //     }
    // }
}
