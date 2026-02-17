//Dialogue Response
using UnityEngine.Events;

[System.Serializable]
public class DialogueResponse
{
    public string responseText;
    public DialogueNode nextNode;

    //public UnityEvent OnSelected;
}