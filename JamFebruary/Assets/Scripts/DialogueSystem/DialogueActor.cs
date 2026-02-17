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

    public void Interact()
    {
        Dialogue dialogueToPlay = GetDialogue();

        // Resume if exists
        if (_currentNode != null)
        {
            DialogueManager.Instance.StartDialogue(Name, _currentNode, this);
        }
        else
        {
            _currentNode = dialogueToPlay.RootNode;
            DialogueManager.Instance.StartDialogue(Name, _currentNode, this);
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
        // First ever interaction
        if (!_hasMet)
        {
            _hasMet = true;
            return FirstMeetingDialogue;
        }

        // Later interactions
        if (RequiredItem != null &&
            _inventorySystem.HasItem(RequiredItem))
        {
            return HasItemDialogue;
        }

        return NoItemDialogue;
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