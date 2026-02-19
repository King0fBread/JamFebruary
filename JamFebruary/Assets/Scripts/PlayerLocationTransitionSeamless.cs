using UnityEngine;

public class PlayerLocationTransitionSeamless : MonoBehaviour
{
    [SerializeField] private Transform _newPlayerLocation;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.transform.position = _newPlayerLocation.position;
        }
    }
}
