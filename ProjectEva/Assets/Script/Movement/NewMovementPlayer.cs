using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovementPlayer : MonoBehaviour
{
    public Vector2 direction;
    public float speed;
    public float crouchSpeed;
    public float runspeed;
    public float radiusSound;
    public Collider2D CircleSound;
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
        if (transform.hasChanged)
        {
            Debug.Log("The transform has changed!");
            MakeTrigger2DSound();
            transform.hasChanged = false;
        }
        else
            StartCoroutine(ReduceRadiusSound());
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
    public void MakeTrigger2DSound()
    {
        Debug.Log("Make Sound");
        CircleSound.GetComponent<CircleCollider2D>().radius = radiusSound;
        StartCoroutine(ReduceRadiusSound());
    }
    IEnumerator ReduceRadiusSound()
    {
        CircleSound.GetComponent<CircleCollider2D>().radius -= 1;
        yield return new WaitForSeconds(1f);
        Debug.Log("ReduceRadiusSound is Complete");
        if(CircleSound.GetComponent<CircleCollider2D>().radius > 0.1)
            StartCoroutine(ReduceRadiusSound());
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
