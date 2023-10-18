using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSafeRoom : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D player)
    {
        if (!player.GetComponent<NewMovementPlayer>().isStaySafeRoom && player.GetComponent<NewMovementPlayer>() != null)
            player.GetComponent<NewMovementPlayer>().isStaySafeRoom = true;
    }
    private void OnTriggerExit2D(Collider2D player)
    {
        if(player.GetComponent<NewMovementPlayer>().isStaySafeRoom && player.GetComponent<NewMovementPlayer>() != null)
            player.GetComponent<NewMovementPlayer>().isStaySafeRoom = false;
    }
}
