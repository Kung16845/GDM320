using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Enemy_State
{
    public class State_Searching : StateMachine
    {
        public int currentPointIndex = 0;
        public bool isExpolre;
        public bool isSetValue;
        public float timeDelay = 2.0f;
        public List<Transform> allpointMove;
        public override void Behavevior(Enemy enemy)
        {
            
            if (!isSetValue)
            {   
                Debug.Log("Start State_Searching");
                var allRoom = new Room();
                var nameRoomClosestPlayer = allRoom.FindNameRoomClosestPlayer(enemy.directorAI.rooms, enemy.directorAI.player);
                var pointInRoomForEnemyMove = enemy.directorAI.rooms.FirstOrDefault(allpoint => allpoint.nameRoom == nameRoomClosestPlayer);
                allpointMove = allRoom.FindPointMoveInRoom(pointInRoomForEnemyMove.AllpointInRoom);
                enemy.SetAreaMask();
                GoPointInRooms(allpointMove.ElementAt<Transform>(currentPointIndex), enemy);
                isSetValue = true;
            }

            if (!enemy.agent.pathPending && enemy.agent.remainingDistance < 0.1f && isExpolre)
                ExploreAllPoint(allpointMove, enemy);

        }
        public void ExploreAllPoint(List<Transform> allpoint, Enemy enemy)
        {
            if (currentPointIndex < allpoint.Count - 1)
            {
                currentPointIndex++;
                GoPointInRooms(allpoint.ElementAt<Transform>(currentPointIndex), enemy);
            }
            else
            {
                StartCoroutine(ResetState_Searching(timeDelay));
            }
        }
        public IEnumerator ResetState_Searching(float time)
        {
            Debug.Log("Start Delay Time in State_Searching");
            isExpolre = false;
            yield return new WaitForSeconds(time);
            currentPointIndex = 0;
            isSetValue = false;
            isExpolre = true;

        }
        public void GoPointInRooms(Transform point, Enemy enemy)
        {
            enemy.agent.SetDestination(point.position);
            isExpolre = true;
        }

    }
}

