using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

namespace Enemy_State
{
    public class State_Retreat : StateMachine
    {   
        public override void Behavevior(Enemy enemy)
        {
            var movetoCloseSpawn = enemy.directorAI.FindClosestPosition(
                enemy.directorAI.listSpawnPosition, enemy.transform);

            enemy.targetPosition = movetoCloseSpawn.position;
            enemy.agent.SetDestination(movetoCloseSpawn.position);

            // Debug.Log("BeforeCheck If");

            
            // float Distance = Vector2.Distance(enemy.transform.position, movetoCloseSpawn.position);

            // if (Distance <= 0.5f)
            // {
            //     enemy.agent.SetDestination(enemy.ResingPoint.position);
            //     // StartCoroutine(DelayedTimeBeforeTeloporttoSpawn(5f, enemy));
            // }

            // Debug.Log("AfterCheck If");

        }
        
        
        private IEnumerator DelayedTimeBeforeTeloporttoSpawn(float time, Enemy enemy)
        {
            Debug.Log("StartCounting");
            yield return new WaitForSeconds(time);
            enemy.hp = enemy.maxhp;
            Findclosestpositiontoplayer(enemy);
        }
        public void Findclosestpositiontoplayer(Enemy enemy)
        {
            enemy.agent.enabled = !enemy.agent.enabled;

            var position = enemy.directorAI.FindClosestPosition(enemy.directorAI.listSpawnPosition,
                                                                enemy.directorAI.player.transform);

            enemy.transform.position = position.position;
            enemy.targetPosition = position.position;
            enemy.agent.enabled = !enemy.agent.enabled;
        }

    }

}
