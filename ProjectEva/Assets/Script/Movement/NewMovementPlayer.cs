using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovementPlayer : MonoBehaviour
{
    public Vector2 direction;
    public float speed;
    public float crouchSpeed;
    public float runspeed;
    public Collider2D CircleSound;
    public ObjectPolling SoundWave;
    public SanityScaleController sanityScaleController;
    private GunSpeedManager gunSpeedManager;

    public bool isRunning = false;
    public bool isWalking = false;
    public bool isCrouching = false;
    public bool isWaittingtime = false;
    private void Start()
    {
        gunSpeedManager = FindObjectOfType<GunSpeedManager>();
        sanityScaleController = FindObjectOfType<SanityScaleController>();
        
    }

    void Update()
    {
        GetDirection();
        if (transform.hasChanged && !isCrouching && !isRunning)
        {   
            
        }
        
        // Check for running and crouching
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            isCrouching = false;
            isWalking = false;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
            isRunning = false;
            isWalking = false;
        }
        else
        {
            isWalking = true;
            isRunning = false;
            isCrouching = false;
        }

        // Adjust movement based on the current state
        if (isRunning)
        {
            Run();
            if(!isWaittingtime && direction.x + direction.y != 0)
                StartCoroutine(DelayTimeSoundWave());
            Debug.Log("Create Sound");
            transform.hasChanged = false;
        }
        else if (isWalking)
        {   
            if(!isWaittingtime && direction.x + direction.y != 0)
                StartCoroutine(DelayTimeSoundWave());
            Debug.Log("Create Sound");
            transform.hasChanged = false;
            Move();
        }
        else 
        {
            Crouch();
        }
    }
    IEnumerator DelayTimeSoundWave()
    {   
        isWaittingtime = true;
        yield return new WaitForSeconds(1);
        if(isRunning) 
            SoundWave.SpawnFromPool("Sound Wave Run", this.transform.position, Quaternion.identity);
        else if(isWalking)
            SoundWave.SpawnFromPool("Sound Wave Walk", this.transform.position, Quaternion.identity);
        isWaittingtime = false;
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
