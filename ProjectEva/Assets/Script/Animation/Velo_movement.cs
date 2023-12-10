using Enemy_State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Velo_movement : MonoBehaviour
{
    Animator animator;
    public EnemyNormal VeloWithScript;
    Transform mon;
    Vector2 movement;
    Vector2 lastPosition;
    public NavMeshAgent agent;
    public Vector3 velocity;

    public bool isRoaming = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        mon = GameObject.FindGameObjectWithTag("Velocrux").transform;
        lastPosition =new Vector2(0,0);
    }


    // Update is called once per frame
    void Update()
    {
        if(!VeloWithScript.state_Listening.isRunState_Listening && !isRoaming)
        {
            isRoaming = true; animator.SetBool("isGoingDown", false);
            animator.SetBool("isGoingUp",true);

            VeloWithScript.agent.speed = 0;

            StartCoroutine(oneSec());  
        }
        else if (VeloWithScript.state_Listening.isRunState_Listening && isRoaming)
        {
            isRoaming = false; animator.SetBool("isGoingUp", false);
            animator.SetLayerWeight(animator.GetLayerIndex("Clawling"), 1);
            animator.SetBool("isGoingDown", true);
        }


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

    IEnumerator oneSec()
    {
        yield return new WaitForSeconds(4);
        animator.SetLayerWeight(animator.GetLayerIndex("Clawling"), 0);
        VeloWithScript.agent.speed = VeloWithScript.speed;
    }
}
