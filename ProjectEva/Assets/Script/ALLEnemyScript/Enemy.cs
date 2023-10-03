using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy_State
{
    public abstract class Enemy : MonoBehaviour
    {
        public float hp;
        public float maxhp;
        public float speed;
        public float rotateSpeed;
        public float damage;
        public float damageSanity;
        public float detection;
        public bool isAttack = false;
        public bool isRunStage = false;
        public bool isUsingTunnel = false;
        public bool isUseTunnalToCloseSpawns = false;
        public Collider2D enemyCollider;
        public Vector2 targetPosition;
        public SpriteRenderer spriteRenderer;
        public Transform ResingPoint;
        public NavMeshAgent agent;
        public Rigidbody2D rb;
        public DirectorAI directorAI;
        public EnemySight enemySight;
        public StateMachine currentState;
        public State_Searching state_Searching;
        public State_Listening state_Listening;
        public State_Hunting state_Hunting;
        public State_Retreat state_Retreat;
        private void Start()
        {

        }
        public void EnterState(StateMachine state)
        {
            currentState = state;
            state.Behavevior(this);
        }
        public void RandomPositionSpawns(DirectorAI directorAI)
        {
            var RandomPosition = Random.Range(1, directorAI.setSpawns.Count);
            var position = directorAI.setSpawns.ElementAt(RandomPosition);

            this.agent.Warp(position.point.position);
        }
        public void SetNavMeshArea(string areaName)
        {
            int areaIndex = NavMesh.GetAreaFromName(areaName);
            if (areaIndex == -1)
            {
                Debug.LogError("Navigation Area with name '" + areaName + "' not found.");
                return;
            }

            int areaMask = 1 << areaIndex;
            agent.areaMask = areaMask;
        }
        public void SetAlpha(byte newAlpha)
        {
            Color32 currentColor = spriteRenderer.color;
            spriteRenderer.color = new Color32(currentColor.r, currentColor.g, currentColor.b, newAlpha);
        }
        public void SetAreaMask()
        {
            int walkableAreaIndex = NavMesh.GetAreaFromName("Walkable");
            int tunnelAreaIndex = NavMesh.GetAreaFromName("Tunnel");

            if (walkableAreaIndex != -1 && tunnelAreaIndex != -1)
            {
                int walkableAreaMask = 1 << walkableAreaIndex;
                int tunnelAreaMask = 1 << tunnelAreaIndex;
                agent.areaMask = walkableAreaMask | tunnelAreaMask;  // Combine the masks using bitwise OR
            }
            else
            {
                Debug.LogError("One or both area names not found.");
            }
        }
        public IEnumerator DelayTimeForHeal(float time)
        {
            Debug.Log("Start Delay Time");
            yield return new WaitForSeconds(time);
            isUseTunnalToCloseSpawns = true;
        }
        public float Vector2toAngle(Vector2 vector)
        {
            return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
        }
        public Vector2 AngletoVector2(float Angle)
        {
            float radians = Angle * Mathf.Deg2Rad;
            return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
        }
        public void TakeDamage(float damage)
        {
            this.hp -= damage;
            StartCoroutine(StuntTime(2.0f));
        }
        private IEnumerator StuntTime(float time)
        {
            agent.speed = 1;
            yield return new WaitForSeconds(time); 
            agent.speed = speed;
        }   
        private void OnTriggerEnter2D(Collider2D player) 
        {
            // StartCoroutine(EnemyAttack(2.0f,player));
            player.GetComponent<Hp>().TakeDamage(damage);
        }
        public IEnumerator EnemyAttack(float time, Collider2D player)
        {   
            
            yield return new WaitForSeconds(time);
            
        }
    }
}