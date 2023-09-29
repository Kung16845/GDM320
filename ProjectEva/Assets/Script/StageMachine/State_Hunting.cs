using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy_State
{
    public class State_Hunting : StateMachine
    {
        public override void Behavevior(Enemy enemy)
        {

            enemy.agent.SetDestination(enemy.directorAI.player.position);

        }
    }

}

