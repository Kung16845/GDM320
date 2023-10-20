using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSafeRoom : MonoBehaviour
{
    
    private void OnTriggerStay2D(Collider2D player)
    {   
        var NewMovementPlayer = player.GetComponent<NewMovementPlayer>();
        if (NewMovementPlayer != null)
        {
            if (!NewMovementPlayer.isStaySafeRoom)
                NewMovementPlayer.isStaySafeRoom = true;
        }
    }
    private void OnTriggerExit2D(Collider2D player)
    {   
        var NewMovementPlayer = player.GetComponent<NewMovementPlayer>();
        if (NewMovementPlayer != null)
        {
            if (NewMovementPlayer.isStaySafeRoom)
                NewMovementPlayer.isStaySafeRoom = false;
        }
    }
}
