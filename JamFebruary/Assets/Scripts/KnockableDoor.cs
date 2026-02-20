using UnityEngine;

public class KnockableDoor : MonoBehaviour
{
    [SerializeField] private DialogueActor _actorToActivate;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            KnockingSystem.Instance.SetCurrentKnockalbe(this);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            KnockingSystem.Instance.ClearCurrentKnockable();
            ToggleResident(false);
        }
    }

    public void ToggleResident(bool state)
    {
        if(_actorToActivate != null)
        {
            _actorToActivate.gameObject.SetActive(state);
        }
    }
}
