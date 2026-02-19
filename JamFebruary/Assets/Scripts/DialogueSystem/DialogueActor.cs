using UnityEngine;

public class DialogueActor : MonoBehaviour, IInteractable
{
    [SerializeField] private InventorySystem _inventorySystem;

    public string Name;

    [Header("Dialogue Variants")]
    public Dialogue FirstMeetingDialogue;
    public Dialogue HasItemDialogue;
    public Dialogue NoItemDialogue;

    [Header("Conditions")]
    public InventoryItem RequiredItem;

    private bool _hasMet = false;
    private DialogueNode _currentNode;

    private enum ActorState
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

                // Player has item → progress quest
                if (RequiredItem != null &&
                    _inventorySystem.HasItem(RequiredItem))
                {
                    _inventorySystem.RemoveItem(RequiredItem);

                    _state = ActorState.ItemDelivered;

                    return HasItemDialogue;
                }

                // Player came back empty → False Item dialogue
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
}