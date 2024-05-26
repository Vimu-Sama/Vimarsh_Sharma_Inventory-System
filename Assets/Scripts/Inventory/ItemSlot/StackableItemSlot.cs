using UnityEngine;
using UnityEngine.EventSystems;


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


        public override void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left)
            {
                if (itemSlotSelectionHighlight.gameObject.activeInHierarchy == true)
                {
                    if (itemName == "MedKit")
                    {
                        PlayerMovement.AddHealth?.Invoke(10);
                        RemoveItem(itemName, 1);
                    }
                }
                else
                {
                    InventoryManager.DeSelectItems?.Invoke();
                    itemSlotSelectionHighlight.gameObject.SetActive(true);
                }
            }
        }

    }
}
