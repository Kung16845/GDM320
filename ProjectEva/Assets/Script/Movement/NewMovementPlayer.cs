using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovementPlayer : MonoBehaviour
{
    public Vector2 direction;
    public float speed;
    public float crouchSpeed;
    public float runspeed;
    public SanityScaleController sanityScaleController;
    private GunSpeedManager gunSpeedManager;

    public bool isRunning = false;
    public bool isCrouching = false;

    private void Start()
    {
        gunSpeedManager = FindObjectOfType<GunSpeedManager>();
        sanityScaleController = FindObjectOfType<SanityScaleController>();
    }

    void Update()
    {
        GetDirection();

        // Check for running and crouching
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            isCrouching = false;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
            isRunning = false;
        }
        else
        {
            isRunning = false;
            isCrouching = false;
        }

        // Adjust movement based on the current state
        if (isRunning)
        {
            Run();
        }
        else if (isCrouching)
        {
            Crouch();
        }
        else
        {
            Move();
        }
    }

    void GetDirection()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        direction = new Vector2(horizontal, vertical);
    }

    void Move()
    {
        transform.Translate(direction * (speed * sanityScaleController.GetSpeedScale()) * Time.deltaTime);
    }

    void Run()
    {
        transform.Translate(direction * (runspeed * sanityScaleController.GetSpeedScale()) * Time.deltaTime);
    }

    void Crouch()
    {
        transform.Translate(direction * (crouchSpeed * sanityScaleController.GetSpeedScale()) * Time.deltaTime);
    }
}
