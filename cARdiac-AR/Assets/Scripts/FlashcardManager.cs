using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;
using Microsoft.MixedReality.Toolkit.UI;

public class Question
{
    public int FID, grade, confidence;
    public string Prompt, Answer;

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
    public RectTransform r;     // Holds Flashcard object scale
    public TMP_Text cardText;
    public TMP_Text cardCounter;
    public GameObject flipButton;
    public GameObject correctButton;
    public GameObject incorrectButton;
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


    // Start is called before the first frame update
    void Start()
    {
        data = CSVReader.Read("cardiac_flashcards");
        correctButton.SetActive(false);
        incorrectButton.SetActive(false);
        distancePerTime = r.localScale.x / flipTime;

        
        

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
        cards[0] = new Question((string)data[0]["Prompt"], (string)data[0]["Answer"]);
        cards[1] = new Question((string)data[1]["Prompt"], (string)data[1]["Answer"]);
        cards[2] = new Question((string)data[2]["Prompt"], (string)data[2]["Answer"]);
        cards[3] = new Question((string)data[3]["Prompt"], (string)data[3]["Answer"]);
        cards[4] = new Question((string)data[4]["Prompt"], (string)data[4]["Answer"]);
        cards[5] = new Question((string)data[5]["Prompt"], (string)data[5]["Answer"]);
        cards[6] = new Question((string)data[6]["Prompt"], (string)data[6]["Answer"]);
        cards[7] = new Question((string)data[7]["Prompt"], (string)data[7]["Answer"]);
        cards[8] = new Question((string)data[8]["Prompt"], (string)data[8]["Answer"]);
        cards[9] = new Question((string)data[9]["Prompt"], (string)data[9]["Answer"]);
        cards[10] = new Question((string)data[10]["Prompt"], (string)data[10]["Answer"]);
        cards[11] = new Question((string)data[11]["Prompt"], (string)data[11]["Answer"]);
    }

    public void m2Cards(Question[] cards)
    {
        cards[0] = new Question((string)data[12]["Prompt"], (string)data[12]["Answer"]);
        cards[1] = new Question((string)data[13]["Prompt"], (string)data[13]["Answer"]);
        cards[2] = new Question((string)data[14]["Prompt"], (string)data[14]["Answer"]);
        cards[3] = new Question((string)data[15]["Prompt"], (string)data[15]["Answer"]);
        cards[4] = new Question((string)data[16]["Prompt"], (string)data[16]["Answer"]);
        cards[5] = new Question((string)data[17]["Prompt"], (string)data[17]["Answer"]);
        cards[6] = new Question((string)data[18]["Prompt"], (string)data[18]["Answer"]);
        cards[7] = new Question((string)data[19]["Prompt"], (string)data[19]["Answer"]);
        cards[8] = new Question((string)data[20]["Prompt"], (string)data[20]["Answer"]);
        cards[9] = new Question((string)data[21]["Prompt"], (string)data[21]["Answer"]);
        cards[10] = new Question((string)data[22]["Prompt"], (string)data[22]["Answer"]);
        cards[11] = new Question((string)data[23]["Prompt"], (string)data[23]["Answer"]);
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

                ques = new Question[12];
                if (string.Compare(sceneName, "M1HeartScene") == 0)
                    m1Cards(ques);
                else
                    m2Cards(ques);
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
        }
    }

    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        form.AddField("FID", ques[cardNum].FID);
        form.AddField("SID", savedData.data.SID);
        form.AddField("Grade", ques[cardNum].grade);
        form.AddField("TimeSpent", "00:00:00");
        form.AddField("Confidence", ques[cardNum].confidence);
        form.AddField("Login", savedData.data.LoggedIn);

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
                Debug.Log("Form upload complete!");
                Debug.Log("WebRequest text: " + webRequest.downloadHandler.text);
                Debug.Log("WebRequest result: " + webRequest.result);
            }
        }
    }
}