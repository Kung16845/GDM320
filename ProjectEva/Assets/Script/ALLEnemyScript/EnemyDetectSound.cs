using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy_State
{
    public class EnemyDetectSound : MonoBehaviour
    {
        public float radius;
        public Enemy enemy;
        public LayerMask layerMask;
        private void Update()
        {
            var Collider2DCheckSound = Physics2D.OverlapCircle(transform.position,radius,layerMask);
            if (Collider2DCheckSound != null)
            {
                // enemy.isHear = true;
                Debug.Log("Sound Is Hear");
            }
            else 
            {   
                Debug.Log("Sound Is Not Hear");
                // enemy.isHear = false;
            }
        }

        public void MakeSoundWalkingRayCast()
        {

        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }

    }
}