using System.Collections;
using UnityEngine;

public class DamageArea : MonoBehaviour
{
    [Header("Damage Settings")]
    public float damageInterval;
    public float damageAmount;
    public float delayBeforeTakingDamage;

    [Header("Dependencies")]
    public HpAndSanity playerSanity;
    public GameManager gameManager;

    private bool isReadyToDamage = false;
    private bool isPlayerInsideArea = false;
    private bool isDamageRoutineRunning = false;

    private void Start()
    {
        Debug.Log(playerSanity.SanityResistance);
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (IsPlayer(other))
        {
            if (ShouldStartDamageRoutine(other))
            {
                StartDamageRoutine(other.gameObject);
            }
            else if (ShouldStopDamageRoutine(other))
            {
                StopDamageRoutine(other.gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (IsPlayer(other) && !isDamageRoutineRunning && !IsPlayerLightOn(other))
        {
            StartDamageRoutine(other.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (IsPlayer(other))
        {
            StopDamageRoutine(other.gameObject);
        }
    }

    private bool IsPlayer(Collider2D collider)
    {
        return collider.CompareTag("Player");
    }

    private bool ShouldStartDamageRoutine(Collider2D other)
    {
        return !IsPlayerLightOn(other) && !isPlayerInsideArea && !isDamageRoutineRunning;
    }

    private bool ShouldStopDamageRoutine(Collider2D other)
    {
        return IsPlayerLightOn(other) && isPlayerInsideArea;
    }

    private bool IsPlayerLightOn(Collider2D other)
    {
        var lightComponent = other.GetComponent<OnOffLight>();
        return lightComponent != null && lightComponent.checkLight;
    }

    private void StartDamageRoutine(GameObject player)
    {
        isPlayerInsideArea = true;
        StartCoroutine(DamageRoutine(player));
    }

    private void StopDamageRoutine(GameObject player)
    {
        isPlayerInsideArea = false;
        StopCoroutine(DamageRoutine(player));
    }

    private IEnumerator DamageRoutine(GameObject player)
    {
        isDamageRoutineRunning = true;

        if (!isReadyToDamage)
        {
            yield return new WaitForSeconds(delayBeforeTakingDamage);
            isReadyToDamage = true;
        }

        while (isPlayerInsideArea && isReadyToDamage)
        {
            ApplyDamageToPlayer(player);
            yield return new WaitForSeconds(damageInterval);
        }

        isDamageRoutineRunning = false;
    }

    private void ApplyDamageToPlayer(GameObject player)
    {
        var damage = CalculateDamage();
        player.GetComponent<HpAndSanity>().TakeSanity(damage);
    }

    private float CalculateDamage()
    {
        return (damageAmount * gameManager.gameDifficulty) - playerSanity.SanityResistance;
    }
}
