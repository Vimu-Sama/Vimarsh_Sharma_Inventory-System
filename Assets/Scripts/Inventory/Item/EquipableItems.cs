using UnityEngine;
using WeaponManagement;

namespace Items
{
    public class EquipableItems : Item
    {

        [SerializeField] private GameObject currentWeapon;
        [SerializeField] private GameObject currentAmmoUsed;
        [SerializeField] private GameObject currentBulletPrefab;
        [SerializeField] private float weaponSwapCoolDownTime= 1f;
        [SerializeField] private WeaponManager weaponManager;

        protected override void Start()
        {
            base.Start();
            weaponManager=FindObjectOfType<WeaponManager>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if((LayerMask.LayerToName(other.gameObject.layer) == "Player") && weaponManager.CurrentWeapon!=null)
            {
                weaponManager.WeaponSwapPrompt.enabled = true;
            }
        }



        protected override void OnTriggerStay(Collider other)
        {
            if (LayerMask.LayerToName(other.gameObject.layer) == "Player")
            {
                GameObject tempCheck = weaponManager.CurrentWeapon;
                if(itemSO.itemType==ItemType.weapon)
                    weaponManager.SetCurrentWeaponProperties(currentWeapon, currentAmmoUsed, currentBulletPrefab);
                else if (itemSO.itemType==ItemType.baggage)
                {
                    //funcionality to increase the inventory size
                }
                if((tempCheck!=null && Input.GetKey(KeyCode.F)) || (tempCheck == null))
                {
                    base.OnTriggerStay(other);
                    weaponManager.WeaponSwapPrompt.enabled = false;
                    //StartCoroutine(CanSwapWeapon());
                }
                    
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (LayerMask.LayerToName(other.gameObject.layer) == "Player")
                weaponManager.WeaponSwapPrompt.enabled=false;
        }
    }
}