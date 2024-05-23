using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WeaponManagement;

namespace Items
{
    public class EquipableItems : Item
    {

        [SerializeField] private GameObject currentWeapon;
        [SerializeField] private GameObject currentAmmoUsed;
        [SerializeField] private GameObject currentBulletPrefab;

        [SerializeField] private WeaponManager weaponManager;

        protected override void Start()
        {
            base.Start();
            weaponManager=FindObjectOfType<WeaponManager>();
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (LayerMask.LayerToName(other.gameObject.layer) == "Player")
            {
                weaponManager.SetCurrentWeapon(currentWeapon, currentAmmoUsed, currentBulletPrefab);
                base.OnTriggerEnter(other);
            }
        }
    }
}