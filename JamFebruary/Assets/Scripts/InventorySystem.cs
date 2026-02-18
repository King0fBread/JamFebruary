using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    [SerializeField] private GameObject[] _inventorySlots;
    private void OnEnable()
    {
        PickableObject.OnItemPicked += HandleItemPicked;
    }
    private void OnDisable()
    {
        PickableObject.OnItemPicked -= HandleItemPicked;
    }
    private void HandleItemPicked(InventoryItem item, GameObject physicalObject)
    {
        AddItemToInventory(item, physicalObject);
    }
    private void AddItemToInventory(InventoryItem item, GameObject physicalObject)
    {
        foreach (var slotObject in _inventorySlots)
        {
            InventorySlot slot = slotObject.GetComponent<InventorySlot>();
            if(slot.IsOccupied == false)
            {
                slot.SetItem(item);
                Destroy(physicalObject);
                return;
            }
        }
        print("inventory full bro");
    }
    public bool HasItem(InventoryItem itemToCheck)
    {
        foreach (var slotObject in _inventorySlots)
        {
            InventorySlot slot = slotObject.GetComponent<InventorySlot>();
            if(slot.IsOccupied == true && slot.Item == itemToCheck)
            {
                return true;
            }
        }
        return false;
    }
    public void RemoveItem(InventoryItem itemToRemove)
    {
        foreach(var slotObject in _inventorySlots)
        {
            InventorySlot slot = slotObject.GetComponent<InventorySlot>();
            if(slot.IsOccupied == true && slot.Item == itemToRemove)
            {
                slot.ClearSlot();
                return;
            }
        }
    }
    public void RemoveAll()
    {
        foreach (var slotObject in _inventorySlots)
        {
            InventorySlot slot = slotObject.GetComponent<InventorySlot>();
            if (slot.IsOccupied == true)
            {
                slot.ClearSlot();
            }
        }
    }
}
