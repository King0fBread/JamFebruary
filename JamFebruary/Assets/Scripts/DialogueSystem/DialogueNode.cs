using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Dialogue/Node")]
public class DialogueNode : ScriptableObject
{
    public string dialogueText;
    public List<DialogueResponse> responses;

    public bool IsLastNode()
    {
        return responses == null || responses.Count == 0;
    }
}