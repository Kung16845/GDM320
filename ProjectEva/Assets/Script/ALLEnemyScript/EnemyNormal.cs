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
            this.directorAI = FindObjectOfType<DirectorAI>();
            this.agent = GetComponent<NavMeshAgent>();
            this.agent.updateRotation = false;
            this.agent.updateUpAxis = false;
            this.agent.speed = speed;
            this.hp = this.maxhp;
            this.spriteRenderer = GetComponent<SpriteRenderer>();
            RandomPositionSpawns(directorAI);
            state_Listening.isRunState_Listening = true;
            currentState = state_Listening;
        }
        private void Update()
        {
            switch (currentState)
            {
                case State_Hunting:
                    EnterState(state_Hunting);
                    if (hp <= 0)
                    {
                        currentState = state_Retreat;
                        state_Retreat.isRunState_Retreat = true;
                    }
                    break;
                case State_Listening:
                    EnterState(state_Listening);
                    if (!state_Listening.isRunState_Listening)
                    {
                        isUsingTunnel = false;
                        SetAreaMask();
                        currentState = state_Searching;
                        EnterState(state_Searching);
                    }
                    break;
                case State_Searching:
                    EnterState(state_Searching);
                    if (enemySight.canSee && hp > 0)
                        currentState = state_Hunting;
                    else if (isHear && hp > 0)
                        currentState = state_SearchingSound;
                    else if (directorAI.Stressvalue == 100)
                    {
                        state_Listening.isRunState_Listening = true;
                        currentState = state_Listening;
                    }
                    else if (hp <= 0)
                    {
                        state_Retreat.isRunState_Retreat = true;
                        currentState = state_Retreat;
                    }
                    break;
                case State_SearchingSound:
                    EnterState(state_SearchingSound);
                    if (agent.remainingDistance <= agent.stoppingDistance && !agent.pathPending && hp > 0)
                    {
                        state_Searching.isSetValue = false;
                        currentState = state_Searching;
                    }
                    else if (enemySight.canSee && hp > 0)
                        currentState = state_Hunting;
                    else if (hp <= 0)
                    {
                        state_Retreat.isRunState_Retreat = true;
                        currentState = state_Retreat;
                    }
                    break;
                case State_Retreat:
                    EnterState(state_Retreat);
                    if (!state_Retreat.isRunState_Retreat)
                    {
                        state_Listening.isRunState_Listening = true;
                        currentState = state_Listening;
                    }
                    break;
            }

        }
        
    }
}