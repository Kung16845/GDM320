using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ShowColliders : MonoBehaviour
{   
    public Collider2D[] colliders;
    private void Start() 
    {
        
    }
    private void OnDrawGizmos()
    {
        foreach (Collider2D collider in colliders)
        {
            // ตรวจสอบว่า Collider นี้เป็น Collider 2D แบบไหน
            Gizmos.color = Color.green; // สีเริ่มต้น
            if (collider is BoxCollider2D boxCollider)
            {
                Gizmos.DrawWireCube(collider.bounds.center, collider.bounds.size);
            }
            else if (collider is CircleCollider2D circleCollider)
            {
                Gizmos.DrawWireSphere(collider.bounds.center, (collider as CircleCollider2D).radius);
            }
            else if (collider is PolygonCollider2D polygonCollider)
            {
                Vector2[] pathPoints = polygonCollider.GetPath(0);
                for (int i = 0; i < pathPoints.Length - 1; i++)
                {
                    Gizmos.DrawLine(pathPoints[i], pathPoints[i + 1]);
                }
                Gizmos.DrawLine(pathPoints[pathPoints.Length - 1], pathPoints[0]);
            }
        }
    }
}
