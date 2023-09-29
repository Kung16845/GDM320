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

            enemy.agent.SetDestination(movetoCloseSpawn.position);

            Debug.Log("BeforeCheck If");

            
            float Distance = Vector2.Distance(enemy.transform.position, movetoCloseSpawn.position);

            if (Distance <= 0.5f)
            {
                enemy.agent.enabled = !enemy.agent.enabled;
                enemy.transform.position = new Vector3(-10f, -18f, 0);
                enemy.agent.enabled = !enemy.agent.enabled;
                StartCoroutine(DelayedTimeBeforeTeloporttoSpawn(5f, enemy));
            }

            Debug.Log("AfterCheck If");

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
            Debug.Log("StartFindclosestpositiontoplayer");
            enemy.agent.enabled = !enemy.agent.enabled;

            var position = enemy.directorAI.FindClosestPosition(enemy.directorAI.listSpawnPosition,
                                                                enemy.transform);

            enemy.transform.position = position.position;
            enemy.agent.enabled = !enemy.agent.enabled;
        }

    }

}
// Vector2 targetposition = new Vector2(enemy.transform.position.x, enemy.transform.position.y);
// if (Vector2.Distance(enemy.transform.position, enemy.savedPositionEnemy) > 0.1f)
// {
//     Vector2 direction = (enemy.savedPositionEnemy - targetposition).normalized;
//     enemy.rb.velocity = direction * enemy.speed;
// }
// else
// {   
//     enemy.rb.velocity = Vector2.zero;
// }