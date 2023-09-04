using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Itemheal : MonoBehaviour
{
    public float sanityheal = 0;
    public float hpheal = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<HpAndSanity>().HealSanity(sanityheal);
            other.GetComponent<HpAndSanity>().HealHp(hpheal);  
            Destroy(this.gameObject);
        }
    }
}
