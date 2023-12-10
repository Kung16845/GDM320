using UnityEngine;

public class PocketFlashlight : MonoBehaviour
{
    public GameObject Player;
    public float AngleBetween;
    public float directionX;
    public float directionY;

    private void Start()
    {
        Player = FindObjectOfType<NewMovementPlayer>().gameObject;
    }

    private void Update()
    {
        directionX = Input.GetAxisRaw("Horizontal");
        directionY = Input.GetAxisRaw("Vertical");

        if (directionX > 0)
        {
            AngleBetween = 0f;
        }
        else if (directionX < 0)
        {
            AngleBetween = 180f;
        }
        else if (directionY > 0)
        {
            AngleBetween = 90f;
        }
        else if (directionY < 0)
        {
            AngleBetween = 270f;
        }

        // Update the light direction based on the calculated angle
        this.transform.up = AngleToVector2(AngleBetween);
    }

    public Vector2 AngleToVector2(float angle)
    {
        float radians = angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
    }
}
