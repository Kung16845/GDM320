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

            if (enemySight.canSee || isAttack)
            {
                isAttack = true;
                Debug.Log("Enter State_Hunting");
                EnterState(state_Hunting);
            }

            if (!enemySight.canSee && !isAttack && currentState != state_Listening && !isRunStage)
            {         
                isRunStage = true;
                Debug.Log("Enter State_Listening");
                EnterState(state_Listening);
            }

            if (currentState != state_Searching && !isRunStage && !isAttack)
            {
                isRunStage = true;
                Debug.Log("Enter State_Searching");
                EnterState(state_Searching);
            }
            if (Vector2.Distance(transform.position, targetPosition) <= 0.5f)
            {
                isRunStage = false;
            }
            if (hp <= 0)
            {
                isAttack = false;
                isRunStage = false;
                Debug.Log("Enter State_Retreat");
                if(!isRunStage)
                    EnterState(state_Retreat);
            }
        }

    }
}