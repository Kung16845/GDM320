using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy_State
{
    public class State_StopFollow_Player : StateMachine
    {
        public override void Behavevior(Enemy enemy)
        {
            enemy.speed = 0;
            Vector2 direction = (enemy.player.position - enemy.transform.position).normalized;
            enemy.rb.velocity = direction * enemy.speed ;
        }
    }

}