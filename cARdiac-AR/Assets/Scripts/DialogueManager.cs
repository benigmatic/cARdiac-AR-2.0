using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text messageText;

    public GameObject hiddenObject;

    Message[] currentMessages;
    int activeMessage = 0;

    void DisplayMessage() {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;
    }

    public void NextMessage(Message[] messages){
        currentMessages = messages;
        activeMessage++;

        // If the tutorial is past the third prompt reveal the object.
        if (activeMessage > 2)
        {
            hiddenObject.SetActive(true);
        }

        if (activeMessage < currentMessages.Length) {
            DisplayMessage();
        } else {
            Debug.Log("Conversation has ended! "+ currentMessages.Length);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        hiddenObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
