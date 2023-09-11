using UnityEngine;

public class RotationClamp : MonoBehaviour
{
    public float maxTurnAngle = 45f;
    private Quaternion lastRotation;

    void Start()
    {
        lastRotation = transform.rotation;
    }

    void Update()
    {
        // Here, add the logic to calculate the desired new rotation
        // As an example, I am taking a simple input method. You can replace this with your actual input method
        float turn = Input.GetAxis("Horizontal") * Time.deltaTime * 150f;
        
        Quaternion targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + turn, 0);

        // Calculate the angle difference
        float angleDifference = Quaternion.Angle(lastRotation, targetRotation);

        // If the angle difference is within the limit, allow the rotation, else revert to the last rotation
        if (angleDifference <= maxTurnAngle)
        {
            transform.rotation = targetRotation;
            lastRotation = targetRotation;
        }
    }
}