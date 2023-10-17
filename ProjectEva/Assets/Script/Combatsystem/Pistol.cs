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
    [SerializeField] float cooldownTime = 0.5f;
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

    // New variables for ammo
    public int currentAmmo; // Current total ammo the player has.
    
    void Start()
    {
        firePoint = transform.Find("FirePoint");
        lastShotTime = -cooldownTime;
        isAiming = false;
        isReloading = false;
        bulletsToReload = 0;
        sanityScaleController = FindObjectOfType<SanityScaleController>();
        gunSpeedManager = FindObjectOfType<GunSpeedManager>();

        // Initialize current ammo to max ammo at the start.
    }

    void Update()
    {
        RotateTowardsMouse();

        if (Input.GetMouseButton(1) && !gunSpeedManager.isRunning)
        {   
            gunSpeedManager.ReduceSpeedDuringAimming();
            accuracyCircle.SetActive(true);
            isAiming = true;
            currentAccuracy = Mathf.Lerp(currentAccuracy, maxAccuracy * sanityScaleController.GetAccuracyScale(), accuracyIncreaseRate * Time.deltaTime);
        }
        else
        {   
            gunSpeedManager.RestoreNormalSpeed();
            isAiming = false;
            accuracyCircle.SetActive(false);
            currentAccuracy = Mathf.Lerp(currentAccuracy, minAccuracy , accuracyDecreaseRate * Time.deltaTime);
        }

        // Scale the accuracy circle based on the currentAccuracy.
        float scaledAccuracy = 0.05f / currentAccuracy;
        accuracyCircle.transform.localScale = new Vector3(scaledAccuracy, scaledAccuracy, 1f);

        if (isReloading)
        {
            gunSpeedManager.ReduceSpeedDuringReload();
            if (Input.GetMouseButton(1) || Input.GetKeyDown(KeyCode.LeftShift)) // Check if right mouse button is pressed.
            {
                // Cancel the reload if right mouse button is pressed.
                isReloading = false;
                bulletsToReload = 0; // Reset the bullets to reload.
                
                // Restore the player's normal speed when the reload is canceled.
                gunSpeedManager.RestoreNormalSpeed();
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
        else if (Input.GetKeyDown(KeyCode.R) && !gunSpeedManager.isRunning && ammoInChamber < maxAmmo && !isAiming)
        {
            // Check if the player has enough total ammo to reload.
            if (currentAmmo > 0)
            {
                StartReload();
            }
        }

        // Move the accuracy circle to the mouse position in world coordinates.
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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        Vector2 bulletDirection = (Vector2)firePoint.up + Random.insideUnitCircle * (1 - currentAccuracy);
        rb.AddForce(bulletDirection.normalized * bulletForce, ForceMode2D.Impulse);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.damage = bulletDamage;
        SoundWave.SpawnFromPool("Sound Wave Gun", this.transform.position, Quaternion.identity);
        ammoInChamber--;

        StartCoroutine(ResetShootFlagAfterDelay());
    }

    IEnumerator ResetShootFlagAfterDelay()
    {
        yield return new WaitForSeconds(1.0f); // Wait for 1 second (you can adjust the time as needed)
        isshoot = false;
    }

    private void StartReload()
    {
        if (ammoInChamber < maxAmmo && currentAmmo > 0)
        {   
            gunSpeedManager.ReduceSpeedDuringReload();
            isReloading = true;
            reloadStartTime = Time.time;
            bulletsToReload = maxAmmo - ammoInChamber;
            // Decrease current ammo when starting a reload.
        }
    }

    private void FinishReload()
    {
        if (bulletsToReload > 0 && currentAmmo > 0)
        {
            ammoInChamber++;
            bulletsToReload--;

            // Decrease current ammo when finishing a reload.
            currentAmmo--;
            reloadStartTime = Time.time; // Start the reload of the next bullet.
        }
        else
        {
            isReloading = false;
            // Restore the player's normal speed when the reload is finished.
            gunSpeedManager.RestoreNormalSpeed();
        }
    }
}
