using Enemy_State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy_State
{
    public class Enemyfootstep : MonoBehaviour
    {
        public EnemyNormal enemyNormal;
        private Vector2 previousPosition;
        public bool isMoving;
        public WalkSoundManager soundManager;
        public string nameofsound;
        public GameObject currentSoundObject;
        public Velo_movement velo_movement;
        public bool screamed = false;

        private float positionThreshold = 0.01f; // Adjust this threshold as needed

        private void Start()
        {
            velo_movement = FindObjectOfType<Velo_movement>();
            previousPosition = transform.position;
        }

        void Update()
        {
            // Check if the distance between the current and previous positions exceeds the threshold
            isMoving = Vector2.Distance((Vector2)transform.position, previousPosition) > positionThreshold;

            // Update the previous position for the next frame
            previousPosition = transform.position;

            if (isMoving && enemyNormal.iswalkingonfloor)
            {
                if (currentSoundObject == null || !currentSoundObject.GetComponent<AudioSource>().isPlaying)
                {
                    currentSoundObject = soundManager.PlaySound(nameofsound, transform);
                }
            }

            if (isMoving && enemyNormal.iswalkingontunnel)
            {
                if (currentSoundObject == null || !currentSoundObject.GetComponent<AudioSource>().isPlaying)
                {
                    currentSoundObject = soundManager.PlaySound("walking in tunnel", transform);
                }
            }
            if(!screamed && velo_movement.isRoaming)
                if (currentSoundObject == null || !currentSoundObject.GetComponent<AudioSource>().isPlaying)
                {
                    screamed = true;
                    currentSoundObject = soundManager.PlaySound("Walkingoutscream", transform);
                }
            if(!velo_movement.isRoaming)
            {
            screamed = false;
            }
        }
    }
}
