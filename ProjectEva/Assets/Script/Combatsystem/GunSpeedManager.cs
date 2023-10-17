using UnityEngine;
using UnityEngine.UI;

public class GunSpeedManager : MonoBehaviour
{
    private SanityScaleController sanityScaleController;
    public NewMovementPlayer newMovementPlayer;
    public bool isRunning = false;
    public bool isCrouching = false;
    public float originalSpeed;
    public float originalSlowSpeed;
    public float originalRunningSpeed;
    public float AimmingSpeed;
    public float reloadspeed;

    void Start() // It should be "Start" with a capital "S"
    {
        newMovementPlayer = FindObjectOfType<NewMovementPlayer>();
        originalSpeed = newMovementPlayer.speed;
        originalSlowSpeed = newMovementPlayer.crouchSpeed;
        originalRunningSpeed = newMovementPlayer.runspeed;
        sanityScaleController = FindObjectOfType<SanityScaleController>();
    }

    void Update()
    {
        isCrouching = newMovementPlayer.isCrouching;
        isRunning = newMovementPlayer.isRunning;
    }
    public void ReduceSpeedDuringAimming()
    {
        newMovementPlayer.speed = AimmingSpeed * sanityScaleController.GetSpeedScale();
    }
    public void ReduceSpeedDuringReload()
    {
        newMovementPlayer.speed = reloadspeed * sanityScaleController.GetSpeedScale();
    }

    public void RestoreNormalSpeed()
    {
        newMovementPlayer.speed = originalSpeed;
        newMovementPlayer.crouchSpeed = originalSlowSpeed;
        newMovementPlayer.runspeed = originalRunningSpeed;
    }
}
