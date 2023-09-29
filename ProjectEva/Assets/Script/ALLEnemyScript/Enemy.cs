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
        public Vector2 targetPosition;
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
            hp = maxhp;
        }
        public void EnterState(StateMachine state)
        {
            currentState = state;
            state.Behavevior(this);
        }
        public void RandomPositionSpawns(DirectorAI directorAI)
        {
            this.agent.enabled = !this.agent.enabled;

            var RandomPosition = Random.Range(1, directorAI.setSpawns.Count);
            var position = directorAI.setSpawns.ElementAt(RandomPosition);

            this.transform.position = position.point.position;

            this.agent.enabled = !this.agent.enabled;
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
        }
        
        public IEnumerator EnemyAttack(float time, Collider2D player)
        {
            yield return new WaitForSeconds(time);
            player.GetComponent<Hp>().TakeDamage(damage);
        }
    }
}