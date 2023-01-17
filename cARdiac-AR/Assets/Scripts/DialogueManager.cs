using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TMP_Text messageText;

    public GameObject heartModel;

    public GameObject continueButton;

    private Vector3 originalPos;

    private Vector3 currPos;

    Message[] currentMessages;
    int activeMessage = 0;

    void DisplayMessage() {
        Message messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;
    }

    public void NextMessage(Message[] messages){
        currentMessages = messages;
        activeMessage++;

        // Movement prompt.
        if (activeMessage == 3)
        {
            heartModel.SetActive(true);

            continueButton.SetActive(false);

            // Cache the heart model current position.
            originalPos = GameObject.Find("HealthyHeart").transform.position;

            // currPos = GameObject.Find("HealthyHeart").transform.position;
            // modelScale = GameObject.Find("HealthyHeart").transform.localScale;
            // modelRot = GameObject.Find("HealthyHeart").transform.eulerAngles;
            // Debug.Log("Scale: " + modelScale.ToString("F4"));
            // Debug.Log("Rotation: " + modelRot.ToString("F4"));
            // Debug.Log("Original Position: " + originalPos.ToString("F4"));
            // Debug.Log("Position: " + currPos.ToString("F4"));
        }

        // Rotation prompt.
        if (activeMessage == 5)
        {
            // Cache the heart model current rotation.
            originalPos = GameObject.Find("HealthyHeart").transform.eulerAngles;
        }

        // Scale up prompt.
        if (activeMessage == 7)
        {
            // Cache the heart model current size.
            originalPos = GameObject.Find("HealthyHeart").transform.localScale;
        }

        // Scale down prompt
        if (activeMessage == 9)
        {
            
        }

        

        if (activeMessage < currentMessages.Length) {
            DisplayMessage();
        } else {
            Debug.Log("Conversation has ended! "+ currentMessages.Length);
            // Debug.Log(pos.ToString("F4"));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        heartModel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Check for model movement from user.
        if (activeMessage == 3)
        {
            currPos = GameObject.Find("HealthyHeart").transform.position;

            //Debug.Log(Vector3.Distance(originalPos, currPos));

            if (Vector3.Distance(originalPos, currPos) > .1)
            {
                continueButton.SetActive(true);
                NextMessage(currentMessages);
            }
        }

        // Check for model rotation from user.
        if (activeMessage == 5)
        {
            currPos = GameObject.Find("HealthyHeart").transform.eulerAngles;

            if (Vector3.Angle(originalPos, currPos) > 10)
            {
                NextMessage(currentMessages);
            }
        }

        // Check for model being scaled up from user.
        if (activeMessage == 7)
        {
            currPos = GameObject.Find("HealthyHeart").transform.localScale;

            if (originalPos.x < currPos.x)
            {
                NextMessage(currentMessages);
            }
        }

        // Check for model being scaled down from user.
        if (activeMessage == 9)
        {
            currPos = GameObject.Find("HealthyHeart").transform.localScale;

            if (originalPos.x > currPos.x)
            {
                NextMessage(currentMessages);
            }
        }
    }
}
