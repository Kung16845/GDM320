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
            this.hp = this.maxhp;
            this.enemyCollider = GetComponent<Collider2D>();
            RandomPositionSpawns(directorAI);

        }
        private void Update()
        {

            if (enemySight.canSee || isAttack && hp > 0)
            {
                isAttack = true;
                Debug.Log("Enter State_Hunting");
                EnterState(state_Hunting);
            }

            else if (!enemySight.canSee && !isAttack && currentState != state_Listening && !isRunStage && hp > 0)
            {
                isRunStage = true;
                Debug.Log("Enter State_Listening");
                EnterState(state_Listening);
            }

            else if (currentState != state_Searching && !isRunStage && !isAttack && hp > 0)
            {
                isRunStage = true;
                Debug.Log("Enter State_Searching");
                EnterState(state_Searching);
            }
            else if (Vector2.Distance(transform.position, ResingPoint.position) <= 0.5f && isUsingTunnel)
            {
                Debug.Log("Chagne State from arrive to ResingPoint");
                this.isRunStage = false;
                isUsingTunnel = false;
                enemyCollider.isTrigger = false;
                SetAreaMask();
                StartCoroutine(DelayTimeForHeal(5.0f));
            }
            else if (Vector2.Distance(transform.position, targetPosition) <= 0.5f && isRunStage)
            {
                Debug.Log("Chagne State");
                this.isRunStage = false;
                if (currentState == state_Retreat && !isRunStage)
                {
                    Debug.Log("Stay State_Retreat");
                    isRunStage = true;
                    isUsingTunnel = true;
                    if (isUsingTunnel)
                    {
                        SetNavMeshArea("Tunnel");
                        agent.speed *= 2;
                        enemyCollider.isTrigger = true;
                        targetPosition = ResingPoint.position;
                        agent.SetDestination(ResingPoint.position);
                    }
                }
            }

            else if (hp <= 0 && currentState != state_Retreat)
            {
                isAttack = false;
                isRunStage = true;
                Debug.Log("Enter State_Retreat");
                EnterState(state_Retreat);
            }
        }

    }
}