using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy_State
{   
    public abstract class StateMachine : MonoBehaviour
    {
        public abstract void Behavevior(Enemy enemy);
    }   
}

