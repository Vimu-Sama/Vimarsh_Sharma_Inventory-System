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
        [SerializeField] private string itemName;
        [SerializeField] private int itemQuantity;
        [SerializeField] private float rotateSpeed = 10;
        [SerializeField] private Sprite itemImage;
        [SerializeField] private ItemType itemType;
        [SerializeField] private GameObject itemPrefab;

        protected virtual void Start()
        {
            itemPrefab = this.gameObject;
            inventoryManager = FindObjectOfType<InventoryManager>().GetComponent<InventoryManager>();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (LayerMask.LayerToName(other.gameObject.layer) == "Player")
            {
                itemQuantity = inventoryManager.AddItemToInventory(itemName, itemQuantity, itemImage, itemType, itemPrefab);
                if (itemQuantity <= 0)
                    Destroy(this.gameObject, 2f);
            }
        }

        protected void FixedUpdate()
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.fixedDeltaTime);
        }

    }
}