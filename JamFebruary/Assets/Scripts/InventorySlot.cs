using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    [SerializeField] private Image _displayImage;
    public bool IsOccupied { get; private set; }
    public InventoryItem Item { get; private set; }
    public void SetItem(InventoryItem item)
    {
        Item = item;
        _displayImage.enabled = true;
        _displayImage.sprite = item.ItemSprite;

        IsOccupied = true;
    }

    public void ClearSlot()
    {
        Item = null;
        _displayImage.enabled = false;
        _displayImage.sprite = null;

        IsOccupied = false;
    }

}
