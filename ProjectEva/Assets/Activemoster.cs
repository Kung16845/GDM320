using System.Collections;
using UnityEngine;
namespace Enemy_State
{
public class Activemonter : MonoBehaviour
{
    public DirectorAI directorAI;
    public GameObject MonsterToActivate; // Array of game objects to activate

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Check if the collider is the player (you can adjust the tag or use a different condition)
        {
            MonsterToActivate.SetActive(true);
            directorAI.MovePositionEnemyChangeFloor();
        }
    }
}
}
