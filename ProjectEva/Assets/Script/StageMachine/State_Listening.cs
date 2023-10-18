using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
namespace Enemy_State
{
    public class State_Listening : StateMachine //มอนเตอร์อยู่ นอกแมพ เตรียมเกิด
    {
        public SetPosition AllSpawns = new SetPosition();
        public bool isRunState_Listening = true;
        public bool isSetValue = false;
        public override void Behavevior(Enemy enemy)
        {
            Debug.Log("Enter Start StageState_Listening");
            if (isRunState_Listening)
            {

                if (!isSetValue)
                {
                    Debug.Log("Start StageState_Listening");
                    enemy.SetNavMeshArea("Tunnel");
                    enemy.SetAlpha(0);
                    enemy.agent.speed = enemy.speed * 2;
                    var SpawmsClosePlayer = AllSpawns.FindClosestPosition(enemy.directorAI.setSpawns, enemy.directorAI.player).position;
                    enemy.agent.SetDestination(SpawmsClosePlayer);
                    enemy.GetComponent<BoxCollider2D>().isTrigger = false;
                    isSetValue = true;
                }
                if (enemy.enemyDetectSound.soundValue >= 8)
                {
                    if (enemy.agent.remainingDistance <= enemy.agent.stoppingDistance && !enemy.agent.pathPending)
                    {
                        enemy.isUsingTunnel = false;
                        isRunState_Listening = false;
                        isSetValue = false;
                        enemy.SetAlpha(255);
                        enemy.agent.speed = enemy.speed;
                        Debug.Log("End State Listen");
                    }
                }
            }
        }

    }

}
