using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Shotgun : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float bulletForce = 10f;
    [SerializeField] public int maxAmmo = 10;
    [SerializeField] public int ammoInChamber;
    [SerializeField] float bulletDamage = 1.0f;
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] public int pelletsPerShot = 5; // Number of bullets per shot
    [SerializeField] public float spreadAngle = 20f; // Spread angle for bullets
    [SerializeField] float maxAccuracy = 1.0f;
    [SerializeField] float minAccuracy = 0.2f;
    [SerializeField] float cooldownTime = 1.5f;
    [SerializeField] float accuracyIncreaseRate = 0.2f;
    [SerializeField] float accuracyDecreaseRate = 0.1f;
    [SerializeField] float reloadTime = 2.0f;
    [SerializeField] Image accuracyCircle; // Reference to the accuracy circle GameObject.
    public string bulletname;
    public bool isReloadCanceled = false;
    private SanityScaleController sanityScaleController;
    public float currentAccuracy = 0.2f;
    public Transform firePoint;
    private float lastShotTime;
    public bool isAiming;
    public bool isReloading;
    private float reloadStartTime;
    private int bulletsToReload;
    private GunSpeedManager gunSpeedManager;
    public ObjectPolling SoundWave;
    public OnOffLight onOffLight;
    public bool isshoot = false;
    private bool playaimsound = false;  
    public SoundManager soundManager;
    public TrapController trapController;
    public Canvas canvas;
    public InventoryPresentCharactor inventoryPresentCharactor;
    // New variables for ammo
    public int currentAmmo; // Current total ammo the player has.
    public bool enable;
    private bool canaim;

    void Awake()
    {   
        initializevariable();
    }
    void Start()
    {
        initializevariable();
    }

    void Update()
    {   
        currentAmmo = inventoryPresentCharactor.GetTotalItemCountByName("ShotgunAmmo");
        if(enable && !inventoryPresentCharactor.openInven)
        { 
            RotateTowardsMouse();
            if (Input.GetMouseButton(1) && !gunSpeedManager.isRunning && canaim)
            {   
                if(!playaimsound)
                {
                    soundManager.PlaySound("ReadyAim");
                    playaimsound = true;
                }
                createcrosshaircircle();
            }
            else if(Input.GetMouseButtonUp(1))
            {
                gunSpeedManager.RestoreNormalSpeed();
            }
            else
            {   
                removecrosshaircircle();
            }
            float scaledAccuracy = 1.0f /currentAccuracy;
            accuracyCircle.transform.localScale = new Vector3(scaledAccuracy, scaledAccuracy, 1f);

            if (isReloading)
            {
                gunSpeedManager.ReduceSpeedDuringReload();
                if (Input.GetMouseButton(1) || Input.GetKeyDown(KeyCode.LeftShift)) 
                {
                    StartCoroutine(WaitBeforeNextReload());
                    cancelreload();
                }
                else if (Time.time - reloadStartTime >= reloadTime)
                {
                    FinishReload();
                }
            }
            else if (Input.GetMouseButtonDown(0) && ammoInChamber > 0 && Time.time - lastShotTime >= cooldownTime && !gunSpeedManager.isRunning && !isReloadCanceled)
            {
                Shoot();
                lastShotTime = Time.time;
            }
            else if (Input.GetMouseButtonDown(0) && ammoInChamber == 0 && Time.time - lastShotTime >= cooldownTime && !gunSpeedManager.isRunning)
            {
                soundManager.PlaySound("Shotgundry");
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
    }

    void movemouseposition()
    {
        // Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // mousePosition.z = 0; // Ensure the circle is at the same depth as the player.
         // Get the mouse position in screen space
        Vector3 mousePosition = Input.mousePosition;
        // Convert the mouse position to Canvas space
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, mousePosition, canvas.worldCamera, out Vector2 canvasMousePosition);

        accuracyCircle.rectTransform.localPosition = canvasMousePosition;
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
        for (int i = 0; i < pelletsPerShot; i++)
        {
            createBulletWithSpread();
        }
        SoundWave.SpawnFromPool("Sound Wave Gun", this.transform.position, Quaternion.identity);
         ammoInChamber--;
        soundManager.PlaySound("Shotgunfire");
        StartCoroutine(ResetShootFlagAfterDelay());
    }

    IEnumerator ResetShootFlagAfterDelay()
    {
        yield return new WaitForSeconds(1.5f);
        isshoot = false;
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
    IEnumerator PlayPullmag()
    {
        yield return new WaitForSeconds(0.4f);
        soundManager.PlaySound("Openloader");
        yield return new WaitForSeconds(0.4f); 
        soundManager.PlaySound("Shelldrop");
    }
    IEnumerator Closeloader()
    {
        yield return new WaitForSeconds(0.8f);
        soundManager.PlaySound("Closeloader");
    }
    IEnumerator WaitBeforeNextReload()
    {
        isReloadCanceled = true;
        yield return new WaitForSeconds(3f); // Wait for 3 seconds
        isReloadCanceled = false; // Reset the reload canceled flag after waiting
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
                StartCoroutine(Closeloader());
            }
        }
        else
        {
            isReloading = false;
            gunSpeedManager.RestoreNormalSpeed();
        }
    }
    void createBulletWithSpread()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();

        // Calculate spread based on the spread angle
        float bulletSpread = Random.Range(-spreadAngle, spreadAngle);
        Vector2 bulletDirection = Quaternion.Euler(0, 0, bulletSpread) * firePoint.up;

        rb.AddForce(bulletDirection.normalized * bulletForce, ForceMode2D.Impulse);

        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.damage = bulletDamage;
    }
    void decreaseammowhenreload()
    {
        ammoInChamber++;
        bulletsToReload--;
        currentAmmo--;
        inventoryPresentCharactor.ManageReduceResource("ShotgunAmmo");
        reloadStartTime = Time.time; // Start the reload of the next bullet.
        StartCoroutine(reloadsound());
    }
    IEnumerator reloadsound()
    {
        soundManager.PlaySound("Shellload");
        yield return new WaitForSeconds(0.8f);
    }
    void initializevariable()
    {
        // firePoint = GameObject.FindGameObjectWithTag("FirePoint").transform;
        lastShotTime = -cooldownTime;
        isAiming = false;
        isReloading = false;
        bulletsToReload = 0;
        canaim = true;
        sanityScaleController = FindObjectOfType<SanityScaleController>();
        SoundWave =  FindObjectOfType<ObjectPolling >();
        onOffLight =  FindObjectOfType<OnOffLight>();
        gunSpeedManager = FindObjectOfType<GunSpeedManager>();
        trapController = FindObjectOfType<TrapController>();
        soundManager = FindObjectOfType<SoundManager>();
        inventoryPresentCharactor = FindObjectOfType<InventoryPresentCharactor>();
    }
    void createcrosshaircircle()
    {
        if(!trapController.stuck)
        {
            gunSpeedManager.ReduceSpeedDuringAimming();
        }
        accuracyCircle.gameObject.SetActive(true);
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
        if(!trapController.stuck)
        {
            playaimsound = false;
            isAiming = false;
            accuracyCircle.gameObject.SetActive(false);
            currentAccuracy = Mathf.Lerp(currentAccuracy, minAccuracy , accuracyDecreaseRate * Time.deltaTime);
        }
    }
}