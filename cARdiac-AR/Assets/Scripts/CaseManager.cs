using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;

public class CaseQuestion
{
    public int CID, grade;
    public string Description, Rhythm, AnswerDescription, A, B, C, AnswerChoice, time;

    public CaseQuestion(string q, string r, string d, string a, string b, string c)
    {
        Description = q;
        Rhythm = r;
        AnswerDescription = d;
        A = a;
        B = b;
        C = c;
    }

    public static CaseQuestion CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<CaseQuestion>(jsonString);
    }
}


public class CaseManager : MonoBehaviour
{
    public CaseQuestion[] caseQuestions;

    public DataManager savedData;

    public GameObject sinusHeartModel;

    public GameObject aFibHeartModel;

    public GameObject aFlutHeartModel;

    public GameObject avnrtHeartModel;

    private GameObject activeHeartModel;

    // This will be a game object we will base the default heart position off of.
    public GameObject resetAnchor;

    public GameObject correctPrompt;

    public GameObject incorrectPrompt;

    public GameObject resultPrompt;

    public GameObject submitButton;

    public GameObject previousCaseButton;

    public TMP_Text caseTitle;

    public TMP_Text caseQuestionText;

    public TMP_Text resultTitle;

    public TMP_Text resultDescription;

    public GameObject[] answerDisplay;
    
    public Texture[] answerImages;

    public InteractableToggleCollection toggleCollection;

    Stopwatch watch = new Stopwatch();
    TimeSpan time;

    private string resultSelection;
    private string answerSelected;

    private string correctAnswer;

    private int currentCaseIndex = 0;

    private int previousCaseIndex = -1;

    // This is unresponsive button in toggle collection used to reset the toggle collection.
    private int bufferChoice = 3;

    // Start is called before the first frame update
    void Start()
    {
        resultPrompt.SetActive(false);

        // Get cases data.
        StartCoroutine(GetRequest(caseQuestions));

        // DataObj = GameObject.FindWithTag("Data");
        // savedData = DataObj.GetComponent<DataManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getCase(int caseNum)
    {
        currentCaseIndex = caseNum;

        caseQuestionText.text = caseQuestions[currentCaseIndex].Description;
        resultDescription.text = caseQuestions[currentCaseIndex].AnswerDescription;
        correctAnswer = caseQuestions[currentCaseIndex].Rhythm;

        if (currentCaseIndex == 0)
        {
            caseTitle.text = "Case 1";
            SetAnswerImages();
            atrialFibr();
        }
        else if (currentCaseIndex == 1)
        {
            caseTitle.text = "Case 2";
            SetAnswerImages();
            atrialFlut();
        }
        else if (currentCaseIndex == 2)
        {
            caseTitle.text = "Case 3";
            SetAnswerImages();
            avnrt();
        }

        watch.Start();  // Starts the timer
    }

    public void nextCase()
    {
        // Save the current case number to previous case.
        previousCaseIndex = currentCaseIndex;

        // Record the time the case was completed.
        watch.Stop();
        time = watch.Elapsed;
        caseQuestions[currentCaseIndex].time = time.Hours.ToString() + ":" + time.Minutes.ToString() + ":" + time.Seconds.ToString();
        Debug.Log(caseQuestions[currentCaseIndex].time);

        if (currentCaseIndex < 2)
        {
            currentCaseIndex++;
            getCase(currentCaseIndex);
            resultPrompt.SetActive(false);
            submitButton.SetActive(true);
            DeselectAllToggles();
        }
        else
        {
            // Loop back to the first case if user started on last case.
            currentCaseIndex = 0;
            getCase(currentCaseIndex);
            resultPrompt.SetActive(false);
            submitButton.SetActive(true);
            DeselectAllToggles();
        }

        // Reset the stopwatch and start it again.
        watch = new Stopwatch(); // Creates new stopwatch so time does not add together
        watch.Start();
    }

    public void previousCase()
    {
        // Record the time the case was completed.
        watch.Stop();
        time = watch.Elapsed;
        caseQuestions[currentCaseIndex].time = time.Hours.ToString() + ":" + time.Minutes.ToString() + ":" + time.Seconds.ToString();
        Debug.Log(caseQuestions[currentCaseIndex].time);

        // Set the current case to the previous case.
        currentCaseIndex = previousCaseIndex;

        // Set the previous case to -1 so that the previous case button is not shown.
        previousCaseIndex = -1;

        getCase(currentCaseIndex);
        resultPrompt.SetActive(false);
        submitButton.SetActive(true);
        DeselectAllToggles();

        // Reset the stopwatch and start it again.
        watch = new Stopwatch(); // Creates new stopwatch so time does not add together
        watch.Start();
    }

    public void getAnswer(int answer)
    {

        if (answer == 1)
        {
            resultSelection = caseQuestions[currentCaseIndex].A;
            answerSelected = "A";
        }
        else if (answer == 2)
        {
            resultSelection = caseQuestions[currentCaseIndex].B;
            answerSelected = "B";
        }
        else if (answer == 3)
        {
            resultSelection = caseQuestions[currentCaseIndex].C;
            answerSelected = "C";
        }
    }

    public void Submit()
    {
        submitButton.SetActive(false);
        // Gets time elapsed and countines timer, timer resets on next case
        watch.Stop();
        time = watch.Elapsed;
        caseQuestions[currentCaseIndex].time = time.Hours.ToString() + ":" + time.Minutes.ToString() + ":" + time.Seconds.ToString();
        Debug.Log(caseQuestions[currentCaseIndex].time);
        watch.Start();

        // Save answer to the case.
        caseQuestions[currentCaseIndex].AnswerChoice = answerSelected;

        // Only show previous case button if user has already gone through a case.
        if (previousCaseIndex != -1)
        {
            previousCaseButton.SetActive(true);
        }
        else
        {
            previousCaseButton.SetActive(false);
        }

        if (resultSelection == correctAnswer)
        {
            resultPrompt.SetActive(true);
            correctPrompt.SetActive(true);
            incorrectPrompt.SetActive(false);
            resultTitle.text = "Correct";

            // Assign grade to the case.
            caseQuestions[currentCaseIndex].grade = 100;
        }
        else
        {
            resultPrompt.SetActive(true);
            incorrectPrompt.SetActive(true);
            correctPrompt.SetActive(false);
            resultTitle.text = "Incorrect";

            // Assign grade to the case.
            caseQuestions[currentCaseIndex].grade = 0;
        }

        // Upload data to the database
        StartCoroutine(Upload());
    }

    // This will deselect all the toggle buttons.
    public void DeselectAllToggles()
    {
        toggleCollection.CurrentIndex = bufferChoice;
    }

    public void tryAgain()
    {
        resultPrompt.SetActive(false);
        submitButton.SetActive(true);
        DeselectAllToggles();
    }

    private void SetAnswerImages()
    {
        // Based on the case number, set the answer images.
        for (int i = 0; i < answerDisplay.Length; i++) 
        {
            Material mat = answerDisplay[i].GetComponent<MeshRenderer>().material;
            mat.mainTexture = answerImages[(currentCaseIndex * 3) + i];
        }
    }

    public void resetHeartPosition()
    {
        Debug.Log("Heart reset is called!");
        activeHeartModel.transform.position = resetAnchor.transform.position + new Vector3(0, 0, 0);
        activeHeartModel.transform.eulerAngles = resetAnchor.transform.eulerAngles;
        activeHeartModel.transform.localScale =  new Vector3(4.8f, 4.8f, 4.8f);
    }

     public void atrialFibr()
    {

        activeHeartModel = aFibHeartModel;
 
        activeHeartModel.SetActive(true);

        // Disable the other heart models.
        sinusHeartModel.SetActive(false);
        aFlutHeartModel.SetActive(false);
        avnrtHeartModel.SetActive(false);

        resetHeartPosition();
    }

    public void atrialFlut()
    {

        activeHeartModel = aFlutHeartModel;
 
        activeHeartModel.SetActive(true);

        // Disable the other heart models.
        sinusHeartModel.SetActive(false);
        aFibHeartModel.SetActive(false);
        avnrtHeartModel.SetActive(false);

        resetHeartPosition();
    }

    public void avnrt()
    {

        activeHeartModel = avnrtHeartModel;
 
        activeHeartModel.SetActive(true);

        // Disable the other heart models.
        sinusHeartModel.SetActive(false);
        aFibHeartModel.SetActive(false);
        aFlutHeartModel.SetActive(false);

        resetHeartPosition();

    }

    public void sinus()
    {

        activeHeartModel = sinusHeartModel;
 
        activeHeartModel.SetActive(true);

        // Disable the other heart models.
        aFibHeartModel.SetActive(false);
        aFlutHeartModel.SetActive(false);
        avnrtHeartModel.SetActive(false);

        resetHeartPosition();
    }

    IEnumerator GetRequest(CaseQuestion[] cases)
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        int val = (string.Compare(sceneName, "M1HeartScene") == 0) ? 1 : 2;
        string uri = "https://hemo-cardiac.azurewebsites.net//cases.php?var1=" + val;

        Debug.Log("Checking request for " + uri);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            // Check if this got here.
            Debug.Log("Here");

            // If webRequest fails or bad uri gets hardcoded case data, else gets case data from database
            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.downloadHandler.text == "No Cases found")
            {
                Debug.Log("Couldn't connect to website when retrieving data");
                Debug.Log("WebRequest text: " + webRequest.downloadHandler.text);
                Debug.Log("WebRequest result: " + webRequest.result);
                Debug.Log("WebRequest error: " + webRequest.error);
            }
            else
            {
                Debug.Log("WebRequest text: " + webRequest.downloadHandler.text);

                // Skips to appropiate JSON data, excludes {"Cases":[
                string casesPrompts = webRequest.downloadHandler.text.Substring(11);
                string[] promptsArray = casesPrompts.Split('{');
                caseQuestions = new CaseQuestion[promptsArray.Length];

                // Formats json to be {"Prompt"...}, adds deleted '{' and removes trailing ','
                for (int i = 0; i < promptsArray.Length; i++)
                {
                    promptsArray[i] = promptsArray[i].Insert(0, "{");
                    promptsArray[i] = promptsArray[i].Remove(promptsArray[i].Length - 1);

                    // Removes ']' from the end of the last Prompt Answer pair
                    if (i == promptsArray.Length - 1)
                        promptsArray[i] = promptsArray[i].Remove(promptsArray[i].Length - 1);

                    caseQuestions[i] = CaseQuestion.CreateFromJSON(promptsArray[i]);
                }
            }
            // Debug.Log(caseQuestions[0].A);
            // Debug.Log(caseQuestions[1].Rhythm);
            // Debug.Log(caseQuestions[2].Rhythm);
        }
    }

    IEnumerator Upload()
    {
        WWWForm form = new WWWForm();
        Debug.Log("SID: " + savedData.data.SID + " LoggedIn: " + savedData.data.LoggedIn);
        form.AddField("CID", caseQuestions[currentCaseIndex].CID);
        form.AddField("SID", savedData.data.SID);
        form.AddField("Grade", caseQuestions[currentCaseIndex].grade);
        form.AddField("TimeSpent", "00:00:00");
        form.AddField("Answer", caseQuestions[currentCaseIndex].AnswerChoice);
        form.AddField("Login", savedData.data.LoggedIn);

        Debug.Log("Form: " + form);

        using (UnityWebRequest webRequest = UnityWebRequest.Post("https://hemo-cardiac.azurewebsites.net/addCaseAttempt.php", form))
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
