using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy_State
{
    public class DirectorAI : MonoBehaviour
    {
        public Transform player;
        public Transform enemy;
        public LayerMask collisionLayer;
        public Vector2 transferPosition;
        public float maxXOffset;
        public float maxYOffset;
        public List<SetPosition> setSpawns = new List<SetPosition>();
        public List<SetPosition> setRoom = new List<SetPosition>();
        private void Start()
        {
            this.player = FindObjectOfType<PlayerMovement>().transform;
            this.enemy = FindObjectOfType<EnemyNormal>().transform;
        }
        private void Update()
        {
           
        }

        public void TransferPositionToEnemy()
        {
            do
            {
                transferPosition = new Vector2(player.position.x + RandomDistance(maxXOffset),
                player.position.y + RandomDistance(maxYOffset));
            } while (IsColliding(transferPosition));
        }
        public float RandomDistance(float distance)
        {
            return Random.Range(-distance, distance);
        }
        public bool IsColliding(Vector2 position)
        {
            // ตรวจสอบการชนโดยใช้ Physics2D.OverlapPoint หรือ Physics.Raycast
            // คืนค่า true ถ้ามีการชน, false ถ้าไม่มีการชน

            Collider2D colliderHit = Physics2D.OverlapPoint(position, collisionLayer); // หรือใช้ Physics.Raycast ในกรณี 3D

            return colliderHit != null;
        }
    }
}
