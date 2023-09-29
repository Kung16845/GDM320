using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy_State
{    
    [Serializable]
    public class SetPosition 
    {
        public string namePoint;
        public Transform point;

        public SetPosition(string name,Transform position)
        {
            this.namePoint = name;
            this.point = position;
        }
        
    }
}

