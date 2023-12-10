using System.Collections;
using System.Collections.Generic;
using Enemy_State;
using UnityEngine;

public class Hitbox : MonoBehaviour
{   
    public EnemyNormal enemyNormal;
    private bool damagetaken = false;
    private void Start() {
        enemyNormal = GameObject.FindAnyObjectByType<EnemyNormal>();
    }
    private void OnTriggerEnter2D(Collider2D player)
    {   

        if(player.GetComponent<Hp>() != null && enemyNormal.currentState == enemyNormal.state_Hunting)
        {
            if(!damagetaken)
            {
            damagetaken = true;
            player.GetComponent<Hp>().TakeDamage(enemyNormal.damage);
            damagetaken = false;
            }
        }

    }
}
