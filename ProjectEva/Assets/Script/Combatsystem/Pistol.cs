using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pistol : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce = 10f;
    [SerializeField] public int maxAmmo = 10;
    [SerializeField] public int ammoInChamber = 5;
    [SerializeField] float bulletDamage = 1.0f;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float maxAccuracy = 1.0f;
    [SerializeField] float minAccuracy = 0.2f;
    [SerializeField] float cooldownTime = 1.5f;
    [SerializeField] float accuracyIncreaseRate = 0.2f;
    [SerializeField] float accuracyDecreaseRate = 0.1f;
    [SerializeField] float reloadTime = 2.0f;
    [SerializeField] GameObject accuracyCircle; // Reference to the accuracy circle GameObject.
    private SanityScaleController sanityScaleController;
    public float currentAccuracy = 0.2f;
    private Transform firePoint;
    private float lastShotTime;
    public bool isAiming;
    public bool isReloading;
    private float reloadStartTime;
    private int bulletsToReload;
    private GunSpeedManager gunSpeedManager;
    public ObjectPolling SoundWave;
    public bool isshoot = false;
    public SoundManager soundManager;

    // New variables for ammo
    public int currentAmmo; // Current total ammo the player has.
    
    void Start()
    {
        initializevariable();
    }

    void Update()
    {
        RotateTowardsMouse();
        Decreasemaxacrrancywhilemoving();
        if (Input.GetMouseButton(1) && !gunSpeedManager.isRunning)
        {   
            createcrosshaircircle();
        }
        else
        {   
            removecrosshaircircle();
        }
        float scaledAccuracy = 0.05f / currentAccuracy;
        accuracyCircle.transform.localScale = new Vector3(scaledAccuracy, scaledAccuracy, 1f);

        if (isReloading)
        {
            gunSpeedManager.ReduceSpeedDuringReload();
            if (Input.GetMouseButton(1) || Input.GetKeyDown(KeyCode.LeftShift)) // Check if right mouse button is pressed.
            {
                cancelreload();
            }
            else if (Time.time - reloadStartTime >= reloadTime)
            {
                FinishReload();
            }
        }
        else if (Input.GetMouseButtonDown(0) && ammoInChamber > 0 && Time.time - lastShotTime >= cooldownTime)
        {
            Shoot();
            lastShotTime = Time.time;
        }
        else if (Input.GetMouseButtonDown(0) && ammoInChamber == 0 && Time.time - lastShotTime >= cooldownTime)
        {
            soundManager.PlaySound("Drymag");
        }
        else if (Input.GetKeyDown(KeyCode.R) && !gunSpeedManager.isRunning && ammoInChamber < maxAmmo && !isAiming)
        {
            // Check if the player has enough total ammo to reload.
            if (currentAmmo > 0)
            {
                StartReload();
            }
        }
        movemouseposition();
    }

    void movemouseposition()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Ensure the circle is at the same depth as the player.
        accuracyCircle.transform.position = mousePosition;
    }
    void RotateTowardsMouse()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    void Shoot()
    {
        isshoot = true;
        createBullet();
        SoundWave.SpawnFromPool("Sound Wave Gun", this.transform.position, Quaternion.identity);
        ammoInChamber--;
        soundManager.PlaySound("Fire");
        StartCoroutine(ResetShootFlagAfterDelay());
    }

    IEnumerator ResetShootFlagAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        isshoot = false;
         if(ammoInChamber > 0)
        {
            soundManager.PlaySound("Cock"); 
        }
    }

    private void StartReload()
    {
        if (ammoInChamber < maxAmmo && currentAmmo > 0)
        {   
            StartCoroutine(PlayPullmag());
            gunSpeedManager.ReduceSpeedDuringReload();
            isReloading = true;
            reloadStartTime = Time.time;
            bulletsToReload = maxAmmo - ammoInChamber;
            // Decrease current ammo when starting a reload.
        }
    }
    IEnumerator Playcockinlastbullet()
    {
        yield return new WaitForSeconds(1.5f);
            soundManager.PlaySound("Cock"); 
    }
    IEnumerator PlayPullmag()
    {
        yield return new WaitForSeconds(0.4f);
        soundManager.PlaySound("Pullmag"); 
        yield return new WaitForSeconds(0.8f);
        soundManager.PlaySound("Ammoshelldrop");
    }
    void cancelreload()
    {
        isReloading = false;
        bulletsToReload = 0; // Reset the bullets to reload.
        // Restore the player's normal speed when the reload is canceled.
        gunSpeedManager.RestoreNormalSpeed();
    }
    private void FinishReload()
    {
        if (bulletsToReload > 0 && currentAmmo > 0)
        {
            decreaseammowhenreload();
            
            if (bulletsToReload == 0 || currentAmmo == 0)
            {
                // No more bullets to reload and only one bullet left in the chamber, play the "Cock" sound.
                StartCoroutine(Playcockinlastbullet());
            }
        }
        else
        {
            isReloading = false;
            // Restore the player's normal speed when the reload is finished.
            gunSpeedManager.RestoreNormalSpeed();
        }
    }
    void createBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 bulletDirection = (Vector2)firePoint.up + Random.insideUnitCircle * (1 - currentAccuracy);
        rb.AddForce(bulletDirection.normalized * bulletForce, ForceMode2D.Impulse);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.damage = bulletDamage;
    }
    void decreaseammowhenreload()
    {
        ammoInChamber++;
        bulletsToReload--;
        currentAmmo--;
        reloadStartTime = Time.time; // Start the reload of the next bullet.
        soundManager.PlaySound("Reload");
    }
    void initializevariable()
    {
        firePoint = transform.Find("FirePoint");
        lastShotTime = -cooldownTime;
        isAiming = false;
        isReloading = false;
        bulletsToReload = 0;
        sanityScaleController = FindObjectOfType<SanityScaleController>();
        gunSpeedManager = FindObjectOfType<GunSpeedManager>();
        soundManager = FindObjectOfType<SoundManager>();
    }
    void createcrosshaircircle()
    {
        gunSpeedManager.ReduceSpeedDuringAimming();
        accuracyCircle.SetActive(true);
        isAiming = true;
        if(currentAccuracy > maxAccuracy)
        {
            currentAccuracy = Mathf.Lerp(currentAccuracy, maxAccuracy * sanityScaleController.GetAccuracyScale(), accuracyDecreaseRate * Time.deltaTime);
        }
        else
        {
            currentAccuracy = Mathf.Lerp(currentAccuracy, maxAccuracy * sanityScaleController.GetAccuracyScale(), accuracyIncreaseRate * Time.deltaTime);
        }
    }
    void removecrosshaircircle()
    {
        gunSpeedManager.RestoreNormalSpeed();
        isAiming = false;
        accuracyCircle.SetActive(false);
        currentAccuracy = Mathf.Lerp(currentAccuracy, minAccuracy , accuracyDecreaseRate * Time.deltaTime);
    }
    void Decreasemaxacrrancywhilemoving()
    {
        if(!gunSpeedManager.IsPlayerNotMoving())
        {
            maxAccuracy = 0.4f;
            if(currentAccuracy > maxAccuracy)
            {
                accuracyIncreaseRate = 0;
                accuracyDecreaseRate = 15;
            }
        }
        else if(gunSpeedManager.IsPlayerNotMoving())
        {
            maxAccuracy = 1;
            accuracyDecreaseRate = 3;
            accuracyIncreaseRate = 0.2f;
        }
    }
}