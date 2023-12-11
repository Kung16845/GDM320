using System.Collections;
using UnityEngine;

public class Trapactivation : MonoBehaviour
{
    // Array of Webtrap objects to activate
    public Webtrap[] objectsToActivate;
    
    // Number of objects to activate (changeable)
    public int numberOfObjectsToActivate = 2;

    void Awake()
    {
        Findwebtrap();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider is the player (you can adjust the tag or use a different condition)
        if (other.CompareTag("Player"))
        {
            // Start coroutine to activate random objects
            StartCoroutine(ActivateRandomObjects());
            
            // Destroy this object after activation
            Destroy(this.gameObject);
        }
    }

    private IEnumerator ActivateRandomObjects()
    {
        // Randomly shuffle the array of Webtrap objects
        ShuffleArray(objectsToActivate);

        // Activate the specified number of objects
        for (int i = 0; i < numberOfObjectsToActivate; i++)
        {
            // Ensure the index is within the array length
            if (i < objectsToActivate.Length)
            {
                // Activate the game object associated with the Webtrap
                objectsToActivate[i].gameObject.SetActive(true);
            }
        }
        yield return null;
    }

    void Findwebtrap()
    {
            objectsToActivate = GameObject.FindObjectsOfType<Webtrap>(true);   
    }

    // Fisher-Yates shuffle algorithm to randomly shuffle the array
    private void ShuffleArray(Webtrap[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = Random.Range(0, n);
            n--;
            Webtrap temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}
