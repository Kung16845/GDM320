using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Enemy_State
{
    public class State_Follow_Player : StateMachine
    {
        public override void Behavevior(Enemy enemy)
        {
            Debug.Log("State_Follow_Player");
            enemy.speed = 2.5f;
            Vector2 direction = (enemy.player.position - enemy.transform.position).normalized;
            enemy.rb.velocity = direction * enemy.speed ;
        }
    }
}