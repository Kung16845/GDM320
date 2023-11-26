using System.Collections;
using System.Collections.Generic;
using Enemy_State;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyNormal enemyNormal;
    public Animator animationActtack;
    public SoundManager soundManager;
    
    private void Start()
    {
        enemyNormal = FindObjectOfType<EnemyNormal>();
        soundManager = FindObjectOfType<SoundManager>();
    }
    private void OnTriggerEnter2D(Collider2D player)
    {   
        if(player.GetComponent<Hp>() != null && enemyNormal.currentState == enemyNormal.state_Hunting)
            StartCoroutine(EnermyAttack(2.0f));
    }
    private IEnumerator EnermyAttack(float waitTime)
    {   
        enemyNormal.agent.speed = 1.5f;
        animationActtack.SetBool("isAttack", true);                 //Play Attack Animation
        yield return new WaitForSeconds(waitTime);
        soundManager.PlaySound("Bite");
        enemyNormal.agent.speed = enemyNormal.speed;
        animationActtack.SetBool("isAttack", false);
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.GetComponent<BoxCollider2D>().enabled = true;
    }

}
