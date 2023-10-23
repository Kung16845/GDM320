using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Velo_movement : MonoBehaviour
{
    Animator animator;
    Transform mon;
    Vector2 movement;
    Vector2 lastPosition;
    public NavMeshAgent agent;
    public Vector3 velocity;
    void Start()
    {
        animator = GetComponent<Animator>();
        mon = GameObject.FindGameObjectWithTag("Velocrux").transform;
        lastPosition =new Vector2(0,0);
    }

    // Update is called once per frame
    void Update()
    {
        velocity = agent.velocity;

        Vector2 currentPosition = mon.position;
        
        if (currentPosition != lastPosition) {
            
            animator.SetFloat("Speed", 1); 
        }
        else {

            animator.SetFloat("Speed", 0); 
        }

        movement.x = agent.velocity.x;
        movement.y = agent.velocity.y;


        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        lastPosition = mon.position;

        transform.position = new Vector3(mon.position.x, mon.position.y);
    }
}
