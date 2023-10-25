using Enemy_State;
using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;  // Bullet speed.
    public float damage = 1.0f; // Bullet damage.
    private SpriteRenderer targetSpriteRenderer; // Reference to the target object's SpriteRenderer component.

    void Start()
    {
        // Find the target object by its name and get its SpriteRenderer component.
        GameObject targetObject = GameObject.Find("Enemy/Velo_Sprite");
        if (targetObject != null)
        {
            targetSpriteRenderer = targetObject.GetComponent<SpriteRenderer>();
            if (targetSpriteRenderer == null)
            {
                Debug.LogError("SpriteRenderer component not found on the target object's child (Velo_Sprite).");
            }
        }
        else
        {
            Debug.LogError("Target object with name 'Enemy/Velo_Sprite' not found!");
        }
    }

    void Update()
    {
        MoveBullet();
    }

    void MoveBullet()
    {
        // Move the bullet forward based on its speed.
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<EnemyNormal>())
        {
            collision.GetComponent<EnemyNormal>().TakeDamage(damage);
            StartCoroutine(ChangeSpriteColorForOneSecond());
            
        }
        // Destroy the bullet on collision with any object.
        if (collision.tag == "Walls")
        {
            Destroy(this.gameObject);
        }
    }

    private IEnumerator ChangeSpriteColorForOneSecond()
    {
        targetSpriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        Debug.Log("Colour");
        targetSpriteRenderer.color = Color.white;
        Destroy(this.gameObject);
    }
}
