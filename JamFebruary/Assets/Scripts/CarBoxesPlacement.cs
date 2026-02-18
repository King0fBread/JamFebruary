using UnityEngine;

public class CarBoxesPlacement : MonoBehaviour
{
    [SerializeField] private float _requiredAmountOfPlacements;
    [SerializeField] private InventoryItem _requiredBoxItem;
    [SerializeField] private InventorySystem _inventorySystem;

    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _positionToMove;

    private float _currentAmountOfPlacements;
    private void Awake()
    {
        _currentAmountOfPlacements = 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (_inventorySystem.HasItem(_requiredBoxItem))
            {
                _inventorySystem.RemoveAll();
                _currentAmountOfPlacements++;
                CheckPlacements();
            }
        }
    }
    private void CheckPlacements()
    {
        if(_currentAmountOfPlacements >= _requiredAmountOfPlacements)
        {
            _player.transform.position = _positionToMove.position;
        }
    }
}
