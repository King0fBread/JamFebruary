using UnityEngine;
using System;

public class PickableObject : MonoBehaviour
{
    public InventoryItem ItemData;

    public static event Action<InventoryItem> OnItemPicked;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            OnItemPicked?.Invoke(ItemData);
            Destroy(gameObject);
        }
    }
}