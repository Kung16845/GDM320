using System.Collections;
using System.Collections.Generic;
using Enemy_State;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyNormal enemyNormal;
    public Animation animationActtack;
    private void Start()
    {
        enemyNormal = FindObjectOfType<EnemyNormal>();
    }
    private void OnTriggerEnter2D(Collider2D player)
    {
        // StartCoroutine(EnemyAttack(2.0f,player));
        Debug.Log("Player Taken Damage ");
        if (player.GetComponent<Hp>() != null)
        {
            if (enemyNormal.currentState == enemyNormal.state_Hunting)
            {
                player.GetComponent<Hp>().TakeDamage(enemyNormal.damage);
                Debug.Log("Player Taken Damage  ");
            }
        }
    }
}
