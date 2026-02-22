using System.Collections;
using UnityEngine;

public class CarBoxesPlacement : MonoBehaviour
{
    [SerializeField] private float _requiredAmountOfPlacements;
    [SerializeField] private InventoryItem _requiredBoxItem;
    [SerializeField] private InventorySystem _inventorySystem;

    [SerializeField] private GameObject _player;
    [SerializeField] private Transform _positionToMove;

    [SerializeField] private GameObject _truckBlackScreen;

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
                StartCoroutine(CheckPlacements());
            }
        }
    }
    private IEnumerator CheckPlacements()
    {
        if(_currentAmountOfPlacements >= _requiredAmountOfPlacements)
        {
            _truckBlackScreen.SetActive(true);
            SoundManager.instance.PlaySound(SoundManager.Sounds.EnvironmentCar);
            yield return new WaitForSeconds(1f);
            _player.transform.position = _positionToMove.position;
        }
    }
}
