using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

namespace Enemy_State
{
    public abstract class Enemy : MonoBehaviour
    {
        public float hp;
        public float speed;
        public float damage;
        public float damageSanity;
        public float detection;
        public Transform player;
        public Transform enemy;
        public Vector2 savedPositionEnemy;
        public Rigidbody2D rb;
        public StateMachine currentState;
        public State_Follow_Player state_Follow_Player; 
        public State_Attacl state_Attacl; 
        public State_StopFollow_Player state_StopFollow_Player;
        public void EnterState(StateMachine state)
        {   
            currentState = state;
            state.Behavevior(this);
        }
        
        
    }
}