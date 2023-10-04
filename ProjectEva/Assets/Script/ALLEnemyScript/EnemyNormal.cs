using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using System.Diagnostics;

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
            this.hp = this.maxhp;
            this.enemyCollider = GetComponent<Collider2D>();
            this.spriteRenderer = GetComponent<SpriteRenderer>();
            RandomPositionSpawns(directorAI);

        }
        private void Update()
        {
            switch (currentState)
            {
                case State_Hunting :
                    EnterState(state_Hunting);    
                    if(hp <= 0) 
                        currentState = state_Retreat;          
                break;
                case State_Listening :
                    EnterState(state_Listening);
                    if(enemySight.canSee)   
                        currentState = state_Hunting;
                break;
                case State_Searching :
                    EnterState(state_Searching);
                    if(enemySight.canSee)   
                        currentState = state_Hunting;
                break;
                case State_Retreat : 
                    EnterState(state_Retreat);
                    if(hp >= 0) 
                        currentState = state_Listening;
                break;
            } 
            // if (enemySight.canSee || isAttack && hp > 0)
            // {
            //     isAttack = true;
            //     Debug.Log("Enter State_Hunting");
            //     EnterState(state_Hunting);
            // }

            // else if (!enemySight.canSee && !isAttack && currentState != state_Listening && !isRunStage && hp > 0)
            // {
            //     isRunStage = true;
            //     SetAreaMask();
            //     Debug.Log("Enter State_Listening");
            //     SetAlpha(255);
            //     EnterState(state_Listening);
            // }

            // else if (currentState != state_Searching && !isRunStage && !isAttack && hp > 0)
            // {
            //     isRunStage = true;
            //     Debug.Log("Enter State_Searching");
            //     EnterState(state_Searching);
            // }
            // else if (isUseTunnalToCloseSpawns)
            // {
            //     agent.SetDestination(directorAI.FindClosestPosition(directorAI.listSpawnPosition, directorAI.player).position);
            //     if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending)
            //     {
            //         Debug.Log("Chagne State from arrive to ClosaeSpawns");
            //         this.isRunStage = false;
            //         isUsingTunnel = false;
            //         isUseTunnalToCloseSpawns = false;
            //         enemyCollider.isTrigger = false;
            //         hp = maxhp;
            //     }
            // }
            // else if (Vector2.Distance(transform.position, ResingPoint.position) <= 0.5f && isUsingTunnel)
            // {
            //     Debug.Log("Go to ResingPoint");
            //     StartCoroutine(DelayTimeForHeal(5.0f));
            // }
           
            // else if (Vector2.Distance(transform.position, targetPosition) <= 0.5f && isRunStage)
            // {
            //     Debug.Log("Chagne State");
            //     this.isRunStage = false;
            //     if (currentState == state_Retreat && !isRunStage)
            //     {
            //         Debug.Log("Stay State_Retreat");
            //         isRunStage = true;
            //         isUsingTunnel = true;
            //         if (isUsingTunnel)
            //         {
            //             SetNavMeshArea("Tunnel");
            //             SetAlpha(0);
            //             agent.speed *= 2;
            //             enemyCollider.isTrigger = true;
            //             targetPosition = ResingPoint.position;
            //             agent.SetDestination(ResingPoint.position);
            //         }
            //     }
            // }

            // else if (hp <= 0 && currentState != state_Retreat)
            // {
            //     isAttack = false;
            //     isRunStage = true;
            //     Debug.Log("Enter State_Retreat");
            //     EnterState(state_Retreat);
            // }
        }

    }
}