using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy_State
{
    public class State_Searching : StateMachine
    {
        public override void Behavevior(Enemy enemy)
        {
            FindClosestPosition(enemy.directorAI, enemy);
            StartCoroutine(DelayTime(2.0f));
        }
        public IEnumerator DelayTime(float time)
        {
            Debug.Log("Start Delay Time");
            yield return new WaitForSeconds(time);
        }
        public void FindClosestPosition(DirectorAI directorAI, Enemy enemy)
        {
            var movetoRoom = directorAI.FindClosestPosition
            (directorAI.listRoomPosition, directorAI.player);

            enemy.targetPosition = movetoRoom.position;
            Debug.Log(enemy.targetPosition);
            Debug.Log(movetoRoom);
            enemy.agent.SetDestination(movetoRoom.position);
        }
    }
}

