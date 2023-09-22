using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemy_State
{
    public class State_Follow_Player : StateMachine
    {
        public override void Behavevior(Enemy enemy)
        {          
            Vector2 direction = (enemy.player.position - enemy.transform.position).normalized;
            enemy.rb.velocity = direction * enemy.speed; 

            Vector2 player = new Vector2(enemy.player.position.x,enemy.player.position.y);
            float AnglePlayer = enemy.Vector2toAngle(player) - 90f;
            
            enemy.transform.up = enemy.AngletoVector2(AnglePlayer).normalized * enemy.rotateSpeed;
        }
    }
}