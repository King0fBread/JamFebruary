using UnityEngine;

public class DialogueActor : MonoBehaviour, IInteractable
{
    [SerializeField] private InventorySystem _inventorySystem;
    [SerializeField] private DeliverySatisfactionCounters _deliverySatisfactionCounters;

    public string Name;

    [Header("Dialogue Variants")]
    public Dialogue FirstMeetingDialogue;
    public Dialogue HasItemDialogue;
    public Dialogue NoItemDialogue;

    [Header("Conditions")]
    public InventoryItem RequiredItem;

    //private bool _hasMet = false;
    private DialogueNode _currentNode;

    [SerializeField] private bool _isFinaleActor = false;
    [SerializeField] private GameObject _fadeObject;
    [SerializeField] private bool _isWarehouseActor = false;

    public enum ActorState
    {
        FirstMeeting,
        WaitingForItem,
        ItemDelivered,
        Finished
    }

    [SerializeField] private ActorState _state = ActorState.FirstMeeting;

    public void Interact()
    {
        Dialogue dialogueToPlay = GetDialogue();

        if (dialogueToPlay == null)
            return; // No dialogue at this phase

        if (_currentNode != null)
        {
            DialogueManager.Instance
                .StartDialogue(Name, _currentNode, this);
        }
        else
        {
            _currentNode = dialogueToPlay.RootNode;

            DialogueManager.Instance
                .StartDialogue(Name, _currentNode, this);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        Interact();
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (!col.CompareTag("Player")) return;

        if (DialogueManager.Instance.IsDialogueActive())
            DialogueManager.Instance.HideDialogue();
    }

    private Dialogue GetDialogue()
    {
        switch (_state)
        {
            case ActorState.FirstMeeting:
                return FirstMeetingDialogue;

            case ActorState.WaitingForItem:

                // Player has the correct item progress
                if (RequiredItem != null &&
                    _inventorySystem.HasItem(RequiredItem))
                {
                    _inventorySystem.RemoveItem(RequiredItem);

                    _state = ActorState.ItemDelivered;

                    _deliverySatisfactionCounters.IncreaseDeliveryCount();

                    return HasItemDialogue;
                }

                // Player has the wrong item or no item
                _deliverySatisfactionCounters.RegisterFailedDelivery();
                return NoItemDialogue;

            case ActorState.ItemDelivered:
            case ActorState.Finished:
                return null;
        }

        return null;
    }
    public void OnDialogueFinished()
    {
        _currentNode = null;

        if (_state == ActorState.FirstMeeting)
        {
            //final actor check
            if (_isFinaleActor)
            {
                if (_fadeObject != null)
                {
                    _fadeObject.SetActive(true);
                    SoundManager.instance.PlaySound(SoundManager.Sounds.PlayerSucceeded);
                }
            }

            //first actor check
            if (_isWarehouseActor)
            {
                _state = ActorState.Finished;

                this.enabled = false;
                return;
            }

            _state = ActorState.WaitingForItem;
        }
        else if (_state == ActorState.ItemDelivered)
        {
            _state = ActorState.Finished;
        }
    }

    public string GetInteractPrompt()
    {
        return $"Talk to {Name}";
    }

    public void SetProgress(DialogueNode node)
    {
        _currentNode = node;
    }

    public ActorState GetState()
    {
        return _state;
    }
}