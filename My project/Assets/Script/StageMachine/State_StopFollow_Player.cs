using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

namespace Enemy_State
{
    public class State_StopFollow_Player : StateMachine
    {
        public override void Behavevior(Enemy enemy)
        {
            Vector2 direction = (enemy.savedPositionEnemy - 
            new Vector2(enemy.transform.position.x,enemy.transform.position.y)).normalized;
            enemy.rb.velocity = direction * enemy.speed;
        }
    }

}