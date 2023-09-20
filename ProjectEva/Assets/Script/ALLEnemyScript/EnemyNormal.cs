using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Enemy_State
{
    public class EnemyNormal : Enemy
    {   
        
        private void Start() 
        {
            this.rb = GetComponent<Rigidbody2D>();
            this.player = FindObjectOfType<PlayerMovement>().transform;
            this.enemy = FindObjectOfType<EnemyNormal>().transform;
            this.savedPositionEnemy = new Vector2(enemy.transform.position.x,enemy.transform.position.y);
        }
        private void Update() 
        {   
            
            if(enemySight.canSee) //Vector2.Distance(player.transform.position, this.transform.position) <= 10f &&
            {              
                EnterState(state_Follow_Player);
            }           
            else
            {
                EnterState(state_StopFollow_Player);
            }
        
        }
    }
}