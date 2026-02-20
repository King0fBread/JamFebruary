using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TruckPickableItem : MonoBehaviour, IPointerEnterHandler,
    IPointerExitHandler
{
    [SerializeField] private InventoryItem _correspondingItem;

    private Image _truckItemImage;
    private Button _button;

    private TextMeshProUGUI _hoverText;

    private void Awake()
    {
        _truckItemImage = GetComponent<Image>();
        _button = GetComponent<Button>();

        _hoverText = gameObject.GetComponentInParent<TextMeshProUGUI>();

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_hoverText != null && _correspondingItem != null)
        {
            _hoverText.text = _correspondingItem.ItemName;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (_hoverText != null)
        {
            _hoverText.text = "";
        }
    }
}