using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Inventory;
using Unity.IO.LowLevel.Unsafe;

public enum ItemType
{
    weapon,
    armor, 
    baggage,
    collectible
}


namespace Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private InventoryManager inventoryManager;
        [SerializeField] protected ScriptableObjectItemScript itemSO;
        [SerializeField] private int itemQuantity;
        [SerializeField] private float rotateSpeed = 10;
        

        protected virtual void Start()
        {
            inventoryManager = FindObjectOfType<InventoryManager>().GetComponent<InventoryManager>();
        }

        protected virtual void OnTriggerStay(Collider other)
        {
            if (LayerMask.LayerToName(other.gameObject.layer) == "Player")
            {
                itemQuantity = inventoryManager.AddItemToInventory(itemSO.name, itemQuantity, itemSO.itemImage, itemSO.itemType, itemSO.itemPrefab);
                if (itemQuantity <= 0)
                    Destroy(this.gameObject);
            }
        }

        protected void FixedUpdate()
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.fixedDeltaTime);
        }

    }
}