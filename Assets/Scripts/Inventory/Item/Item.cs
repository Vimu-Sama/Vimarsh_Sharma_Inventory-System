using System.Collections;
using UnityEngine;
using Inventory;

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
        

        public int ItemQuantity { get { return itemQuantity; } set { itemQuantity = value; } }

        //to avoid immediate pick up when object is dropped
        private void Awake()
        {
            gameObject.GetComponent<BoxCollider>().enabled = false;
            StartCoroutine(ActivateColliderAgain());
        }

        private IEnumerator ActivateColliderAgain()
        {
            yield return new WaitForSeconds(1f);
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }

        protected virtual void Start()
        {
            inventoryManager = FindObjectOfType<InventoryManager>().GetComponent<InventoryManager>();
        }

        protected virtual void OnTriggerStay(Collider other)
        {
            //adding item to player's inventory whenever the player is detected
            if (LayerMask.LayerToName(other.gameObject.layer) == "Player")
            {
                itemQuantity = inventoryManager.AddItemToInventory(itemSO.name, itemQuantity, itemSO.itemImage, itemSO.itemType, itemSO.itemPrefab);
                if (itemQuantity <= 0)
                    Destroy(this.gameObject);
            }
        }

        protected void FixedUpdate()
        {
            //for giving visual effects to the item
            transform.Rotate(Vector3.up, rotateSpeed * Time.fixedDeltaTime);
        }

    }
}