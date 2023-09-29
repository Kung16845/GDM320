using UnityEngine;

public class ShowColliders : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        // ดึง Collider ทั้งหมดในฉาก
        Collider2D[] colliders = FindObjectsOfType<Collider2D>();

        foreach (Collider2D collider in colliders)
        {
            // ตรวจสอบว่า Collider นี้เป็น Collider 2D หรือ 3D
            if (collider is Collider2D collider2D)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(collider2D.bounds.center, collider2D.bounds.size);
            }
            else if (collider is Collider2D collider3D)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireCube(collider3D.bounds.center, collider3D.bounds.size);
            }
        }
    }
}
