using Inventory;
using UnityEngine;
using TMPro;
using System;


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


        // This function helps in assigning the player, the type of current gun, it's respective ammo and bullet prefab
        public void SetCurrentWeaponProperties(GameObject weaponModel, GameObject ammo, GameObject bulletPrefab)
        {
            if (currentWeapon == null || (currentWeapon != null && Input.GetKey(KeyCode.F)))
            {
                //this updates the bullet count in HUD
                InventoryManager.UpdateBulletUI(ammo.name);
                currentWeapon = weaponModel;
                currentAmmoUsed = ammo;
                currentBulletPrefab = bulletPrefab;
                //destroying previous weapon on the player if there's any 
                Destroy(tempVariable);
                //assigning new weapon to the player, this makes for the visual, the gun player is holding
                tempVariable = Instantiate(currentWeapon, weaponSpawnLocation);
                tempVariable.transform.parent = this.transform;
            }
        }

        //when there's no weapon available or player just drops off the gun
        public void ResetWeapon()
        {
            tempVariable.transform.parent = this.transform;
            Destroy(tempVariable);
            currentWeapon = null;
            currentAmmoUsed= null;
            currentBulletPrefab = null;
        }


        private void OnDestroy()
        {
            InventoryManager.DroppedAllWeapons -= ResetWeapon;
        }
    }

}
