using Inventory;
using Unity.VisualScripting;
using UnityEngine;
using WeaponManagement;
using static UnityEngine.GraphicsBuffer;

public class PlayerShooter : MonoBehaviour
{
    private bool disableFiring = false;
    private AudioSource audioSource;
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public Camera fpsCam;
    public GameObject bulletPrefab;
    public GameObject currentAmmoBeingUsed;
    public Transform bulletSpawn;
    public float bulletSpeed = 20f;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private WeaponManager weaponManager;

    private float nextTimeToFire = 0f;


    private void Start()
    {
        PlayerMovement.RestrictPlayerMovementAndShooting += SetAbilityToFire;
        audioSource = GetComponent<AudioSource>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void SetAbilityToFire(bool var)
    {
        disableFiring = var;
    }

    void Update()
    {
        if (disableFiring)
            return;
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        if(Input.GetButtonUp("Fire1"))
        {
            audioSource.Stop();
        }
    }

    void Shoot()
    {
        if(weaponManager.CurrentWeapon==null)
        {
            return;
        }
        bulletPrefab = weaponManager.CurrentBulletPrefab;
        currentAmmoBeingUsed = weaponManager.CurrentAmmoUsed;
        if(inventoryManager.DeleteItemFromInventory(currentAmmoBeingUsed.name, 1))
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);
            if(!audioSource.isPlaying)
                audioSource.Play();
            Rigidbody rb = bullet.GetComponent<Rigidbody>();
            rb.AddForce(bulletSpawn.forward * bulletSpeed, ForceMode.Impulse);
        }
        else
        {
            audioSource.Stop();
        }
    }
}
