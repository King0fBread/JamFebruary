using UnityEngine;

public class TruckItemsManager : MonoBehaviour
{
    [SerializeField] private TruckPickableItem[] _truckItems;
    [SerializeField] private InventorySystem _inventorySystem;
    [SerializeField] private GameObject _truckMainImage;

    private void Start()
    {
        foreach (var truckItem in _truckItems)
        {
            TruckPickableItem itemRef = truckItem;

            itemRef.GetComponent<UnityEngine.UI.Button>()
                   .onClick.AddListener(() => OnTruckItemClicked(itemRef));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _truckMainImage.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _truckMainImage.SetActive(false);
        }
    }

    private void OnTruckItemClicked(TruckPickableItem truckItem)
    {
        InventoryItem item = truckItem.GetItem();

        if (_inventorySystem.HasItem(item))
        {
            Debug.Log("Already have this item");
            return;
        }

        AddToInventory(item);

        truckItem.gameObject.SetActive(false);
    }

    private void AddToInventory(InventoryItem item)
    {
        foreach (var slotObj in _inventorySystem.GetComponentsInChildren<InventorySlot>())
        {
            if (!slotObj.IsOccupied)
            {
                slotObj.SetItem(item);
                return;
            }
        }

        Debug.Log("Inventory full");
    }

    public void ReturnAllItemsToTruck()
    {
        foreach (var truckItem in _truckItems)
        {
            InventoryItem item = truckItem.GetItem();

            if (_inventorySystem.HasItem(item))
            {
                _inventorySystem.RemoveItem(item);
                truckItem.gameObject.SetActive(true);
            }
        }
    }
}