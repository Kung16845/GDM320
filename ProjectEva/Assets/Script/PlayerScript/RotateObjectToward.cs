using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class RotateObjectToward : MonoBehaviour
{   
    public Transform tranformplayer;
    public float Anglemouse;
    public float AnglemouseMin;
    public float AnglemouseMax;
    public float playertransfromZ;
    public float Anglebetween;
    private void Start()
    {
        tranformplayer = FindObjectOfType<PlayerMovement>().transform;
    }
    private void Update() 
    {   
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //รับต่ำแหน่งจากเมาส์โดยอ้างอิงจาก main camera 
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        Anglemouse = Vector2toAngle(direction);

        Vector3 directionplayer = tranformplayer.up; 
        playertransfromZ = Mathf.Atan2(directionplayer.y, directionplayer.x) * Mathf.Rad2Deg;

        AnglemouseMin =   playertransfromZ -45f;
        AnglemouseMax =   playertransfromZ +45f;

        if( AnglemouseMax > 180) 
        {   
            AnglemouseMax -= 360f; 
            if(Anglemouse > 0 && Anglemouse <=180)
                Anglebetween = Mathf.Clamp(Anglemouse ,AnglemouseMin ,180f);   
            else if(Anglemouse < 0 && Anglemouse >= -180)
                Anglebetween = Mathf.Clamp(Anglemouse ,-180f ,AnglemouseMax);
        }   
        else if(AnglemouseMin < -180)
        {   
            AnglemouseMin += 360f;
            if(Anglemouse > 0 && Anglemouse <=180)
                Anglebetween = Mathf.Clamp(Anglemouse ,AnglemouseMin ,180f);   
            else if(Anglemouse < 0 && Anglemouse >= -180)
                Anglebetween = Mathf.Clamp(Anglemouse ,-180f ,AnglemouseMax);
        }   
        else
        {
            Anglebetween = Mathf.Clamp(Anglemouse ,AnglemouseMin ,AnglemouseMax);
        }
        
        this.transform.up = AngletoVector2(Anglebetween);
    
    }
    public float Vector2toAngle(Vector2 vector)
    {
        return Mathf.Atan2(vector.y,vector.x) * Mathf.Rad2Deg;
    }
    public Vector2 AngletoVector2(float Angle)    
    {
        float radians = Angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians)); 
    }
}
