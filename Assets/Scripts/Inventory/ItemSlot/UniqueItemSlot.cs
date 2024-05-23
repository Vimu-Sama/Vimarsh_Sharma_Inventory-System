using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


namespace Inventory.itemSlot
{
    public enum ItemSlotType
    {
        weapon,
        bag,
        armor
    }
    public class UniqueItemSlot : ItemSlot
    {
        [SerializeField] private ItemSlotType typeOfSlot;


        public override int AddItem(string name, int quantity, Sprite image, ItemType itemType, GameObject itemModel)
        {
            if(itemType.ToString()!=typeOfSlot.ToString())
            {
                return quantity;
            }
            else
            {
                AssignItemToSlot(name, quantity, image, itemModel);
                return -1;
            }
        }

        public override bool RemoveItem(string name, int count)
        {
            throw new System.NotImplementedException();
        }
    }
}