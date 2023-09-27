using UnityEngine;

public class Pistol : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float bulletForce = 10f;
    public int maxAmmo = 10;
    public int ammoInChamber = 5;
    public float bulletDamage = 1.0f;
    public float bulletSpeed = 10f;
    public float maxAccuracy = 1.0f;
    public float minAccuracy = 0.2f;
    public float cooldownTime = 0.5f;
    public float accuracyIncreaseRate = 0.2f;
    public float accuracyDecreaseRate = 0.1f;
    public float reloadTime = 2.0f;
    public GameObject accuracyCircle; // Reference to the accuracy circle GameObject.

    private float currentAccuracy = 0.2f;
    private Transform firePoint;
    private float lastShotTime;
    private bool isHoldingRightMouse;
    private bool isReloading;
    private float reloadStartTime;
    private int bulletsToReload;
    
    void Start()
    {
        firePoint = transform.Find("FirePoint");
        lastShotTime = -cooldownTime;
        isHoldingRightMouse = false;
        isReloading = false;
        bulletsToReload = 0;
    }

    void Update()
    {
        RotateTowardsMouse();

        if (Input.GetMouseButton(1))
        {
            isHoldingRightMouse = true;
            currentAccuracy = Mathf.Lerp(currentAccuracy, maxAccuracy, accuracyIncreaseRate * Time.deltaTime);
        }
        else
        {
            isHoldingRightMouse = false;
            currentAccuracy = Mathf.Lerp(currentAccuracy, minAccuracy, accuracyDecreaseRate * Time.deltaTime);
        }

        // Scale the accuracy circle based on the currentAccuracy.
        float scaledAccuracy = 0.08f / currentAccuracy;
        accuracyCircle.transform.localScale = new Vector3(scaledAccuracy, scaledAccuracy, 1f);

         if (isReloading)
        {
            if (Input.GetMouseButton(1)) // Check if right mouse button is pressed.
        {
            // Cancel the reload if right mouse button is pressed.
            isReloading = false;
            bulletsToReload = 0; // Reset the bullets to reload.
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
        else if (Input.GetKeyDown(KeyCode.R) && !isReloading && ammoInChamber < maxAmmo && !isHoldingRightMouse)
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
        }
    }
}
