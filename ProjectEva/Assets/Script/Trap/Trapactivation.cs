using System.Collections;
using UnityEngine;

public class Trapactivation : MonoBehaviour
{
    public GameObject[] objectsToActivate; // Array of game objects to activate
    public int numberOfObjectsToActivate = 2; // Number of objects to activate (changeable)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the collider is the player (you can adjust the tag or use a different condition)
        {
            StartCoroutine(ActivateRandomObjects());
            Destroy(this.gameObject);
        }
    }

    private IEnumerator ActivateRandomObjects()
    {
        // Randomly shuffle the array of game objects
        ShuffleArray(objectsToActivate);

        // Activate the specified number of objects
        for (int i = 0; i < numberOfObjectsToActivate; i++)
        {
            if (i < objectsToActivate.Length)
            {
                objectsToActivate[i].SetActive(true);
            }
        }
        yield return null;
    }

    // Fisher-Yates shuffle algorithm to randomly shuffle the array
    private void ShuffleArray(GameObject[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            int k = Random.Range(0, n);
            n--;
            GameObject temp = array[n];
            array[n] = array[k];
            array[k] = temp;
        }
    }
}
