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

            if (isRunState_Listening)
            {
                // if (enemy.isUsingTunnel)
                // {
                if (!isSetValue)
                {
                    Debug.Log("Start StageState_Listening");
                    enemy.SetNavMeshArea("Tunnel");
                    enemy.SetAlpha(0);
                    enemy.agent.speed = enemy.speed * 2;
                    var SpawmsClosePlayer = AllSpawns.FindClosestPosition(enemy.directorAI.setSpawns, enemy.directorAI.player).position;
                    enemy.agent.SetDestination(SpawmsClosePlayer);
                    isSetValue = true;
                }
                else if (enemy.agent.remainingDistance <= enemy.agent.stoppingDistance && !enemy.agent.pathPending)
                {
                    enemy.isUsingTunnel = false;
                    isRunState_Listening = false;
                    isSetValue = false;
                    enemy.SetAlpha(255);
                    enemy.agent.speed = enemy.speed;
                    Debug.Log("End State Listen");
                }
                // }
                // else
                // {
                //     Debug.Log("enemy Find Closest Spawns");
                //     var SpawmsCloseEnemy = AllSpawns.FindClosestPosition(enemy.directorAI.setSpawns, enemy.directorAI.enemy).position;
                //     enemy.agent.SetDestination(SpawmsCloseEnemy);
                //     if (enemy.agent.remainingDistance <= enemy.agent.stoppingDistance && !enemy.agent.pathPending)
                //         enemy.isUsingTunnel = true;
                // }
            }
        }
        // public void MovetonClosesttunnel(DirectorAI directorAI, Enemy enemy)
        // {

        //     var movetoSpawn = directorAI.FindClosestPosition
        //     (directorAI.listSpawnPosition, directorAI.player);

        //     Debug.Log(movetoSpawn.position);
        //     enemy.targetPosition = movetoSpawn.position;
        //     Debug.Log(movetoSpawn);

        //     enemy.agent.SetDestination(movetoSpawn.position);
        // }
    }

}
