using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NavMeshPlus.Components;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy_State
{
    public class DirectorAI : MonoBehaviour
    {
        public Transform player;
        public Transform enemy;
        public NavMeshSurface navMeshSurface;
        public LayerMask collisionLayer;
        public Vector2 transferPosition;
        public float maxXOffset;
        public float maxYOffset;
        public float Stressvalue;
        public List<Transform> allResingPoint;
        public List<SetPosition> setSpawns = new List<SetPosition>();

        public List<Room> rooms = new List<Room>();
        private void Start()
        {
            this.player = FindObjectOfType<NewMovementPlayer>().transform;
            FindInactiveEnemyNormals();
            navMeshSurface = FindAnyObjectByType<NavMeshSurface>();
            StartCoroutine(EverySeconReduce(1.0f));
        }
        public void MovePositionEnemyChangeFloor()
        {
            var enemyNormal = GetComponent<EnemyNormal>();

            var setposition = new SetPosition();
            var position = setposition.FindClosestPosition(setSpawns, player);

            enemyNormal.currentState = enemyNormal.state_Searching;
            enemyNormal.agent.Warp(position.position);

        }
        public void CheckClosestResingPoint()
        {
            var enemynormal = GetComponent<EnemyNormal>();
            Transform newRestingPoint = allResingPoint.ElementAt<Transform>(0);
            float distanceRestingPoint = 0;
            foreach (var position in allResingPoint)
            {
                float positionDistance = Vector3.Distance(position.position, enemy.position);
                if (distanceRestingPoint < positionDistance)
                {   
                    distanceRestingPoint = positionDistance;
                    newRestingPoint = position;
                }
            }

            enemynormal.ResingPoint = newRestingPoint;
        }
        private void FindInactiveEnemyNormals()
        {
            // หาทุก Object ที่มี script EnemyNormal ในฉาก
            EnemyNormal[] enemyNormals = GameObject.FindObjectsOfType<EnemyNormal>(true);

            foreach (EnemyNormal enemyNormal in enemyNormals)
            {
                // ตรวจสอบว่า Object นี้ไม่ได้ Active
                if (!enemyNormal.gameObject.activeSelf)
                {
                    this.enemy = enemyNormal.transform;

                }
            }
        }
        private void Update()
        {
            // navMeshSurface.UpdateNavMesh(navMeshSurface.navMeshData); //ไว้อัปเดตแมพ
        }
        private IEnumerator EverySeconReduce(float time)
        {
            yield return new WaitForSeconds(time);
            Stressvalue -= 10;
            if (Stressvalue < 0)
                Stressvalue = 0;
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
