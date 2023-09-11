using System.Collections;
using UnityEngine;

public class DamageArea : MonoBehaviour
{   
    public float damageInterval;
    public float damageAmount;
    public float delayBeforeTakingDamage;

    public HpAndSanity playerSanity;
    public GameManager gameManager;

    private bool isDamageRoutineRunning = false;

    private void Start()
    {
        playerSanity = FindObjectOfType<HpAndSanity>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        HandlePlayerCollision(other);
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if (!CheckLightPlayer(player) && !isDamageRoutineRunning)
        {
            StartCoroutine(DamageRoutine(player));
        }
    }

    private void OnTriggerExit2D(Collider2D player)
    {
        if (CheckLightPlayer(player) || !CheckLightPlayer(player))
        {
            isDamageRoutineRunning = false;
            Debug.Log(isDamageRoutineRunning);
            StopCoroutine(DamageRoutine(player));
        }
    }

    private void HandlePlayerCollision(Collider2D player)
    {
        if (player.GetComponent<OnOffLight>() != null)  
        {
            if (!CheckLightPlayer(player)  && !isDamageRoutineRunning)
            {     
                StartCoroutine(DamageRoutine(player));
            }
            else if (CheckLightPlayer(player))
            { 
                StopCoroutine(DamageRoutine(player));
            }
        }
    }
    private IEnumerator DamageRoutine(Collider2D player)
    {
        isDamageRoutineRunning = true;
        yield return new WaitForSeconds(delayBeforeTakingDamage);
        
        while (!CheckLightPlayer(player) && isDamageRoutineRunning)
        {
            var damage = (damageAmount * gameManager.gameDifficulty) - playerSanity.SanityResistance;
            player.GetComponent<HpAndSanity>().TakeSanity(damage);
            Debug.Log("TakingDamge");
            yield return new WaitForSeconds(damageInterval);
        }

        isDamageRoutineRunning = false;
    }

    private bool CheckLightPlayer(Collider2D player)
    {
        return player.GetComponent<OnOffLight>().checkLight;
    }
}
