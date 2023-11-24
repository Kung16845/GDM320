using UnityEngine;

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

    private void Start()
    {
        previousPosition = transform.position;
    }
    void Update()
    {
        // Check if the current position is different from the previous position
        isMoving = transform.position != (Vector3)previousPosition;
        // Update the previous position for the next frame
        previousPosition = transform.position;
        if (isMoving && enemyNormal.iswalkingonfloor)
        {
            if (currentSoundObject == null || !currentSoundObject.GetComponent<AudioSource>().isPlaying)
            {
                currentSoundObject = soundManager.PlaySound("Spiderwalk",transform);
            }
        }
    }   
    }
}
