using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemheal : MonoBehaviour
{
    public float sanityheal = 0;
    public float hpheal = 0;
    private void OnTriggerEnter2D(Collider2D player)
    {
        if(player.GetComponent<Sanity>() != null)
        {
            player.GetComponent<Sanity>().HealSanity(sanityheal);  
            player.GetComponent<Hp>().HealHp(hpheal);  
            Destroy(this.gameObject); 
        }
    }
}
