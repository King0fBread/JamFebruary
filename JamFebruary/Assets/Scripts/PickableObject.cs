using UnityEngine;
using System;

public class PickableObject : MonoBehaviour, IPickable
{
    public InventoryItem ItemData;

    public static event Action<InventoryItem, GameObject> OnItemPicked;

    public string GetPickupPrompt()
    {
        return $"Press E to pick up {ItemData.ItemName}";
    }

    public void PickUp()
    {
        OnItemPicked?.Invoke(ItemData, gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<ItemPickupSystem>()?.SetPickable(this);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<ItemPickupSystem>()?.ClearPickable(this);
        }
    }
}