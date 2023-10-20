using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy_State
{
    public abstract class Enemy : MonoBehaviour
    {   
        [Header ("---------EnemyValue-----------")]
        [Space(25f)]
        public float hp;
        public float maxhp;
        public float speed;
        public float rotateSpeed;
        public float damage;
        public float damageSanity;
        public float detection;
        [Header ("---------CheckValue-----------")]
        [Space(25f)]
        public bool isHear;
        public bool isAttack = false;
        public bool isWaittingtime = false;
        public bool isRunStage = false;
        public bool isUsingTunnel = false;
        public bool isUseTunnalToCloseSpawns = false;
        
        
        [Header ("---------ScriptValue-----------")]
        [Space(25f)]
        public Transform ResingPoint;
        public SpriteRenderer spriteRenderer;
        public NavMeshAgent agent;
        public DirectorAI directorAI;
        public EnemySight enemySight;
        public EnemyDetectSound enemyDetectSound;
        public StateMachine currentState;
        public State_Searching state_Searching;
        public State_SearchingSound state_SearchingSound;
        public State_Listening state_Listening;
        public State_Hunting state_Hunting;
        public State_Retreat state_Retreat;
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
         public IEnumerator DelayTime(float time)
        {
            Debug.Log("Start Delay Time");
            isWaittingtime = true;
            yield return new WaitForSeconds(time);
            isWaittingtime = false;
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
            StartCoroutine(StuntTime(3.5f));
        }
        private IEnumerator StuntTime(float time)
        {
            agent.speed = 1;
            yield return new WaitForSeconds(time); 
            agent.speed = speed;
        }   
        
        public IEnumerator EnemyAttack(float time, Collider2D player)
        {              
            yield return new WaitForSeconds(time);
        }
        
    }
}