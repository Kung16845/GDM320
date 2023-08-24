using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
        public Vector2 direction;
        public float speed;
        void Update()
        {
            GetDirection();
            Move();
        }

        void GetDirection()
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");
            
            direction = new Vector2(horizontal,vertical);
        }

        void Move()
        {
            transform.Translate(direction * speed * Time.deltaTime);
        }
}
