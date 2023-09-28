using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemy_State
{
    public class State_Listening : StateMachine //มอนเตอร์อยู่ นอกแมพ เตรียมเกิด
    {
        public override void Behavevior(Enemy enemy)
        {
            MovetonClosesttunnel(enemy.directorAI,enemy);
        }
        public void MovetonClosesttunnel(DirectorAI directorAI,Enemy enemy)
        {
            Dictionary<string, Vector2> RoomPosition = new Dictionary<string, Vector2>();
            var positionplayer = new Vector2(directorAI.player.position.x,directorAI.player.position.y);
            var nameRoom = new List<string>();
            var distanceRoom = new List<float>();

            foreach(var Point in directorAI.setSpawns)
            {   
                var positionRoomConvertVector2 = new Vector2(Point.point.position.x,Point.point.position.y);

                RoomPosition.Add(Point.namePoint,positionRoomConvertVector2);           
                nameRoom.Add(Point.namePoint);

                var range = Vector2.Distance(positionRoomConvertVector2,positionplayer);          
                distanceRoom.Add(range);

                Debug.Log(positionRoomConvertVector2);

            }
            int numListdistanceRoom = distanceRoom.IndexOf(distanceRoom.Min());
            var movePositionEnemy = RoomPosition[nameRoom.ElementAt<string>(numListdistanceRoom)];
            
            Debug.Log(movePositionEnemy);

            enemy.agent.SetDestination(movePositionEnemy);
        }
    }

}

 // Vector2 direction = (enemy.directorAI.player.position - enemy.transform.position).normalized;
            // enemy.rb.velocity = direction * enemy.speed; 

            // Vector2 player = new Vector2(enemy.directorAI.player.position.x,enemy.directorAI.player.position.y);
            // float AnglePlayer = enemy.Vector2toAngle(player) - 90f;
            
            // enemy.transform.up = enemy.AngletoVector2(AnglePlayer).normalized * enemy.rotateSpeed;