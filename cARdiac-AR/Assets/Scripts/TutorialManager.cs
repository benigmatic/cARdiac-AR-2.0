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

    // This will be a game object we will base the default heart position off of.
    public GameObject resetAnchor;

    public GameObject continueButton;

    public GameObject slider;

    private Vector3 originalPos;

    private Vector3 currPos;

    // Distance that will trigger movement prompt.
    private float distThreshold = 0.115f;

    // Size that will trigger scale prompt.
    private float scaleThreshold = 0.02f;

    // Angle that will trigger rotation prompt.
    private float rotThreshold = 35f;

    [SerializeField]
    private float delayBeforeLoading = 2f;
    private float timeElapsed = 0;
    

    public Dialogue[] currentMessages = new Dialogue[14];
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

            resetHeartPosition();

            // Cache the heart model current position.
            originalPos = GameObject.Find("HealthyHeart").transform.position;

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
            heartModel.SetActive(false);

            slider.SetActive(true);

            continueButton.SetActive(false);
        }

        if (activeMessage == 13)
        {
            slider.SetActive(false);

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
        currentMessages[5] = new Dialogue("Next let’s try rotating an object. To rotate an object, reach out and grab it. While holding it twist your hand to turn the object.");
        currentMessages[6] = new Dialogue("Great!");
        currentMessages[7] = new Dialogue("Now, let’s try expanding an object. Grab the object with both hands and pinch the opposite ends of it. Now pinch and pull your hands away from each other.");
        currentMessages[8] = new Dialogue("You got it!");
        currentMessages[9] = new Dialogue("Let’s try shrinking an object now. Grab the object with both hands and pinch the opposite ends of it. Now pinch and push your hands towards each other.");
        currentMessages[10] = new Dialogue("Fantastic!");
        currentMessages[11] = new Dialogue("Finally, let’s try moving the slider. Reach out and pinch the thumb and drag it to one of the ends of the slider.");
        currentMessages[12] = new Dialogue("Amazing!");
        currentMessages[13] = new Dialogue("This concludes the tutorial! Launching application now…");
    }

    public void resetHeartPosition()
    {
        heartModel.transform.position = resetAnchor.transform.position + new Vector3(0, 0, 0);
        heartModel.transform.eulerAngles = resetAnchor.transform.eulerAngles;
        heartModel.transform.localScale =  new Vector3(.1f, .1f, .1f);
    }

    // Start is called before the first frame update
    void Start()
    {
        tutorialMessages(currentMessages);

        heartModel.SetActive(false);

        slider.SetActive(false);
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

            if ((originalPos.x + scaleThreshold) < currPos.x)
            {
                continueButton.SetActive(true);
                NextMessage();
            }
        }

        // Check for model being scaled down from user.
        if (activeMessage == 9)
        {
            currPos = GameObject.Find("HealthyHeart").transform.localScale;

            if ((originalPos.x - scaleThreshold) > currPos.x)
            {
                continueButton.SetActive(true);
                NextMessage();
            }
        }

        // Check slider movement from user.
        if (activeMessage == 11)
        {
            if (FindObjectOfType<SliderController>().HasSliderChanged())
            {
                continueButton.SetActive(true);
                NextMessage();
            }
        }

        // On the last prompt load the next scene after time delay.
        if (activeMessage == 13)
        {
            timeElapsed += Time.deltaTime;

            if (timeElapsed > delayBeforeLoading)
            {
                SceneManager.LoadScene(sceneName);
            }
        }
    }
}
