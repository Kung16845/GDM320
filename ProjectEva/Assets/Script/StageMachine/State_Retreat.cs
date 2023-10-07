using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

namespace Enemy_State
{
    public class State_Retreat : StateMachine
    {
        public bool isRunState_Retreat = true;
        public SetPosition AllSpawns = new SetPosition();
        public override void Behavevior(Enemy enemy)
        {
            if (isRunState_Retreat)
            {
                if (!enemy.isUsingTunnel)
                {
                    Debug.Log("enemy Find Closest Spawns");
                    var SpawmsCloseEnemy = AllSpawns.FindClosestPosition(enemy.directorAI.setSpawns, enemy.directorAI.enemy).position;
                    enemy.agent.SetDestination(SpawmsCloseEnemy);
                    if (enemy.agent.remainingDistance <= enemy.agent.stoppingDistance && !enemy.agent.pathPending)
                        enemy.isUsingTunnel = true;
                }
                else
                {
                    Debug.Log("enemy FindRestingPoint");
                    enemy.SetNavMeshArea("Tunnel");
                    enemy.SetAlpha(0);
                    enemy.agent.speed = enemy.speed * 2;
                    enemy.agent.SetDestination(enemy.ResingPoint.position);
                    if (enemy.agent.remainingDistance <= enemy.agent.stoppingDistance && !enemy.agent.pathPending)
                    {
                        StartCoroutine(WaitInResingPoint(5.0f,enemy));
                    }
                }
            }
            // var movetoCloseSpawn = enemy.directorAI.FindClosestPosition(
            //     enemy.directorAI.listSpawnPosition, enemy.transform);

            // enemy.targetPosition = movetoCloseSpawn.position;
            // enemy.agent.SetDestination(movetoCloseSpawn.position);

            // Debug.Log("BeforeCheck If");


            // float Distance = Vector2.Distance(enemy.transform.position, movetoCloseSpawn.position);

            // if (Distance <= 0.5f)
            // {
            //     enemy.agent.SetDestination(enemy.ResingPoint.position);
            //     // StartCoroutine(DelayedTimeBeforeTeloporttoSpawn(5f, enemy));
            // }

            // Debug.Log("AfterCheck If");
        }
        IEnumerator WaitInResingPoint(float time,Enemy enemy)
        {
            yield return new WaitForSeconds(time);
            isRunState_Retreat = false;
            enemy.hp = enemy.maxhp;
        }

        // private IEnumerator DelayedTimeBeforeTeloporttoSpawn(float time, Enemy enemy)
        // {
        //     Debug.Log("StartCounting");
        //     yield return new WaitForSeconds(time);
        //     enemy.hp = enemy.maxhp;
        //     Findclosestpositiontoplayer(enemy);
        // }
        // public void Findclosestpositiontoplayer(Enemy enemy)
        // {
        //     enemy.agent.enabled = !enemy.agent.enabled;

        //     var position = enemy.directorAI.FindClosestPosition(enemy.directorAI.listSpawnPosition,
        //                                                         enemy.directorAI.player.transform);

        //     enemy.transform.position = position.position;
        //     enemy.targetPosition = position.position;
        //     enemy.agent.enabled = !enemy.agent.enabled;
        // }

    }

}
