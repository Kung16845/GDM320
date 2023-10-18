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
        public float soundValue;
        public float maxSoundValue;
        public bool isCountingValueSound;
        public bool isRunningReduceSoundValue;
        public NewMovementPlayer newMovementPlayer;
        public Pistol pistol;
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
                if (!isCountingValueSound)
                    StartCoroutine(DelayTimeCountSoundValue());

                if (soundValue >= maxSoundValue)
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

                if (time >= timeDelay && !enemy.isHear && soundValue > 0)
                {
                    time = 0;
                    StartCoroutine(ReduceSoundValue(enemy));
                    isRunningReduceSoundValue = true;
                }
            }
            if (soundValue > 16)
                soundValue = 16;

            if (enemy.currentState == enemy.state_Listening)
                radius = Saveradius / 2;
            else
                radius = Saveradius;
        }
        IEnumerator DelayTimeCountSoundValue()
        {
            isCountingValueSound = true;
            if (newMovementPlayer.isRunning)
            {
                if (enemy.currentState = enemy.state_Listening)
                    soundValue += 16;
                else
                    soundValue += 8;
            }
            else if (pistol.isshoot)
                soundValue += 16;

            else if (newMovementPlayer.isWalking)
            {
                if (enemy.currentState = enemy.state_Listening)
                    soundValue += 4 * 2;
                else
                    soundValue += 4;
            }
            else
                soundValue += 0;
            yield return new WaitForSeconds(4.0f);
            isCountingValueSound = false;
        }
        IEnumerator ReduceSoundValue(Enemy enemy)
        {
            soundValue -= 1;
            Debug.Log("ReduceSoundValue");
            time = 0;
            yield return new WaitForSeconds(1.0f);
            if (soundValue > 0 && isRunningReduceSoundValue)
                StartCoroutine(ReduceSoundValue(enemy));
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }

    }
}