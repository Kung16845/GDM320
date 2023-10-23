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
    public WalkSoundManager WalkSoundManager;
    private GameObject currentSoundObject;


    private void Start()
    {
        gunSpeedManager = FindObjectOfType<GunSpeedManager>();
        sanityScaleController = FindObjectOfType<SanityScaleController>();
        WalkSoundManager = FindObjectOfType<WalkSoundManager>();
        
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
    }
    else if (Input.GetKey(KeyCode.LeftControl))
    {
        isCrouching = true;
        isRunning = false;
        isWalking = false;
        Crouch();
    }
    else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
    {
        isWalking = true;
        isRunning = false;
        isCrouching = false;
        if (currentSoundObject == null || !currentSoundObject.GetComponent<AudioSource>().isPlaying)
        {
            currentSoundObject = WalkSoundManager.PlaySound("Walk", transform);
        }

        Move();
    }
    else
    {
        isWalking = false;
        isRunning = false;
        isCrouching = false;
        WalkSoundManager.StopSound("Walk");
        WalkSoundManager.StopSound("Run");
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
        WalkSoundManager.StopSound("Run");
        transform.Translate(direction * (speed * sanityScaleController.GetSpeedScale()) * Time.deltaTime);
    }
    void Run()
    {
    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
    {
        WalkSoundManager.StopSound("Walk");
        if (currentSoundObject == null || !currentSoundObject.GetComponent<AudioSource>().isPlaying)
        {

            currentSoundObject = WalkSoundManager.PlaySound("Run", transform);
        }
    }
    else
    {
        WalkSoundManager.StopSound("Walk"); // Stop the walk sound if the player is not pressing movement keys
    }

    transform.Translate(direction * (runspeed * sanityScaleController.GetSpeedScale()) * Time.deltaTime);
}

    void Crouch()
    {
        transform.Translate(direction * (crouchSpeed * sanityScaleController.GetSpeedScale()) * Time.deltaTime);
    }
}

