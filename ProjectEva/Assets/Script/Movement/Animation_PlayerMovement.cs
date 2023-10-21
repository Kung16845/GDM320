using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Animation_PlayerMovement : MonoBehaviour
{
    Animator animator;
    Vector2 movement;
    Vector2 mPmovement;
    Transform player;
    bool isAiming = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform; 
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetMouseButton(1)){ isAiming = true; animator.SetBool("isAiming", true); }
        else { isAiming= false; animator.SetBool("isAiming", false); }

        if (Input.GetKey(KeyCode.LeftShift)) { animator.SetBool("isRunning", true); }
        else { animator.SetBool("isRunning", false); }

        if(Input.GetKey(KeyCode.LeftControl)) { animator.SetBool("isSlow", true); }
        else { animator.SetBool("isSlow", false); }

        if (isAiming) { aimRotate(); }
        else { normalRotate(); }
        
        animator.SetFloat("Speed", movement.sqrMagnitude);

    }
    void normalRotate()
    {
        if (movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            transform.position = new Vector3(player.position.x, player.position.y);
        }
    }

    void aimRotate()
    {
        Vector2 refLine = new Vector2(Screen.width, Screen.height / 2) - new Vector2(Screen.width / 2, Screen.height / 2);
        Vector2 mP = (Vector2)Input.mousePosition - (new Vector2(Screen.width / 2, Screen.height / 2));

        mPmovement.x = Mathf.Cos(Mathf.Deg2Rad * Vector2.Angle(refLine, mP));

        refLine = new Vector2(Screen.width / 2, Screen.height) - new Vector2(Screen.width / 2, Screen.height / 2);

        mPmovement.y = Mathf.Cos(Mathf.Deg2Rad * Vector2.Angle(refLine, mP));


        animator.SetFloat("Horizontal", mPmovement.x);
        animator.SetFloat("Vertical", mPmovement.y);
        transform.position = new Vector3(player.position.x, player.position.y);
    }
}
