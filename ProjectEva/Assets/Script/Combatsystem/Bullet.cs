using Enemy_State;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;  // Bullet speed.
    public float damage = 1.0f; // Bullet damage.

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
        if(collision.GetComponent<EnemyNormal>() != null) 
        {
            collision.GetComponent<EnemyNormal>().TakeDamage(damage);
            Destroy(gameObject);
        }
        // Destroy the bullet on collision with any object.
        if (collision.tag == "Walls")
        {
        Destroy(this.gameObject);
        }
    }
    
}
