//Dialogue Response
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class DialogueResponse
{
    public string responseText;

    [SerializeReference]
    public DialogueNode nextNode;

    //public UnityEvent OnSelected;
}