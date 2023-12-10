using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy_State
{
    public class EnemySensorVision : MonoBehaviour
    {
        public float radius;
        public float angle;
        public GameObject player;
        
        public LayerMask targetMask;
        public LayerMask obstrctionMask;
        public bool canSeePlayer;
        private void Start()
        {
            player = FindObjectOfType<NewMovementPlayer>().gameObject;
        }
    }
}
