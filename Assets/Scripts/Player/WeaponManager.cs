using Inventory;
using UnityEngine;


namespace WeaponManagement
{
    public class WeaponManager : MonoBehaviour
    {
        private GameObject currentWeapon;
        private GameObject currentAmmoUsed;
        private GameObject currentBulletPrefab;
        [SerializeField] private Transform weaponSpawnLocation;
        
        public void SetCurrentWeapon(GameObject weaponModel, GameObject ammo, GameObject bulletPrefab)
        {
            currentWeapon = weaponModel;
            currentAmmoUsed= ammo;
            currentBulletPrefab = bulletPrefab;
            GameObject spawnedGun= Instantiate(currentWeapon, weaponSpawnLocation);
            spawnedGun.transform.parent= this.transform;
        }

        public GameObject CurrentBulletPrefab
        {
            get
            {
                return currentBulletPrefab;
            }
        }

    }

}
