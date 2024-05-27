using Inventory.itemSlot;
using System;
using UnityEngine;
using Items;
using TMPro;


namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {

        //actions for talking to other scripts, without sacrifing the flexibility
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


        private void Start()
        {
            DeSelectItems += DeselectAllItemSlots;
            UpdateBulletUI += UpdateBulletCount;
            UpdateBulletCount("none");
        }

        private void Update()
        {
            //To open Inventory, checks input and if the inventory is not already opened
            if (Input.GetKeyDown(KeyCode.Tab) && !InventoryUI.activeInHierarchy)
            {
                InventoryUI.SetActive(true);
                isInventoryOn = true;
                Cursor.lockState = CursorLockMode.None;
            }
            //To close Inventory, checks input and if the inventory is already opened
            else if (Input.GetKeyDown(KeyCode.Tab) && InventoryUI.activeInHierarchy)
            {
                InventoryUI.SetActive(false);
                isInventoryOn = false;
                DeselectAllItemSlots();
                Cursor.lockState = CursorLockMode.Locked;
            }
            //for dropping a selected item from the Inventory
            if(Input.GetKeyDown(KeyCode.G))
            {
                for (int i = 0; i < itemSlots.Length;  i++)
                {
                    //checks whether the itemSlot is selected or not and if it is not empty- for stackable item slots
                    if (itemSlots[i].itemSlotSelectionHighlight.activeInHierarchy && itemSlots[i].IsItemSlotEmpty==false)
                    {
                        temp = Instantiate(itemSlots[i].ItemPrefab, droppedItemSpawnPos);
                        temp.GetComponent<Item>().ItemQuantity =itemSlots[i].CurrentSlotQuantity; 
                        temp.transform.parent = null;
                        temp.transform.rotation = Quaternion.identity;
                        temp.transform.localScale = Vector3.one;
                        itemSlots[i].RemoveItem(itemSlots[i].name, itemSlots[i].CurrentSlotQuantity);
                        DeselectAllItemSlots();
                        break;
                    }
                }
                //same for the unique items,checking if slot is selected or not
                for(int j = 0; j < uniqueItemSlots.Length; j++)
                {
                    if (uniqueItemSlots[j].itemSlotSelectionHighlight.activeInHierarchy && uniqueItemSlots[j].IsItemSlotEmpty == false)
                    {
                        //code to drop item
                        temp = Instantiate(uniqueItemSlots[j].ItemPrefab, droppedItemSpawnPos);
                        temp.transform.parent = null;
                        temp.transform.rotation = Quaternion.identity;
                        temp.transform.localScale = droppedItemSpawnPos.localScale;
                        DroppedAllWeapons?.Invoke();
                        uniqueItemSlots[j].RemoveItem(uniqueItemSlots[j].name, uniqueItemSlots[j].CurrentSlotQuantity);
                        DeselectAllItemSlots();
                        break;
                    }
                }
                //updating bullet count by adding all the ammo from check with parameter passed below
                UpdateBulletCount(currentAmmoBeingUsed);
            }
            //restricts player movements when inventory is open
            PlayerMovement.RestrictPlayerMovementAndShooting(InventoryUI.activeInHierarchy);
        }

        public void UpdateBulletCount(string ammoName)
        {
            currentAmmoBeingUsed = ammoName;
            tempBulletCountVariable = 0;
            for(int i = 0; i < itemSlots.Length;i++)
            {
                if (itemSlots[i].ItemName==currentAmmoBeingUsed)
                {
                    tempBulletCountVariable += itemSlots[i].CurrentSlotQuantity;
                }
                bulletCount.text = tempBulletCountVariable.ToString();
            }
        }

        //deselects all item slots, when selected in inventory
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


        //Add item to Inventory
        public int AddItemToInventory(string itemName, int itemQuantity, Sprite itemImage, ItemType itemType, GameObject itemPrefab)
        {
            if (itemType == ItemType.collectible)
            {
                tempIntegerVariable = 0;
                for (int i = 0; i < itemSlots.Length; i++)
                {
                    //checks if item is present or not, if yes then the new item will be added onto the old one or it will assigned to new compartment
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

        //removing item from inventory
        public bool RemoveItemFromInventory(string deleteItemName, int itemQuantity)
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

        private void OnDestroy()
        {
            DeSelectItems -= DeselectAllItemSlots;
            UpdateBulletUI -= UpdateBulletCount;
        }
    }
}