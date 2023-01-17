using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public TMP_Text messageText;

    public string sceneName;

    public GameObject heartModel;

    public GameObject continueButton;

    private Vector3 originalPos;

    private Vector3 currPos;

    // Distance that will trigger movement prompt.
    private float distThreshold = 0.1f;

    // Angle that will trigger rotation prompt.
    private float rotThreshold = 10f;

    [SerializeField]
    private float delayBeforeLoading = 2f;
    private float timeElapsed = 0;
    

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
            continueButton.SetActive(false);

            // Cache the heart model current rotation.
            originalPos = GameObject.Find("HealthyHeart").transform.eulerAngles;
        }

        // Scale up prompt.
        if (activeMessage == 7)
        {
            continueButton.SetActive(false);

            // Cache the heart model current size.
            originalPos = GameObject.Find("HealthyHeart").transform.localScale;
        }

        // Scale down prompt
        if (activeMessage == 9)
        {
            continueButton.SetActive(false);
        }

        if (activeMessage == 11)
        {
            continueButton.SetActive(false);
        }

        

        if (activeMessage < currentMessages.Length) {
            DisplayMessage();
        } else {
            Debug.Log("Tutorial has ended! "+ currentMessages.Length);
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

            if (Vector3.Distance(originalPos, currPos) > distThreshold)
            {
                continueButton.SetActive(true);
                NextMessage(currentMessages);
            }
        }

        // Check for model rotation from user.
        if (activeMessage == 5)
        {
            currPos = GameObject.Find("HealthyHeart").transform.eulerAngles;

            if (Vector3.Angle(originalPos, currPos) > rotThreshold)
            {
                continueButton.SetActive(true);
                NextMessage(currentMessages);
            }
        }

        // Check for model being scaled up from user.
        if (activeMessage == 7)
        {
            currPos = GameObject.Find("HealthyHeart").transform.localScale;

            if (originalPos.x < currPos.x)
            {
                continueButton.SetActive(true);
                NextMessage(currentMessages);
            }
        }

        // Check for model being scaled down from user.
        if (activeMessage == 9)
        {
            currPos = GameObject.Find("HealthyHeart").transform.localScale;

            if (originalPos.x > currPos.x)
            {
                continueButton.SetActive(true);
                NextMessage(currentMessages);
            }
        }

        // On the last prompt load the next scene after time delay.
        if (activeMessage == 11)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > delayBeforeLoading)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
