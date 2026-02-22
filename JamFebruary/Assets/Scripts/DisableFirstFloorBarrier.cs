using UnityEngine;

public class DisableFirstFloorBarrier : MonoBehaviour
{
    [SerializeField] private GameObject _barrier;
    [SerializeField] private bool _state;
    [SerializeField] private bool _shouldDisableBackgrounds;
    [SerializeField] private GameObject[] _backgroundTriggers;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _barrier.SetActive(_state);

            if (_shouldDisableBackgrounds)
            {
                foreach (var trigger in _backgroundTriggers)
                {
                    Destroy(trigger.gameObject);
                }
            }
            Destroy(gameObject);
        }
    }
}
