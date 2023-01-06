using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text messageText;

    Message[] currentMessages;
    int activeMessage = 0;
    public static bool isActive = false;

    void DisplayMessage() {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;
    }

    public void NextMessage(Message[] messages){
        currentMessages = messages;
        isActive = true;
        activeMessage++;

        if (activeMessage < currentMessages.Length) {
            DisplayMessage();
        } else {
            Debug.Log("Conversation has ended! "+ currentMessages.Length);
            isActive = false;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
