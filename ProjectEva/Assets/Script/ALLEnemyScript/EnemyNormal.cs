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
            this.directorAI = FindObjectOfType<DirectorAI>();
            this.agent = GetComponent<NavMeshAgent>();
            this.agent.updateRotation = false;
            this.agent.updateUpAxis = false;
            this.agent.speed = speed;
            RandomPositionSpawns(directorAI);
            
        }
        private void Update()
        {
            
            if (enemySight.canSee) //Vector2.Distance(player.transform.position, this.transform.position) <= 10f &&
            {

            }
            else if (!enemySight.canSee)
            {
                EnterState(state_Listening);

                // if (Vector2.Distance(transform.position, targetPosition) <= 0.5f)
                // {   
                //     directorAI.TransferPositionToEnemy();
                //     this.targetPosition = directorAI.transferPosition;
                // }
            }
            if (hp <= 0)
            {
                EnterState(state_Retreat);
            }

        }
    }
}