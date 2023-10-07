using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemy_State
{
    public class State_SearchingSound : StateMachine
    {
        public override void Behavevior(Enemy enemy)
        {
            if (enemy.isHear)
            {
                var localsound = enemy.directorAI.player.position;
                enemy.agent.SetDestination(localsound);
                Debug.Log("Find position plater Lasty");
                enemy.isHear = false;
            }
        }
    }
}