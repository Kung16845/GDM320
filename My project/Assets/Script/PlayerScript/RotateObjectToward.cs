using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class RotateObjectToward : MonoBehaviour
{   
    public float anglemin;
    public float AxisZ;
    public float anglemax;
    public float CalAngke;
    public Vector2 directionrotate;

    private void Update() 
    {   
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
        transform.up = direction;
        directionrotate = direction;

                
    }
}