using Inventory.itemSlot;
using System.Security.Cryptography;
using UnityEngine;


namespace Inventory
{
    public class InventoryManager : MonoBehaviour
    {
        private int tempIntegerVariable = 0;
        private bool tempBoolVariable= false;

        #region Inventory Variables
        private bool isInventoryOn = false;
        [SerializeField] private GameObject InventoryUI;
        [SerializeField] private StackableItemSlot[] itemSlots;
        [SerializeField] private UniqueItemSlot[] uniqueItemSlots;
        [SerializeField] private Transform droppedItemSpawnPos;
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
            return tempBoolVariable;
        }
    }
}