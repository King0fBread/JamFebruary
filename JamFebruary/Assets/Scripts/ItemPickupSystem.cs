using TMPro;
using UnityEngine;

public class ItemPickupSystem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _pickupDisplayMessage;

    private IPickable _currentPickableItem;
    //private void OnEnable()
    //{
    //    if (MovementSystem.Instance != null)
    //        MovementSystem.Instance.OnInteractPressed += OnPick;
    //}

    //private void OnDisable()
    //{
    //    if (MovementSystem.Instance != null)
    //        MovementSystem.Instance.OnInteractPressed -= OnPick;
    //}

    public void OnPick()
    {
        Debug.Log("Pickup system received Interact Action");

        _currentPickableItem?.PickUp();
    }
    public void SetPickable(IPickable pickableItem)
    {
        _currentPickableItem = pickableItem;

        _pickupDisplayMessage.gameObject.SetActive(true);
        _pickupDisplayMessage.text = pickableItem.GetPickupPrompt();
    }
    public void ClearPickable(IPickable pickableItem)
    {
        if(_currentPickableItem == pickableItem)
        {
            _currentPickableItem = null;
        }

        _pickupDisplayMessage.gameObject.SetActive(false);
        _pickupDisplayMessage.text = "";
    }
}
