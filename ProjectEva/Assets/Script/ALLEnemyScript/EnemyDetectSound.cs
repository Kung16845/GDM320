using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Enemy_State
{
    public class EnemyDetectSound : MonoBehaviour
    {
        [Header("-----------Sound Detection---------")]
        [Space(25f)]
        public float radius;
        public float Saveradius;
        public int currentsoundValue;
        public int maxSoundValue;
        public bool isCountingValueSound;
        public bool isRunningReduceSoundValue;
        public NewMovementPlayer newMovementPlayer;
        public Pistol pistol;
        public Shotgun shotgun;
        public Enemy enemy;
        public LayerMask layerMask;

        [Header("-----------Time-----------")]
        [Space(25f)]
        public float time;
        public float timeDelay;
        private void Start()
        {
            Saveradius = radius;
        }
        private void Update()
        {
            var Collider2DCheckSound = Physics2D.OverlapCircle(transform.position, radius, layerMask);
            if (Collider2DCheckSound != null)
            {
                isRunningReduceSoundValue = false;
                // enemy.isHear = true;
                Debug.Log("Sound Is Hear");
                if (!Collider2DCheckSound.GetComponent<SoundWave>().isDetect) //!isCountingValueSound &&
                {   
                    Collider2DCheckSound.GetComponent<SoundWave>().isDetect = true;
                    StartCoroutine(DelayTimeCountSoundValue());
                }
                if (currentsoundValue >= maxSoundValue)
                {
                    if (enemy.currentState != enemy.state_SearchingSound)
                        enemy.isHear = true;
                }
                time = 0;
            }
            else
            {
                Debug.Log("Sound Is Not Hear");
                time = time + 1f * Time.deltaTime;

                if (time >= timeDelay && !enemy.isHear && currentsoundValue > 0)
                {
                    time = 0;
                    StartCoroutine(ReduceSoundValue(enemy));
                    isRunningReduceSoundValue = true;
                }
            }
            if (currentsoundValue > maxSoundValue)
                currentsoundValue = maxSoundValue;

            if (enemy.currentState == enemy.state_Listening)
                radius = Saveradius * 2;
            else
                radius = Saveradius;
        }
        IEnumerator DelayTimeCountSoundValue()
        {
            isCountingValueSound = true;
            if (newMovementPlayer.isRunning)
            {
                if (enemy.currentState = enemy.state_Listening)
                    currentsoundValue += 16;
                else
                    currentsoundValue += 8;
            }
            else if (pistol.isshoot)
                currentsoundValue += 16;
            else if (shotgun.isshoot)
                currentsoundValue += 32;

            else if (newMovementPlayer.isWalking)
            {
                if (enemy.currentState = enemy.state_Listening)
                    currentsoundValue += 4 * 2;
                else
                    currentsoundValue += 4;
            }
            else
                currentsoundValue += 0;
            yield return new WaitForSeconds(4.0f);
            isCountingValueSound = false;
        }
        IEnumerator ReduceSoundValue(Enemy enemy)
        {
            currentsoundValue -= 1;
            Debug.Log("ReduceSoundValue");
            time = 0;
            yield return new WaitForSeconds(1.0f);
            if (currentsoundValue > 0 && isRunningReduceSoundValue)
                StartCoroutine(ReduceSoundValue(enemy));
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }

    }
}