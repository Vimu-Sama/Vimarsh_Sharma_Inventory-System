using Inventory;
using UnityEngine;
using WeaponManagement;

public class PlayerShooter : MonoBehaviour
{
    private bool disableFiring = false;
    private AudioSource audioSource;
    [SerializeField] private float range = 100f;
    [SerializeField] private float fireRate = 15f;
    [SerializeField] private Camera fpsCam;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject currentAmmoBeingUsed;
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private WeaponManager weaponManager;

    private float nextTimeToFire = 0f;


    private void Start()
    {
        PlayerMovement.RestrictPlayerMovementAndShooting += SetAbilityToFire;
        audioSource = GetComponent<AudioSource>();
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
        //this assigns the ammo from which the count needs to be deducted
        currentAmmoBeingUsed = weaponManager.CurrentAmmoUsed;
        //condition to check the availablity of the ammo of that particular type of bullet in inventory
        if(inventoryManager.RemoveItemFromInventory(currentAmmoBeingUsed.name, 1))
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

    private void OnDestroy()
    {
        PlayerMovement.RestrictPlayerMovementAndShooting -= SetAbilityToFire;
    }
}
