using System.Collections;
using UnityEngine;

public class DamageArea : MonoBehaviour
{   
    public float damageInterval;
    public float damageAmount;
    public float delayBeforeTakingDamage;
    public bool cantakedamage;
    public Sanity playerSanity;
    public GameManager gameManager;

    private bool isDamageRoutineRunning = false;

    private void Start()
    {
        playerSanity = FindObjectOfType<Sanity>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(cantakedamage)
        {
        HandlePlayerCollision(other);
        }
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (!CheckLightPlayer(player) && !isDamageRoutineRunning )
        {
            StartCoroutine(DamageRoutine(player));
        }
    }

    private void OnTriggerExit2D(Collider2D player)
    {
        isDamageRoutineRunning = false;
        Debug.Log(isDamageRoutineRunning);
        StopCoroutine(DamageRoutine(player));  
    }

    private void HandlePlayerCollision(Collider2D player)
    {
        if (player.GetComponent<OnOffLight>() != null)  
        {
            if (!CheckLightPlayer(player)  && !isDamageRoutineRunning)
            {     
                StartCoroutine(DamageRoutine(player));
            }
            else if (isDamageRoutineRunning)
            { 
                StopCoroutine(DamageRoutine(player));
            }
        }
    }
    private IEnumerator DamageRoutine(Collider2D player)
    {
        isDamageRoutineRunning = true;
        yield return new WaitForSeconds(delayBeforeTakingDamage);

        while (!CheckLightPlayer(player) && isDamageRoutineRunning &&  player.GetComponent<Sanity>() != null)
        {
            var damage = (damageAmount * gameManager.gameDifficulty) - playerSanity.SanityResistance;         
            player.GetComponent<Sanity>().TakeSanity(damage);  
            yield return new WaitForSeconds(damageInterval);
        }

        isDamageRoutineRunning = false;
    }

    private bool CheckLightPlayer(Collider2D player)
    {
        if(player.GetComponent<OnOffLight>() != null)
            return player.GetComponent<OnOffLight>().checkLight;
        else 
            return false;
    }
}
