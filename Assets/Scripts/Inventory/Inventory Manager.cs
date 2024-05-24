using Inventory.itemSlot;
using System;
using UnityEngine;
using Items;
using WeaponManagement;
using TMPro;


namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        public static Action DeSelectItems;
        public static Action DroppedAllWeapons;
        public static Action<string> UpdateBulletUI;

        private GameObject temp;
        private string currentAmmoBeingUsed;
        private int tempBulletCountVariable=0;
        private int tempIntegerVariable = 0;
        private bool tempBoolVariable= false;

        #region Inventory Variables
        private bool isInventoryOn = false;
        [SerializeField] private GameObject InventoryUI;
        [SerializeField] private StackableItemSlot[] itemSlots;
        [SerializeField] private UniqueItemSlot[] uniqueItemSlots;
        [SerializeField] private Transform droppedItemSpawnPos;
        [SerializeField] private TextMeshProUGUI bulletCount;
        #endregion


        private int fixedPrimaryGunSlot;
        private int fixedSecondaryGunSlot;
        private int armorFixedSlot;

        private void Start()
        {
            DeSelectItems += DeselectAllItemSlots;
            UpdateBulletUI += UpdateBulletCount;
            UpdateBulletCount("none");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab) && !InventoryUI.activeInHierarchy)
            {
                InventoryUI.SetActive(true);
                isInventoryOn = true;
                Cursor.lockState = CursorLockMode.None;
            }

            else if (Input.GetKeyDown(KeyCode.Tab) && InventoryUI.activeInHierarchy)
            {
                InventoryUI.SetActive(false);
                isInventoryOn = false;
                DeselectAllItemSlots();
                Cursor.lockState = CursorLockMode.Locked;
            }
            if(Input.GetKeyDown(KeyCode.G))
            {
                for (int i = 0, j = 0; i < itemSlots.Length && j < uniqueItemSlots.Length; i++, j++)
                {
                    if (itemSlots[i].itemSlotSelectionHighlight.activeInHierarchy && itemSlots[i].IsItemSlotEmpty==false)
                    {
                        temp = Instantiate(itemSlots[i].ItemPrefab, droppedItemSpawnPos);
                        temp.GetComponent<Item>().ItemQuantity =itemSlots[i].CurrentSlotQuantity; 
                        temp.transform.parent = null;
                        temp.transform.localScale = Vector3.one; 
                        itemSlots[i].RemoveItem(itemSlots[i].name, itemSlots[i].CurrentSlotQuantity);
                        DeselectAllItemSlots();
                        break;
                    }
                    else if (uniqueItemSlots[j].itemSlotSelectionHighlight.activeInHierarchy && uniqueItemSlots[i].IsItemSlotEmpty==false)
                    {
                        //code to drop item
                        temp = Instantiate(uniqueItemSlots[i].ItemPrefab, droppedItemSpawnPos);
                        temp.transform.parent = null;
                        temp.transform.localScale = droppedItemSpawnPos.localScale;
                        DroppedAllWeapons?.Invoke();
                        uniqueItemSlots[i].RemoveItem(uniqueItemSlots[i].name, uniqueItemSlots[i].CurrentSlotQuantity);
                        DeselectAllItemSlots();
                        break;
                    }
                }
                UpdateBulletCount(currentAmmoBeingUsed);
            }
            PlayerMovement.RestrictPlayerMovementAndShooting(InventoryUI.activeInHierarchy);
        }

        public void UpdateBulletCount(string ammoName)
        {
            currentAmmoBeingUsed = ammoName;
            Debug.Log("Current ammo-> "+ currentAmmoBeingUsed);
            tempBulletCountVariable = 0;
            for(int i = 0; i < itemSlots.Length;i++)
            {
                Debug.Log("item slot ammo->" + itemSlots[i].ItemName);
                if (itemSlots[i].ItemName==currentAmmoBeingUsed)
                {
                    tempBulletCountVariable += itemSlots[i].CurrentSlotQuantity;
                }
                bulletCount.text = tempBulletCountVariable.ToString();
            }
        }

        public void DeselectAllItemSlots()
        {
            for(int i=0;i<uniqueItemSlots.Length;i++)
            {
                uniqueItemSlots[i].itemSlotSelectionHighlight.SetActive(false);
            }
            for(int i=0;i<itemSlots.Length;i++)
            {
                itemSlots[i].itemSlotSelectionHighlight.SetActive(false);
            }
        }



        public int AddItemToInventory(string itemName, int itemQuantity, Sprite itemImage, ItemType itemType, GameObject itemPrefab)
        {
            if (itemType == ItemType.collectible)
            {
                tempIntegerVariable = 0;
                for (int i = 0; i < itemSlots.Length; i++)
                {
                    if ((!itemSlots[i].IsItemSlotFilled && itemSlots[i].ItemName == itemName) ||
                        itemSlots[i].IsItemSlotEmpty)
                    {
                        tempIntegerVariable = itemSlots[i].AddItem(itemName, itemQuantity, itemImage, itemType, itemPrefab);
                        if (tempIntegerVariable > 0)
                        {
                            itemQuantity = tempIntegerVariable;
                            continue;
                        }
                        else
                            break;
                    }
                }
            }
            else
            {
                for(int i=0;i<uniqueItemSlots.Length;i++)
                {
                    if (uniqueItemSlots[i].IsItemSlotEmpty == false)
                    {
                        GameObject temp =Instantiate(uniqueItemSlots[i].ItemPrefab, droppedItemSpawnPos);
                        temp.transform.localScale = Vector3.one;
                        temp.transform.parent = null;
                    }
                    tempIntegerVariable= uniqueItemSlots[i].AddItem(itemName, itemQuantity, itemImage, itemType, itemPrefab);
                    if (tempIntegerVariable <= 0)
                        break;
                }
            }
            UpdateBulletCount(currentAmmoBeingUsed);
            return tempIntegerVariable;
        }

        public bool DeleteItemFromInventory(string deleteItemName, int itemQuantity)
        {
            tempBoolVariable = false;
            for(int i = 0; i < itemSlots.Length;i++)
            {
                if (itemSlots[i].ItemName== deleteItemName)
                {
                    tempBoolVariable= itemSlots[i].RemoveItem(deleteItemName, itemQuantity);
                    if (tempBoolVariable)
                        break;
                }
            }
            UpdateBulletCount(currentAmmoBeingUsed);
            return tempBoolVariable;
        }
    }
}