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

            var movetoSpawn = directorAI.FindClosestPosition
            (directorAI.listSpawnPosition,directorAI.player);

            Debug.Log(movetoSpawn.position);
            enemy.targetPosition = movetoSpawn.position;
            Debug.Log(movetoSpawn);
            
            enemy.agent.SetDestination(movetoSpawn.position);
        }
    }

}
