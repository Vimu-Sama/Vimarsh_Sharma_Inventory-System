using Inventory;
using Items;
using UnityEngine;
using TMPro;


namespace WeaponManagement
{
    public class WeaponManager : MonoBehaviour
    {
        private GameObject tempVariable;

        private GameObject currentWeapon;
        private GameObject currentAmmoUsed;
        private GameObject currentBulletPrefab;
        [SerializeField] private Transform weaponSpawnLocation;
        [SerializeField] private TextMeshProUGUI weaponSwapPrompt;


        #region getters
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


        public TextMeshProUGUI WeaponSwapPrompt
        {
            get
            {
                return weaponSwapPrompt;
            }
        }

        #endregion


        private void Start()
        {
            InventoryManager.DroppedAllWeapons += ResetWeapon;
        }

        public void SetCurrentWeaponProperties(GameObject weaponModel, GameObject ammo, GameObject bulletPrefab)
        {
            if (currentWeapon == null || (currentWeapon != null && Input.GetKey(KeyCode.F)))
            {
                currentWeapon = weaponModel;
                currentAmmoUsed = ammo;
                currentBulletPrefab = bulletPrefab;
                Destroy(tempVariable);
                tempVariable = Instantiate(currentWeapon, weaponSpawnLocation);
                tempVariable.transform.parent = this.transform;
            }
        }

        public void ResetWeapon()
        {
            tempVariable.transform.parent = this.transform;
            Destroy(tempVariable);
            currentWeapon = null;
            currentAmmoUsed= null;
            currentBulletPrefab = null;
        }

    }

}
