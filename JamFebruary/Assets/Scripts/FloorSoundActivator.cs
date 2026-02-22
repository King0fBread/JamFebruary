using UnityEngine;

public class FloorSoundActivator : MonoBehaviour
{
    [SerializeField] private SoundManager.Sounds _floorSound;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(_floorSound);
        }
    }
}
