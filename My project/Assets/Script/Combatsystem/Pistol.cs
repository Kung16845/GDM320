using UnityEngine;

public class Pistol : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce = 10f;
    [SerializeField] public int maxAmmo = 10;
    [SerializeField] public int ammoInChamber = 5;
    [SerializeField] float bulletDamage = 1.0f;
    [SerializeField] public float bulletSpeed = 10f;
    [SerializeField] float maxAccuracy = 1.0f;
    [SerializeField] float minAccuracy = 0.2f;
    [SerializeField] float cooldownTime = 0.5f;
    [SerializeField] float accuracyIncreaseRate = 0.2f;
    [SerializeField] float accuracyDecreaseRate = 0.1f;
    [SerializeField] float reloadTime = 2.0f;
    [SerializeField] GameObject accuracyCircle; // Reference to the accuracy circle GameObject.

    private float currentAccuracy = 0.2f;
    private Transform firePoint;
    private float lastShotTime;
    public bool isAiming;
    public bool isReloading;
    private float reloadStartTime;
    private int bulletsToReload;
    private PlayerMovement playerMovement;

    void Start()
    {
        firePoint = transform.Find("FirePoint");
        lastShotTime = -cooldownTime;
        isAiming = false;
        isReloading = false;
        bulletsToReload = 0;
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
    RotateTowardsMouse();

    if (Input.GetMouseButton(1))
    {
        accuracyCircle.SetActive(true);
        isAiming = true;
        currentAccuracy = Mathf.Lerp(currentAccuracy, maxAccuracy, accuracyIncreaseRate * Time.deltaTime);
    }
    else
    {
        isAiming = false;
        accuracyCircle.SetActive(false);
        currentAccuracy = Mathf.Lerp(currentAccuracy, minAccuracy, accuracyDecreaseRate * Time.deltaTime);
    }

    // Scale the accuracy circle based on the currentAccuracy.
    float scaledAccuracy = 0.05f / currentAccuracy;
    accuracyCircle.transform.localScale = new Vector3(scaledAccuracy, scaledAccuracy, 1f);

    if (isReloading)
    {
        playerMovement.ReduceSpeedDuringReload();
        if (Input.GetMouseButton(1)) // Check if right mouse button is pressed.
        {
            // Cancel the reload if right mouse button is pressed.
            isReloading = false;
            bulletsToReload = 0; // Reset the bullets to reload.
            
            // Restore the player's normal speed when the reload is canceled.
            playerMovement.RestoreNormalSpeed();
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
    else if (Input.GetKeyDown(KeyCode.R) && !isReloading && ammoInChamber < maxAmmo && !isAiming)
    {
        StartReload();
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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
    
        Vector2 bulletDirection = (Vector2)firePoint.up + Random.insideUnitCircle * (1 - currentAccuracy);
        rb.AddForce(bulletDirection.normalized * bulletForce, ForceMode2D.Impulse);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.damage = bulletDamage;

        ammoInChamber--;
    }

    void StartReload()
    {
        if (ammoInChamber < maxAmmo)
        {
            isReloading = true;
            reloadStartTime = Time.time;
            bulletsToReload = maxAmmo - ammoInChamber;
        }
    }

    void FinishReload()
    {
        if (bulletsToReload > 0)
    {
        ammoInChamber++;
        bulletsToReload--;
        reloadStartTime = Time.time; // Start the reload of the next bullet.
    }
    else
    {
        isReloading = false;
        // Restore the player's normal speed when the reload is finished.
        playerMovement.RestoreNormalSpeed();
    }
    }
}