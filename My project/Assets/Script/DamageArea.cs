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
        Debug.Log(playerSanity.SanityResistance);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        HandlePlayerCollision(other);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsPlayer(other) && !isDamageRoutineRunning)
        {
            StartCoroutine(DamageRoutine(other));
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (IsPlayer(other))
        {
            StopCoroutine(DamageRoutine(other));
        }
    }

    private void HandlePlayerCollision(Collider2D player)
    {

        if (IsPlayer(player))
        {
            if (CheckLightPlayer(player)  && !isDamageRoutineRunning)
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

        while (CheckLightPlayer(player) == false)
        {
            var damage = (damageAmount * gameManager.gameDifficulty) - playerSanity.SanityResistance;
            player.GetComponent<HpAndSanity>().TakeSanity(damage);
            yield return new WaitForSeconds(damageInterval);
        }
        isDamageRoutineRunning = false;
    }

    private bool CheckLightPlayer(Collider2D player)
    {
        return player.GetComponent<OnOffLight>().checkLight;
    }

    private bool IsPlayer(Collider2D collider)
    {
        return collider.CompareTag("Player");
    }
}
