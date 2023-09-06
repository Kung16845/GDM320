using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupItem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D player) 
    {     
        if(player.GetComponent<OnOffLight>() != null)
        {
            player.GetComponent<OnOffLight>().isHave = true;
            Destroy(this.gameObject);
        }
    }
}
