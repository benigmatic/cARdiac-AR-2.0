using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Dialogue
{
    public string message;

    public Dialogue(string m)
    {
        message = m;
    }
}

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
    

    public Dialogue[] currentMessages = new Dialogue[12];
    int activeMessage = 0;

    void DisplayMessage() {
        Dialogue messageToDisplay = currentMessages[activeMessage];
        messageText.text = messageToDisplay.message;
    }

    public void NextMessage(){
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

    public void tutorialMessages(Dialogue[] currentMessages)
    {
        currentMessages[0] = new Dialogue("This tutorial will introduce you to the basics of the UCF cARdiac App.");
        currentMessages[1] = new Dialogue("In this app, you will be able to interact with a heart model by moving, rotating, and resizing it using your hands within the Microsoft HoloLens 2 view.");
        currentMessages[2] = new Dialogue("Let’s try moving an object.");
        currentMessages[3] = new Dialogue("To move an object, reach out and grab it. While holding it move your hand to drag the object.");
        currentMessages[4] = new Dialogue("Good job!");
        currentMessages[5] = new Dialogue("Now let’s try rotating an object. To rotate an object, reach out and grab it. While holding it twist your hand to turn the object.");
        currentMessages[6] = new Dialogue("Great!");
        currentMessages[7] = new Dialogue("Now, let’s try expanding an object. Grab the object with both hands and pinch the opposite ends of it. Now pinch and pull your hands away from each other.");
        currentMessages[8] = new Dialogue("You got it!");
        currentMessages[9] = new Dialogue("Finally, let’s try shrinking an object. Grab the object with both hands and pinch the opposite ends of it. Now pinch and push your hands towards each other.");
        currentMessages[10] = new Dialogue("Fantastic!");
        currentMessages[11] = new Dialogue("This concludes the tutorial! Launching application now…");
    }

    // Start is called before the first frame update
    void Start()
    {
        tutorialMessages(currentMessages);

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
                NextMessage();
            }
        }

        // Check for model rotation from user.
        if (activeMessage == 5)
        {
            currPos = GameObject.Find("HealthyHeart").transform.eulerAngles;

            if (Vector3.Angle(originalPos, currPos) > rotThreshold)
            {
                continueButton.SetActive(true);
                NextMessage();
            }
        }

        // Check for model being scaled up from user.
        if (activeMessage == 7)
        {
            currPos = GameObject.Find("HealthyHeart").transform.localScale;

            if (originalPos.x < currPos.x)
            {
                continueButton.SetActive(true);
                NextMessage();
            }
        }

        // Check for model being scaled down from user.
        if (activeMessage == 9)
        {
            currPos = GameObject.Find("HealthyHeart").transform.localScale;

            if (originalPos.x > currPos.x)
            {
                continueButton.SetActive(true);
                NextMessage();
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
