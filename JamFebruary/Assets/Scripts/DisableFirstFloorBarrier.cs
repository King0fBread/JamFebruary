using UnityEngine;

public class DisableFirstFloorBarrier : MonoBehaviour
{
    [SerializeField] private GameObject _barrier;
    [SerializeField] private bool _state;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _barrier.SetActive(_state);
            Destroy(gameObject);
        }
    }
}
