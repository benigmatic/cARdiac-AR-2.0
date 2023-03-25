using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
using Microsoft.MixedReality.Toolkit.UI;
using System.Diagnostics;

using Debug = UnityEngine.Debug;

public class Question
{
    public int FID, grade, confidence;
    public string Prompt, Answer, time;

    public Question(string q, string a)
    {
        Prompt = q;
        Answer = a;
    }

    public static Question CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Question>(jsonString);
    }
}

public class FlashcardManager : MonoBehaviour
{
    private int numberofM1Questions = 0;
    private int numberofM2Questions = 0;
    public RectTransform r;     // Holds Flashcard object scale
    public TMP_Text cardText;
    public TMP_Text cardCounter;
    public GameObject flipButton;
    public GameObject correctButton;
    public GameObject incorrectButton;
    public GameObject DataObj;
    public Question[] ques;
    public DataManager savedData;
    List<Dictionary<string, object>> data;



    private float flipTime = 0.5f;
    private int faceSide = 0;   // 0 is the front of the flashcard, 1 is the back of it
    private int isShrinking = -1;   // -1 means get smaller, 1 means get bigger
    private bool isFlipping = false;

    // Starting flashcard;
    private int cardNum = 0;
    private float distancePerTime;
    private float timeCount = 0;
    Stopwatch watch = new Stopwatch();
    TimeSpan time;


    // Start is called before the first frame update
    void Start()
    {
        data = CSVReader.Read("cardiac_flashcards");
        correctButton.SetActive(false);
        incorrectButton.SetActive(false);
        distancePerTime = r.localScale.x / flipTime;
        DataObj = GameObject.FindWithTag("Data");
        savedData = DataObj.GetComponent<DataManager>();

        int i = 0;
        while ((int)data[i]["Section"] == 1)
        {
            numberofM1Questions++;
            i++;
        }

        numberofM2Questions = data.Count - numberofM1Questions;





        // Gets flashcard questions and answers from database
        StartCoroutine(GetRequest(ques));
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlipping)
        {
            Vector3 v = r.localScale;
            v.x += isShrinking * distancePerTime * Time.deltaTime;
            r.localScale = v;

            timeCount += Time.deltaTime;

            if ((timeCount >= flipTime) && (isShrinking < 0))
            {
                // Make the card grow.
                isShrinking = 1;
                timeCount = 0;

                if (faceSide == 0)
                {
                    // Front of the card to the back.
                    faceSide = 1;
                    cardText.text = ques[cardNum].Answer;
                }
                else
                {
                    // Back of the card to front.
                    faceSide = 0;
                    cardText.text = ques[cardNum].Prompt;
                }
            }
            else if ((timeCount >= flipTime) && (isShrinking == 1))
            {
                isFlipping = false;
            }
        }
    }

    public void NextCard(int grade)
    {
        watch.Stop();
        time = watch.Elapsed;
        ques[cardNum].time = time.Hours.ToString() + ":" + time.Minutes.ToString() + ":" + time.Seconds.ToString();
        Debug.Log(ques[cardNum].time);

        ques[cardNum].grade = grade;
        correctButton.SetActive(false);
        incorrectButton.SetActive(false);
        
        // Resets Slider position.
        FindObjectOfType<SliderController>().ResetSliderValue();

        // Uploads data to database for each card
        StartCoroutine(Upload());

        faceSide = 0;
        cardNum++;
        if (cardNum >= ques.Length)
        {
            cardNum = 0;
        }

        ques[cardNum].confidence = 1; // Sets default confidence value
        cardText.text = ques[cardNum].Prompt;
        cardCounter.text = (cardNum + 1).ToString() + " / " + ques.Length;
        watch = new Stopwatch(); // Creates new stopwatch so time does not add together
        watch.Start();
    }

    public void getConfidence(int confidence)
    {
        ques[cardNum].confidence = confidence;
    }

    public void FlipCard()
    {
        correctButton.SetActive(true);
        incorrectButton.SetActive(true);
        timeCount = 0;
        isFlipping = true;
        isShrinking = -1;
    }

    public void m1Cards(Question[] cards)
    {
        // Load from csv

        for (int i = 0; i < numberofM1Questions; i++)
        {
            cards[i] = new Question((string)data[i]["Prompt"], (string)data[i]["Answer"]);
        }
   
    }

    public void m2Cards(Question[] cards)
    {
        for (int i = 0; i < numberofM2Questions; i++)
        {
            cards[i] = new Question((string)data[i + numberofM1Questions]["Prompt"], (string)data[i + numberofM1Questions]["Answer"]);
        }
    }

    IEnumerator GetRequest(Question[] cards)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        int val = (string.Compare(sceneName, "M1HeartScene") == 0) ? 1 : 2;
        string uri = "https://hemo-cardiac.azurewebsites.net//flashcards.php?var1=" + val;

        Debug.Log("Checking request for " + uri);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            // If webRequest fails or bad uri gets cardoded flascard data, else gets flashcard data from database
            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.downloadHandler.text == "No Flashcards found")
            {
                Debug.Log("Couldn't connect to website when retrieving data");
                Debug.Log("WebRequest text: " + webRequest.downloadHandler.text);
                Debug.Log("WebRequest result: " + webRequest.result);
                Debug.Log("WebRequest error: " + webRequest.error);

                if (string.Compare(sceneName, "M1HeartScene") == 0)
                {
                    ques = new Question[numberofM1Questions];
                    m1Cards(ques);
                }
                else
                {
                    ques = new Question[numberofM2Questions];
                    m2Cards(ques);
                }
            }
            else
            {
                Debug.Log("WebRequest text: " + webRequest.downloadHandler.text);

                // Skips to appropiate JSON data, excludes {"Flashcards":[
                string flashCardPrompts = webRequest.downloadHandler.text.Substring(16);
                string[] promptsArray = flashCardPrompts.Split('{');
                ques = new Question[promptsArray.Length];

                // Formats json to be {"Prompt"...}, adds deleted '{' and removes trailing ','
                for (int i = 0; i < promptsArray.Length; i++)
                {
                    promptsArray[i] = promptsArray[i].Insert(0, "{");
                    promptsArray[i] = promptsArray[i].Remove(promptsArray[i].Length - 1);

                    // Removes ']' from the end of the last Prompt Answer pair
                    if (i == promptsArray.Length - 1)
                        promptsArray[i] = promptsArray[i].Remove(promptsArray[i].Length - 1);

                    ques[i] = Question.CreateFromJSON(promptsArray[i]);
                }
            }

            ques[cardNum].confidence = 1; // Sets default confidence value
            cardText.text = ques[cardNum].Prompt;
            cardCounter.text = 1 + " / " + ques.Length;
            watch.Start();  // Starts the timer
        }
    }

    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        Debug.Log("SID: " + savedData.data.SID + " LoggedIn: " + savedData.data.LoggedIn);
        form.AddField("FID", ques[cardNum].FID);
        form.AddField("SID", savedData.data.SID);
        form.AddField("Grade", ques[cardNum].grade);
        form.AddField("TimeSpent", ques[cardNum].time);
        form.AddField("Confidence", ques[cardNum].confidence);
        form.AddField("Login", savedData.data.LoggedIn);

        Debug.Log("Form: " + form);

        using (UnityWebRequest webRequest = UnityWebRequest.Post("https://hemo-cardiac.azurewebsites.net/addFlashcardAttempt.php", form))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.Log("Couldn't connect to website when uploading data");
                Debug.Log("WebRequest text: " + webRequest.downloadHandler.text);
                Debug.Log("WebRequest result: " + webRequest.result);
                Debug.Log("WebRequest error: " + webRequest.error);
            }
            else if (webRequest.downloadHandler.text != "New record created successfully")
            {
                Debug.Log("Couldn't upload data");
                Debug.Log("WebRequest text: " + webRequest.downloadHandler.text);
                Debug.Log("WebRequest result: " + webRequest.result);
                Debug.Log("WebRequest error: " + webRequest.error);
            }
            else
            {
                Debug.Log("SID: " + savedData.data.SID + " LoggedIn: " + savedData.data.LoggedIn);
                Debug.Log("Form upload complete!");
                Debug.Log("WebRequest text: " + webRequest.downloadHandler.text);
                Debug.Log("WebRequest result: " + webRequest.result);
            }
        }
    }
}