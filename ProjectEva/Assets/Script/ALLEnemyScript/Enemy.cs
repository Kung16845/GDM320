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
        public Vector2 targetPosition;
        public Transform enemy;
        public Rigidbody2D rb;
        public DirectorAI directorAI;
        public EnemySight enemySight;
        public StateMachine currentState;
        public State_Searching state_Searching;
        public State_Listening_Follow_Player state_Listening_Follow_Player;
        public State_Hunting state_Hunting;
        public State_Retreat state_Retreat;
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