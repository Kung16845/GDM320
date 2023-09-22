using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;

namespace Enemy_State
{
    public class EnemyNormal : Enemy
    {   
        
        private void Start() 
        {
            this.rb = GetComponent<Rigidbody2D>();
            this.player = FindObjectOfType<PlayerMovement>().transform;
            this.enemy = FindObjectOfType<EnemyNormal>().transform;
            this.savedPositionEnemy = new Vector2(enemy.transform.position.x,enemy.transform.position.y);
            this.agent = GetComponent<NavMeshAgent>();     
            this.agent.updateRotation = false; 
            this.agent.updateUpAxis = false; 
        }
        private void Update() 
        {   
            agent.SetDestination(player.position);
            if(enemySight.canSee) //Vector2.Distance(player.transform.position, this.transform.position) <= 10f &&
            {              
                EnterState(state_Follow_Player);
            }           
            // else
            // {
            //     EnterState(state_StopFollow_Player);
            // }
        
        }
    }
}