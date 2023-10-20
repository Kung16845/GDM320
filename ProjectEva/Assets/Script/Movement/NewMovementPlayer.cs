using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMovementPlayer : MonoBehaviour
{
    
    [Header ("--------Movenment----------")]
    [Space(25f)]
    public float speed;
    public float crouchSpeed;
    public float runspeed;
    public Vector2 direction;
    
    [Header ("--------CheckValue----------")]
    [Space(25f)]
    public bool isRunning;
    public bool isWalking;
    public bool isCrouching;
    public bool isWaittingtime;
    public bool isStaySafeRoom;

    [Header ("--------SoundValue----------")]
    [Space(25f)]
    public GameObject SoundWavePrefeb;
    public ObjectPolling SoundWave;

    [Header("---------ScriptValue------------")]
    [Space(25f)]
    public SanityScaleController sanityScaleController;
    private GunSpeedManager gunSpeedManager;

    [Header("---------Audiomanager------------")]
    private SoundManager soundManager;

    private void Start()
    {
        gunSpeedManager = FindObjectOfType<GunSpeedManager>();
        sanityScaleController = FindObjectOfType<SanityScaleController>();
        soundManager = FindObjectOfType<SoundManager>();
        
    }

     void Update()
    {
        GetDirection();

        // Check for running and crouching
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
            isCrouching = false;
            isWalking = false;
            Run();
            // soundManager.PlaySound("Run"); // Play the run sound
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            isCrouching = true;
            isRunning = false;
            isWalking = false;
            Crouch();
            // soundManager.PlaySound("CrouchSound"); // Play the crouch sound
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            isWalking = true;
            isRunning = false;
            isCrouching = false;
            Move();
            // soundManager.PlaySound("Walk"); // Play the walk sound
        }
        else
        {
            isWalking = false;
            isRunning = false;
            isCrouching = false;
            // soundManager.StopSound();
        }
        if (!isWaittingtime && direction.x + direction.y != 0 && !isStaySafeRoom)
            StartCoroutine(DelayTimeSoundWave());
    }
    IEnumerator DelayTimeSoundWave()
    {   
        isWaittingtime = true;
        
        yield return new WaitForSeconds(1);
        SoundWave.SpawnFromPool("Sound Wave", this.transform.position, Quaternion.identity);

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
