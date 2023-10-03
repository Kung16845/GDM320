using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Enemy_State
{    
    [Serializable]
    public class SetPosition 
    {
        public string namePoint;
        public Transform point;

        public Transform FindClosestPosition(List<SetPosition> setPositions,Transform Target)
        {   
            string name = null;
            float closestDistance = float.MaxValue; 

            foreach(var place in setPositions)
            {
                float distance = Vector2.Distance(place.point.position,Target.position);
                if(distance < closestDistance)
                {
                    closestDistance = distance;
                    name = place.namePoint;
                }
            }

            var pointPosition = setPositions.FirstOrDefault(point => point.namePoint == name);

            return pointPosition.point;
        }
    }
}

