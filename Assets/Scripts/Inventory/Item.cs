using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Inventory;


public class Item : MonoBehaviour
{
    [SerializeField] private InventoryManager inventoryManager;
    [SerializeField] private string itemName;
    [SerializeField] private int itemQuantity;
    [SerializeField] private float rotateSpeed= 10;
    [SerializeField] private Sprite itemImage;


    private void Start()
    {
        inventoryManager = FindObjectOfType<InventoryManager>().GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(LayerMask.LayerToName(other.gameObject.layer)=="Player")
        {
            itemQuantity= inventoryManager.AddItemToInventory(itemName, itemQuantity, itemImage);
            if(itemQuantity<=0)
                Destroy(this.gameObject);
        }
    }

    private void FixedUpdate()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.fixedDeltaTime);
    }


}
