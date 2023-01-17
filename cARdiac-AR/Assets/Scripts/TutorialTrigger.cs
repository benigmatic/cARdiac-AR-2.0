using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTrigger : MonoBehaviour
{
    public Message[] messages;

    public void StartDialogue(){
        FindObjectOfType<TutorialManager>().NextMessage(messages);
    }
}


[System.Serializable]
public class Message {
    public string message;
}

