using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemy_State
{
    [Serializable]
    public class SetPosition
    {
        public string namePoint;
        public Transform point;

        public Transform FindClosestPosition(List<SetPosition> setPositions, Transform Target)
        {
            string name = null;
            float closestDistance = float.MaxValue;

            foreach (var place in setPositions)
            {
                float distance = Vector2.Distance(place.point.position, Target.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    name = place.namePoint;
                }
            }

            var pointPosition = setPositions.FirstOrDefault(point => point.namePoint == name);

            return pointPosition.point;
        }
    }
    [Serializable]
    public class Room
    {
        public string nameRoom;
        public Transform pointCenterRoom;
        public List<Transform> AllpointInRoom;

        public string FindNameRoomClosestPlayer(List<Room> rooms, Transform player)
        {
            string name = null;
            float closestDistance = float.MaxValue;

            foreach (var place in rooms)
            {
                float distance = Vector2.Distance(place.pointCenterRoom.position, player.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    name = place.nameRoom;
                }
            }
            return name;
        }
        public List<Transform> FindPointMoveInRoom(List<Transform> allpointInRoom)
        {
            var pointinRoomforEnemyMove = new List<Transform>();
            var NumMove = UnityEngine.Random.Range(2, allpointInRoom.Count);
            
            Debug.Log(NumMove);
            for (int i = 0; i < NumMove; i++)
            {
                var point = allpointInRoom.ElementAt<Transform>(UnityEngine.Random.Range(0, allpointInRoom.Count));
                // if(point != pointinRoomforEnemyMove.ElementAt<Transform>(i-1))
                if(i > 0)
                    pointinRoomforEnemyMove.Add(CheckedTranform(point,allpointInRoom,i));
                else 
                    pointinRoomforEnemyMove.Add(point);
            }
            foreach (var point in pointinRoomforEnemyMove)
            {
                Debug.Log(point);
            }
            Debug.Log(pointinRoomforEnemyMove);
            return pointinRoomforEnemyMove;
        }
        public Transform CheckedTranform(Transform transform, List<Transform> listpoint, int Count)
        {

            if (transform == listpoint.ElementAt<Transform>(Count - 1))
            {
                var listposition = listpoint;
                listposition.Remove(transform);
                var position = listpoint.ElementAt<Transform>(UnityEngine.Random.Range(0, listposition.Count));
                return position;
            }
            else
                return transform;
        }
        
    }



}

