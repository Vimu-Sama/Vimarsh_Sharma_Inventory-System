using Inventory;
using Items;
using UnityEngine;


namespace WeaponManagement
{
    public class WeaponManager : MonoBehaviour
    {
        private GameObject tempVariable;

        private GameObject currentWeapon;
        private GameObject currentAmmoUsed;
        private GameObject currentBulletPrefab;
        [SerializeField] private Transform weaponSpawnLocation;
        
        public void SetCurrentWeaponProperties(GameObject weaponModel, GameObject ammo, GameObject bulletPrefab)
        {
            currentWeapon = weaponModel;
            currentAmmoUsed= ammo;
            currentBulletPrefab = bulletPrefab;
            Destroy(tempVariable);
            tempVariable= Instantiate(currentWeapon, weaponSpawnLocation);
            tempVariable.transform.parent= this.transform;
        }

        public void ResetWeapon()
        {
            currentWeapon = null;
            currentAmmoUsed= null;
            currentBulletPrefab = null;
            tempVariable = Instantiate(currentWeapon, weaponSpawnLocation);
            tempVariable.transform.parent = this.transform;
        }


        public GameObject CurrentWeapon
        {
            get
            {
                return currentWeapon;
            }
        }

        public GameObject CurrentBulletPrefab
        {
            get
            {
                return currentBulletPrefab;
            }
        }

        public GameObject CurrentAmmoUsed
        {
            get
            {
                return currentAmmoUsed;
            }
        }

    }

}
