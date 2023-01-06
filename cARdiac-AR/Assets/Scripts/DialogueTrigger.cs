using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Message[] messages;

    public void StartDialogue(){
        FindObjectOfType<DialogueManager>().NextMessage(messages);
    }
}


[System.Serializable]
public class Message {
    public string message;
}

