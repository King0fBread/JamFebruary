using UnityEngine;
using UnityEngine.UI;

public class TruckPickableItem : MonoBehaviour
{
    [SerializeField] private InventoryItem _correspondingItem;

    private Image _truckItemImage;
    private Button _button;

    private void Awake()
    {
        _truckItemImage = GetComponent<Image>();
        _button = GetComponent<Button>();

        if (_correspondingItem != null)
            _truckItemImage.sprite = _correspondingItem.ItemSprite;
    }

    public InventoryItem GetItem()
    {
        return _correspondingItem;
    }

    public void SetInteractable(bool state)
    {
        _button.interactable = state;
    }
}