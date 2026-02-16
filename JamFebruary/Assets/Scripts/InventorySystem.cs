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
    private void HandleItemPicked(InventoryItem item)
    {
        AddItemToInventory(item);
    }
    private void AddItemToInventory(InventoryItem item)
    {
        foreach (var slotObject in _inventorySlots)
        {
            InventorySlot slot = slotObject.GetComponent<InventorySlot>();
            if(slot.IsOccupied == false)
            {
                slot.SetItem(item);
                return;
            }
        }
        print("inventory full bro");
    }
}
