using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;

public class Question
{
    public int FID;
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

    private float flipTime = 0.5f;
    private int faceSide = 0;   // 0 is the front of the flashcard, 1 is the back of it
    private int isShrinking = -1;   // -1 means get smaller, 1 means get bigger
    private bool isFlipping = false;

    // Starting flashcard;
    private int cardNum = 0;
    private float distancePerTime;
    private float timeCount = 0;

    private float startingTick;

    // Start is called before the first frame update
    void Start()
    {
        // startingTick = SliderEventHandler.SliderValue;

        // Debug.Log("Starting tick = " + startingTick);

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

    public void NextCard()
    {
        correctButton.SetActive(false);
        incorrectButton.SetActive(false);

        faceSide = 0;
        cardNum++;
        if (cardNum >= ques.Length)
        {
            cardNum = 0;
        }

        cardText.text = ques[cardNum].Prompt;
        cardCounter.text = (cardNum + 1).ToString() + " / " + ques.Length;
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
        cards[0] = new Question("The force that the heart must contract against to pump blood into the systemic circulation:", "Afterload");
        cards[1] = new Question("The stretch on the heart prior to contraction:", "Preload");
        cards[2] = new Question("The force and rate that the heart beats with:", "Contractility");
        cards[3] = new Question("Contractility is determined by:", "Myocyte calcium levels");
        cards[4] = new Question("The most posterior chamber of the heart:", "Left atrium");
        cards[5] = new Question("The most anterior chamber of the heart:", "Right ventricle");
        cards[6] = new Question("The P wave on an ECG represents:", "Atrial depolarization");
        cards[7] = new Question("The QRS complex on an ECG represent:", "Ventricular depolarization");
        cards[8] = new Question("The T wave on an ECG represents:", "Ventricular repolarization");
        cards[9] = new Question("Electrical conduction in the heart is slowest through which node?", "Atrioventricular Node");
        cards[10] = new Question("Conduction through the AV node is represented on an ECG as:", "The PR interval");
        cards[11] = new Question("A slowed heart rate (i.e., regularly below 60 BPM in the adult) is called:", "Bradycardia");
    }

    public void m2Cards(Question[] cards)
    {
        cards[0] = new Question("Narrow QRS complex is typical of which arrhythmia?", "AVNRT");
        cards[1] = new Question("The \"sawtooth\" ECG pattern observed with atrial flutter is formed by which ECG component?", "P waves that are continuous");
        cards[2] = new Question("Sinus tachycardia is any heart rate above how many BPM in an adult?", "100 BMP");
        cards[3] = new Question("Which arrhythmia is characterized by supraventricular tachycardia(SVT)?", "AVNRT");
        cards[4] = new Question("\"Irregularly irregular\" RR interval is descriptive of which arrhythmia?", "Atrial fibrillation");
        cards[5] = new Question("Retrograde P wave is characteristic of which arrhythmia?", "AVNRT");
        cards[6] = new Question("Define 4:1 conduction in atrial flutter.", "4 atrial contractions to every 1 ventricular contraction");
        cards[7] = new Question("Sinus bradycardia is any heart rate above how many BPM in an adult?", "60 BPM");
        cards[8] = new Question("Risk of thromboembolism related to blood pooling in the atrial appendages is greatest in which arrhythmia?", "Atrial fibrillation");
        cards[9] = new Question("Vagal maneuvers are a potential intervention for tachyarrhythmias or bradyarrhythmias?", "Tachyarrhythmias, particularly supraventricular tachycardia (SVT)");
        cards[10] = new Question("The QRS complex on an ECG represents:", "Ventricular depolarization");
        cards[11] = new Question("The T wave on an ECG represents:", "Ventricular repolarization");
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
                Debug.Log("Couldn't connect to website");
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

            Debug.Log(ques[0].Prompt);
            Debug.Log(ques[1].Prompt);

            cardText.text = ques[cardNum].Prompt;
            cardCounter.text = 1 + " / " + ques.Length;
        }
    }
}