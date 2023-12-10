using UnityEngine;
using UnityEngine.AI;

namespace Enemy_State
{
    public class EnemySight : MonoBehaviour
    {
        public LayerMask obstacleLayer;
        public Collider2D playerCollider; // Assign the player's collider in the Inspector
        public bool canSee = false;
        public float coneVisionAngle = 45f; // Adjust the cone vision angle as needed
        public NavMeshAgent agent; // Reference to the NavMeshAgent component

        private void Update()
        {
            Vector2 enemyPosition = transform.position;
            Vector2 playerPosition = playerCollider.bounds.center;

            Vector2 direction = playerPosition - enemyPosition;

            // Perform a raycast to check for obstacles within the cone vision
            RaycastHit2D hit = Physics2D.Raycast(enemyPosition, direction, direction.magnitude, obstacleLayer);

            // Check if the angle between the enemy's forward vector and the direction to the player is within the cone vision angle
            float angleToPlayer = Vector2.Angle(agent.velocity.normalized, direction.normalized);

            // Check if the player's Collider is within the enemy's Collider and within the cone vision angle
            if (playerCollider != null && GetComponent<Collider2D>().IsTouching(playerCollider) && hit.collider == null && angleToPlayer <= coneVisionAngle / 2f)
            {
                // The player's Collider is touching the enemy's Collider, and the player is within the cone vision.
                canSee = true;
                Debug.Log("Player is in sight.");
            }
            else
            {
                canSee = false;
            }

            // Draw the cone vision
            Debug.DrawRay(enemyPosition, Quaternion.Euler(0, 0, coneVisionAngle / 2f) * agent.velocity.normalized * direction.magnitude, Color.red);
            Debug.DrawRay(enemyPosition, Quaternion.Euler(0, 0, -coneVisionAngle / 2f) * agent.velocity.normalized * direction.magnitude, Color.red);

            // Draw the RaycastHit2D line
            Debug.DrawRay(enemyPosition, direction, Color.black);
        }
    }
}
