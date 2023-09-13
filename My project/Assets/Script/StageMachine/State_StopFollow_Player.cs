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
            Vector2 targetposition = new Vector2(enemy.transform.position.x, enemy.transform.position.y);
            if (Vector2.Distance(enemy.transform.position, enemy.savedPositionEnemy) > 0.1f)
            {
                Vector2 direction = (enemy.savedPositionEnemy - targetposition).normalized;
                enemy.rb.velocity = direction * enemy.speed;
            }
            else
            {   
                enemy.rb.velocity = Vector2.zero;
            }
        }
    }

}