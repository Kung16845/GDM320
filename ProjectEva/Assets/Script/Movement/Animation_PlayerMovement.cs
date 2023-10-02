using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation_PlayerMovement : MonoBehaviour
{
    Animator animator;
    Vector2 movement;
    Transform player;


    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            transform.position = new Vector3(player.position.x, player.position.y);
        }
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }
}
