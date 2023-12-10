using System.Collections;
using System.Collections.Generic;
using Enemy_State;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public EnemyNormal enemyNormal;
    public Animator animationActtack;
    private bool isattackcalled = false;
    public SoundManager soundManager;
    
    private void Start()
    {
        enemyNormal = FindObjectOfType<EnemyNormal>();
        soundManager = FindObjectOfType<SoundManager>();
    }
    private void OnTriggerEnter2D(Collider2D player)
    {   
        if(!isattackcalled)
        {
        if(player.GetComponent<Hp>() != null && enemyNormal.currentState == enemyNormal.state_Hunting)
        StartCoroutine(EnermyAttack(2.0f));
        }
    }
    private IEnumerator EnermyAttack(float waitTime)
    {   
        isattackcalled = true;
        animationActtack.SetBool("isAttack", true);
        enemyNormal.agent.speed = 4.5f;                 
        yield return new WaitForSeconds(waitTime);
        soundManager.PlaySound("Bite");
        enemyNormal.agent.speed = enemyNormal.speed;
        animationActtack.SetBool("isAttack", false);
        this.GetComponent<BoxCollider2D>().enabled = false;
        this.GetComponent<BoxCollider2D>().enabled = true;
        isattackcalled = false;
    }

}
