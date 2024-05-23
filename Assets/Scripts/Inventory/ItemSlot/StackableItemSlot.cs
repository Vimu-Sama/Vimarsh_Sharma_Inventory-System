using UnityEngine;
using TMPro;
using UnityEngine.UI;
using JetBrains.Annotations;


namespace Inventory.itemSlot
{
    public class StackableItemSlot : ItemSlot
    {
        private int tempVariable;
 

        public override int AddItem(string itemName, int itemContentQuantity, Sprite itemImage, ItemType itemType, GameObject itemModel)
        {
            tempVariable = 0;
            this.itemName = itemName;
            currentSlotQuantity += itemContentQuantity;
            if(currentSlotQuantity>slotMaxQuantity)
            {
                tempVariable = currentSlotQuantity-slotMaxQuantity;
                currentSlotQuantity = slotMaxQuantity;
                isItemSlotFilled=true;
            }
            isItemSlotEmpty=false;
            AssignItemToSlot(itemName, currentSlotQuantity, itemImage, itemModel);
            contentQuantityIndicator.text = currentSlotQuantity.ToString();
            return tempVariable;
        }


        public override bool RemoveItem(string deletingItemName, int deletionCount)
        {
            if(deletionCount<=currentSlotQuantity)
            {
                isItemSlotFilled = false;
                currentSlotQuantity -= deletionCount;
                contentQuantityIndicator.text= currentSlotQuantity.ToString();
                if(currentSlotQuantity==0)
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

    }
}
