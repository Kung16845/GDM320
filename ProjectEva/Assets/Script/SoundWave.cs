using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundWave : MonoBehaviour
{
    [Header("-----------Time----------")]
    [Space(25f)]
    public float delayTimeIncreateRadius = 0.5f;
    public float durationSound = 4;

    [Header("-----------RadiusCollider----------")]
    [Space(25f)]
    public float radiusWalk = 16;
    public float radiusRun;
    public float radiusGun;
    public float radiusshotGun;
    [Header("---------CheckValue------------")]
    [Space(25f)]
    public bool isCreatingSoundWave;
    public bool isReducing;
    public bool isDetect;
    
    [Header("---------ScriptValue------------")]
    [Space(25f)]
    public NewMovementPlayer checkMovementPlayer;
    public Pistol checkShoot;
    public Shotgun shotGun;
    
    // Start is called before the first frame update
    private void Update()
    {   
        checkMovementPlayer = FindObjectOfType<NewMovementPlayer>();
        checkShoot = FindObjectOfType<Pistol>();
        shotGun = FindObjectOfType<Shotgun>();
        if (this.gameObject.activeInHierarchy && !isReducing && !isCreatingSoundWave)
        {   
            CheckCreateSoundWave();
        }
        
    }
    void CheckCreateSoundWave()
    {
        if (checkMovementPlayer.isRunning && !isCreatingSoundWave)
            StartCoroutine(CreateCircleCollder2DSound(radiusRun));

        else if (checkMovementPlayer.isWalking && !isCreatingSoundWave)
            StartCoroutine(CreateCircleCollder2DSound(radiusWalk));
            
        else if (checkShoot.isshoot && !isCreatingSoundWave)
            StartCoroutine(CreateCircleCollder2DSound(radiusGun));
        else if (shotGun.isshoot && !isCreatingSoundWave)
            StartCoroutine(CreateCircleCollder2DSound(radiusshotGun));
    }
    IEnumerator CreateCircleCollder2DSound(float radius)
    {   
        Debug.Log(radius);
        yield return new WaitForSeconds(delayTimeIncreateRadius);
        isCreatingSoundWave = true;
        this.GetComponent<CircleCollider2D>().radius = radius;
        if(!isReducing)
            StartCoroutine(ReduceSoundWave(this.GetComponent<CircleCollider2D>().radius / 4));
    }
    IEnumerator ReduceSoundWave(float reduceradius)
    {
        isReducing = true;
        yield return new WaitForSeconds(1.0f);
        if (this.GetComponent<CircleCollider2D>().radius > 0.5)
        {
            this.GetComponent<CircleCollider2D>().radius -= reduceradius;      
            StartCoroutine(ReduceSoundWave(reduceradius));
        }
        else if(this.GetComponent<CircleCollider2D>().radius < 0.5)
        {   
            this.gameObject.SetActive(false);
            isReducing = false;
            isDetect =false;
             Debug.Log("Object is not active");
            // Destroy(this.gameObject);
            
        }

    }
}
