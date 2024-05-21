using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.itemSlot;


namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        private int tempIntegerVariable = 0;
        private bool tempBoolVariable= false;

        #region Inventory Variables
        private bool isInventoryOn = false;
        [SerializeField] private GameObject InventoryUI;
        [SerializeField] private ItemSlot[] itemSlots;
        #endregion


        private int fixedPrimaryGunSlot;
        private int fixedSecondaryGunSlot;
        private int armorFixedSlot;


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
                Cursor.lockState = CursorLockMode.Locked;
            }

        }


        public int AddItemToInventory(string itemName, int itemQuantity, Sprite itemImage)
        {
            tempIntegerVariable = 0;
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if ((!itemSlots[i].IsItemSlotFilled && itemSlots[i].itemName == itemName) ||
                    itemSlots[i].IsItemSlotEmpty)
                {
                    tempIntegerVariable = itemSlots[i].AddItems(itemName, itemQuantity, itemImage);
                    if (tempIntegerVariable > 0)
                    {
                        itemQuantity = tempIntegerVariable;
                        continue;
                    }
                    else
                        break;
                }
            }
            return tempIntegerVariable;
        }

        public bool DeleteItemFromInventory(string deleteItemName, int itemQuantity)
        {
            tempBoolVariable = false;
            for(int i = 0; i < itemSlots.Length;i++)
            {
                if (itemSlots[i].itemName== deleteItemName)
                {
                    tempBoolVariable= itemSlots[i].DeleteItem(deleteItemName, itemQuantity);
                    if (tempBoolVariable)
                        break;
                }
            }
            return tempBoolVariable;
        }
    }
}