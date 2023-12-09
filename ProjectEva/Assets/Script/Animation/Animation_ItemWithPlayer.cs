using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class Animation_ItemWithPlayer : MonoBehaviour
{
    public Animation_PlayerMovement PlayerAnim;


    // Update is called once per frame
    void Update()
    {
        float faceX = PlayerAnim.movement.x;
        float faceY = PlayerAnim.movement.y;
        

    }
}
