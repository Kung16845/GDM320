using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy_State
{
    public class State_Hunting : StateMachine
    {
        public bool setValue;
        public SoundManager soundManager;
        private void Start()
        {
            soundManager = FindObjectOfType<SoundManager>();
        }  
        public override void Behavevior(Enemy enemy)
        { 
            if(!setValue)
            {
                setValue = true;
                soundManager.PlaySound("Alert");
            } 
            enemy.agent.SetDestination(enemy.directorAI.player.position);
        }
    }

}

