using UnityEngine;
using UnityEngine.AI;

namespace Enemy_State
{
    public class EnemySight : MonoBehaviour
    {
        public LayerMask obstacleLayer;
        public Collider2D playerCollider; // Assign the player's collider in the Inspector
        public Collider2D lightCollider; // Assign the light collider in the Inspector
        public Collider2D pocketCollider; // Assign the light collider in the Inspector
        public Collider2D LampCollider; // Assign the light collider in the Inspector
        public bool canSee = false;
        public float coneVisionAngle = 45f; // Adjust the cone vision angle as needed
        public NavMeshAgent agent; // Reference to the NavMeshAgent component

        // Detection parameters
        public float detectionThreshold = 5f; // The threshold at which the enemy can see the player
        public float detectionIncreaseRate = 2f; // Detection increase rate per second
        public float currentDetection = 0f;

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
                // Accumulate detection value
                currentDetection += detectionIncreaseRate * Time.deltaTime;

                // Check if the detection value exceeds the threshold
                if (currentDetection >= detectionThreshold)
                {
                    canSee = true;
                    Debug.Log("Player is in sight.");
                }
            }
            else
            {
                canSee = false;
            }

            // Check if the light collider is hit
            if (lightCollider != null && GetComponent<Collider2D>().IsTouching(lightCollider))
            {
                // Increase detection value only if the light collider is hit
                currentDetection += (detectionIncreaseRate/5)  * Time.deltaTime;
            }
            if (pocketCollider != null && GetComponent<Collider2D>().IsTouching(pocketCollider))
            {
                // Increase detection value only if the light collider is hit
                currentDetection += (detectionIncreaseRate/10) * Time.deltaTime;
            }
            if (LampCollider != null && GetComponent<Collider2D>().IsTouching(LampCollider))
            {
                // Increase detection value only if the light collider is hit
                currentDetection += (detectionIncreaseRate/20) * Time.deltaTime;
            }

            // Draw the cone vision
            Debug.DrawRay(enemyPosition, Quaternion.Euler(0, 0, coneVisionAngle / 2f) * agent.velocity.normalized * direction.magnitude, Color.red);
            Debug.DrawRay(enemyPosition, Quaternion.Euler(0, 0, -coneVisionAngle / 2f) * agent.velocity.normalized * direction.magnitude, Color.red);

            // Draw the RaycastHit2D line
            Debug.DrawRay(enemyPosition, direction, Color.black);
        }
    }
}
