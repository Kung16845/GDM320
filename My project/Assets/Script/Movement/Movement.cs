using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
        public Vector2 direction;
        public float speed;
        public float rotateSpeed;
        private float horizontal;
        private float vertical;
        void Update()
        {
            GetDirection();
            Move();       
        }
        void GetDirection()
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            
            direction = new Vector2(horizontal,vertical);
        }

        void Move()
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
       
}
