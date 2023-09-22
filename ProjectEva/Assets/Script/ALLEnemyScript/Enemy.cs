using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy_State
{
    public abstract class Enemy : MonoBehaviour
    {
        public float hp;
        public float speed;
        public float rotateSpeed;
        public float damage;
        public float damageSanity;
        public float detection;
        public NavMeshAgent agent;
        public Transform player;
        public Transform enemy;
        public Vector2 savedPositionEnemy;
        public Rigidbody2D rb;
        public EnemySight enemySight;
        public StateMachine currentState;
        public State_Follow_Player state_Follow_Player;
        public State_Attacl state_Attacl;
        public State_StopFollow_Player state_StopFollow_Player;
        public void EnterState(StateMachine state)
        {
            currentState = state;
            state.Behavevior(this);
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

    }
}