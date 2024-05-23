using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


namespace Inventory.itemSlot
{
    public abstract class ItemSlot : MonoBehaviour
    {
        #region Item Info
        protected string itemName = "";
        protected int slotMaxQuantity = 120;
        protected int currentSlotQuantity = 0;
        protected bool isItemSlotFilled = false;
        protected bool isItemSlotEmpty = true;
        protected GameObject itemPrefab;
        #endregion

        #region Item Slot Visual References
        [SerializeField] protected Image itemImagePlaceHolder;
        [SerializeField] protected TextMeshProUGUI contentQuantityIndicator;
        #endregion

        #region Getters
        public string ItemName { get { return itemName; } }

        public bool IsItemSlotFilled { get { return isItemSlotFilled; } }
        public bool IsItemSlotEmpty { get { return isItemSlotEmpty; } }

        public GameObject ItemPrefab {  get { return itemPrefab; } }

        #endregion

        #region abstract functions
        public abstract int AddItem(string name, int quantity, Sprite image, ItemType itemType, GameObject itemModel);
        public abstract bool RemoveItem(string name, int count);
        #endregion

        #region Common Functions

        private void Start()
        {
            itemPrefab = new GameObject();
        }
        protected void AssignItemToSlot(string name, int quantity, Sprite image, GameObject itemModel)
        {
            itemName = name;
            currentSlotQuantity = quantity;
            contentQuantityIndicator.text = currentSlotQuantity.ToString();
            contentQuantityIndicator.enabled = true;
            itemImagePlaceHolder.sprite = image;
            itemImagePlaceHolder.enabled = true;
            isItemSlotEmpty = false;
            itemPrefab = itemModel;
        }

        protected void ResetSlot()
        {
            itemName = "";
            contentQuantityIndicator.enabled = false;
            itemImagePlaceHolder.sprite = null;
            itemImagePlaceHolder.enabled = false;
            isItemSlotEmpty = true;
            isItemSlotFilled = false;
            itemPrefab = null;
        }


        #endregion
    }
}