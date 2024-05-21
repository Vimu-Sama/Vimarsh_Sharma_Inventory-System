using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;


namespace Inventory.itemSlot
{
    public class ItemSlot : MonoBehaviour
    {
        private int tempVariable;

        #region Item Info
        public string itemName;
        private int slotMaxQuantity = 120;
        private int currentSlotQuantity = 0;
        #endregion


        #region Item Slot Visual References
        [SerializeField] private Image itemImagePlaceHolder;
        [SerializeField] private TextMeshProUGUI contentQuantityIndicator;
        #endregion


        #region item slot status
        private bool isItemSlotFilled = false;
        private bool isItemSlotEmpty = true;
        public bool IsItemSlotFilled { get { return isItemSlotFilled; } }
        public bool IsItemSlotEmpty { get {  return isItemSlotEmpty; } }
        #endregion  

        public int AddItems(string itemName, int itemContentQuantity, Sprite itemImage)
        {
            tempVariable = 0;
            this.itemName = itemName;
            contentQuantityIndicator.enabled = true;
            itemImagePlaceHolder.sprite = itemImage;
            itemImagePlaceHolder.enabled = true;
            currentSlotQuantity += itemContentQuantity;
            if(currentSlotQuantity>slotMaxQuantity)
            {
                tempVariable = currentSlotQuantity-slotMaxQuantity;
                currentSlotQuantity = slotMaxQuantity;
                isItemSlotFilled=true;
            }
            isItemSlotEmpty=false;
            contentQuantityIndicator.text = currentSlotQuantity.ToString();
            return tempVariable;
        }


        public bool DeleteItem(string deletingItemName, int deletionCount)
        {
            if(deletionCount<=currentSlotQuantity)
            {
                isItemSlotFilled = false;
                currentSlotQuantity -= deletionCount;
                contentQuantityIndicator.text= currentSlotQuantity.ToString();
                if(currentSlotQuantity==0)
                {
                    this.itemName = null;
                    contentQuantityIndicator.enabled = false;
                    itemImagePlaceHolder.sprite = null;
                    itemImagePlaceHolder.enabled = false;
                    isItemSlotEmpty = true;
                }
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
