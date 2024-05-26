using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace Inventory.itemSlot
{
    public abstract class ItemSlot : MonoBehaviour, IPointerClickHandler
    {


        #region Item Info

        protected string itemName = "";
        protected int slotMaxQuantity = 120;
        protected int currentSlotQuantity = 0;
        protected bool isItemSlotFilled = false;
        protected bool isItemSlotEmpty = true;
        protected GameObject itemPrefab;
        public GameObject itemSlotSelectionHighlight;

        #endregion

        #region Item Slot Visual References
        [SerializeField] protected Image itemImagePlaceHolder;
        [SerializeField] protected TextMeshProUGUI contentQuantityIndicator;
        #endregion

        #region Getters Setters
        public string ItemName { get { return itemName; } }

        public bool IsItemSlotFilled { get { return isItemSlotFilled; } }
        public bool IsItemSlotEmpty { get { return isItemSlotEmpty; } }

        public int CurrentSlotQuantity { set { currentSlotQuantity= value; } get { return currentSlotQuantity; } }
        public GameObject ItemPrefab {  get { return itemPrefab; } }

        #endregion

        #region abstract functions
        public abstract int AddItem(string name, int quantity, Sprite image, ItemType itemType, GameObject itemModel);

        #endregion

        #region Common Functions

        protected void AssignItemToSlot(string name, int quantity, Sprite image, GameObject itemModel)
        {
            itemName = name;
            currentSlotQuantity = quantity;
            contentQuantityIndicator.text = currentSlotQuantity.ToString();
            contentQuantityIndicator.enabled = true;
            itemImagePlaceHolder.sprite = image;
            itemImagePlaceHolder.enabled = true;
            isItemSlotEmpty = false;
            itemPrefab= itemModel;
        }
        public virtual bool RemoveItem(string deletingItemName, int deletionCount)
        {
            if (deletionCount <= currentSlotQuantity)
            {
                isItemSlotFilled = false;
                currentSlotQuantity -= deletionCount;
                contentQuantityIndicator.text = currentSlotQuantity.ToString();
                if (currentSlotQuantity == 0)
                {
                    ResetSlot();
                }
                return true;
            }
            else
            {
                return false;
            }
        }


        public void ResetSlot()
        {
            itemName = "";
            contentQuantityIndicator.enabled = false;
            itemImagePlaceHolder.sprite = null;
            itemImagePlaceHolder.enabled = false;
            isItemSlotEmpty = true;
            isItemSlotFilled = false;
        }

        #endregion

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if(eventData.button== PointerEventData.InputButton.Left)
            {
                InventoryManager.DeSelectItems?.Invoke();
                itemSlotSelectionHighlight.gameObject.SetActive(true);
            }
        }

    }
}