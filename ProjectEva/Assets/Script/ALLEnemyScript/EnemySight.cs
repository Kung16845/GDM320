using UnityEngine;

namespace Enemy_State
{
    public class EnemySight : MonoBehaviour
    {
        public LayerMask obstacleLayer;
        public Collider2D playerCollider; // Assign the player's collider in the Inspector
        public bool canSee = false;

        private void Update()
        {
            Vector2 enemyPosition = transform.position;
            Vector2 playerPosition = playerCollider.bounds.center;

            Vector2 direction = playerPosition - enemyPosition;
            
            // Perform a raycast to check for obstacles
            RaycastHit2D hit = Physics2D.Raycast(enemyPosition, direction, direction.magnitude, obstacleLayer);
            Debug.DrawRay(enemyPosition, direction , Color.black);
            // Check if the player's Collider is within the enemy's Collider
            if (playerCollider != null && GetComponent<Collider2D>().IsTouching(playerCollider) && hit.collider == null)
            {
                // The player's Collider is touching the enemy's Collider, meaning the player is within sight.
                canSee = true;
                Debug.Log("Player is in sight.");
            }
            
        }
    }
}
