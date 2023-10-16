using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class NewRotateLight : MonoBehaviour
{
    public GameObject Player;
    public float Anglemouse;
    public float Anglebetween;
    public float directionX;
    public float directionY;
    public bool North;
    public bool South;
    public bool West;
    public bool East;

    private void Start()
    {
        Player = FindObjectOfType<NewMovementPlayer>().gameObject;
        directionX = Player.GetComponent<NewMovementPlayer>().direction.x;
        directionY = Player.GetComponent<NewMovementPlayer>().direction.y;
        StartCoroutine(CheckDirector());
        East = true;
    }
    private void Update()
    {
        if (transform.hasChanged)
        {
            Debug.Log("The Rotate has changed!");
            directionX = Player.GetComponent<NewMovementPlayer>().direction.x;
            directionY = Player.GetComponent<NewMovementPlayer>().direction.y;
            if (directionX > 0)
            {   
                North = false;
                South = false;
                West = false;
                East = true;
            }
            else if(directionX < 0)
            {
                North = false;
                South = false;
                West = true;
                East = false;
            }
            else if(directionY > 0)
            {
                North = true;
                South = false;
                West = false;
                East = false;
            }
            else if(directionY < 0)
            {
                North = false;
                South = true;
                West = false;
                East = false;
            }

            transform.hasChanged = false;
        }

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition); //รับต่ำแหน่งจากเมาส์โดยอ้างอิงจาก main camera 
        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        Anglemouse = Vector2toAngle(direction);

        if (East)
            Anglebetween = Mathf.Clamp(Anglemouse, -45, 45);
        else if (North)
            Anglebetween = Mathf.Clamp(Anglemouse, 45, 135);
        else if (South)
            Anglebetween = Mathf.Clamp(Anglemouse, -135, -45);
        else if (West)
        {
            if (Anglemouse > 0 && Anglemouse <= 180)
                Anglebetween = Mathf.Clamp(Anglemouse, 135, 180);
            else if (Anglemouse < 0 && Anglemouse >= -180)
                Anglebetween = Mathf.Clamp(Anglemouse, -180, -135);
        }


        this.transform.up = AngletoVector2(Anglebetween);
    }
    IEnumerator CheckDirector()
    {

        yield return new WaitForSeconds(1.0f);


    }
    public float Vector2toAngle(Vector2 vector)
    {
        return Mathf.Atan2(vector.y, vector.x) * Mathf.Rad2Deg;
    }
    public Vector2 AngletoVector2(float Angle)
    {
        float radians = Angle * Mathf.Deg2Rad;
        return new Vector2(Mathf.Cos(radians), Mathf.Sin(radians));
    }
}