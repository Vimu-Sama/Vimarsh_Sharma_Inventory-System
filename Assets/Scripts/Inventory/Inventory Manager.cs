using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Inventory.itemSlot;


namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        private int tempVariable = 0;

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
            tempVariable = 0;
            for (int i = 0; i < itemSlots.Length; i++)
            {
                if ((!itemSlots[i].IsItemSlotFilled && itemSlots[i].itemName == itemName) ||
                    itemSlots[i].IsItemSlotEmpty)
                {
                    tempVariable = itemSlots[i].AddItems(itemName, itemQuantity, itemImage);
                    if (tempVariable > 0)
                    {
                        itemQuantity = tempVariable;
                        continue;
                    }
                    else
                        break;
                }
            }
            return tempVariable;
        }


    }
}