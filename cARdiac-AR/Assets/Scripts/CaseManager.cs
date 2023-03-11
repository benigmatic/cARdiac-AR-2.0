using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;

public class CaseManager : MonoBehaviour
{
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

    private int answerChoice = 0;

    private int correctAnswer = 0;

    private int currentCaseIndex = 0;

    private int previousCaseIndex = -1;

    // This is unresponsive button in toggle collection used to reset the toggle collection.
    private int bufferChoice = 3;

    // Start is called before the first frame update
    void Start()
    {
        resultPrompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getCase(int caseNum)
    {
        currentCaseIndex = caseNum;

        if (currentCaseIndex == 0)
        {
            caseTitle.text = "Case 1";
            caseQuestionText.text = "A 49-year-old man presents to the emergency department with palpatations. " +
            "He has felt fatigued all day and the feeling got worse after drinking alcohol. " +
            "The animation represents the patient's cardiac activity. " +
            "Which of the following ECG strips best fits the patient's arrhythmia?";
            resultDescription.text = "The ECG shows indistinct P waves with QRS complexes at irregular intervals. " +
            "Alcohol consumption is a risk factor for atrial fibrillation.";
            correctAnswer = 2;
            SetAnswerImages();
            atrialFibr();
        }
        else if (currentCaseIndex == 1)
        {
            caseTitle.text = "Case 2";
            caseQuestionText.text = "A 60-year-old woman presents to her physician feeling lightheaded and like her heart is racing. " +
            "Patient history is significant for atrial fibrillation for which she is currently taking the antiarrhythmic drug propafenone. " +
            "The animation represents the patient's cardiac activity. " +
            "Which of the following ECG strips best fits the patient's arrhythmia?";
            resultDescription.text = "The ECG demonstrates the sawtooth pattern with 2:1 conduction. " +
            "The proarrhythmia effects of the drug have induced atrial flutter.";
            correctAnswer = 2;
            SetAnswerImages();
            atrialFlut();
        }
        else if (currentCaseIndex == 2)
        {
            caseTitle.text = "Case 3";
            caseQuestionText.text = "A 56-year-old man presents to the emergency department with palpitations shortness of breath and a " + 
            "pulsing sensation in his neck. The patient recalls exercising just prior to the onset of symptoms. " + 
            "The animation represents the patient's cardiac activity. " + 
            "Which of the following ECG strips best fits the patient's arrhythmia?";
            resultDescription.text = "The ECG reflects AVNRT. Notice the tachycardia and retrograde P waves (upward deflections) " +
            "after the QRS complex (large downward deflections). The pulsing sensation in the neck results from the atria contracting " +
            "against a closed tricuspid valve because the atria and ventricles can contract nearly simultaneously.";
            correctAnswer = 3;
            SetAnswerImages();
            avnrt();
        }
    }

    public void nextCase()
    {
        // Save the current case number to previous case.
        previousCaseIndex = currentCaseIndex;

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
    }

    public void previousCase()
    {
        // Set the current case to the previous case.
        currentCaseIndex = previousCaseIndex;

        // Set the previous case to -1 so that the previous case button is not shown.
        previousCaseIndex = -1;

        getCase(currentCaseIndex);
        resultPrompt.SetActive(false);
        submitButton.SetActive(true);
        DeselectAllToggles();
    }

    public void getAnswer(int answer)
    {
        answerChoice = answer;
    }

    public void Submit()
    {
        submitButton.SetActive(false);

        // Only show previous case button if user has already gone through a case.
        if (previousCaseIndex != -1)
        {
            previousCaseButton.SetActive(true);
        }
        else
        {
            previousCaseButton.SetActive(false);
        }

        if (answerChoice == correctAnswer)
        {
            resultPrompt.SetActive(true);
            correctPrompt.SetActive(true);
            incorrectPrompt.SetActive(false);
            resultTitle.text = "Correct";
        }
        else
        {
            resultPrompt.SetActive(true);
            incorrectPrompt.SetActive(true);
            correctPrompt.SetActive(false);
            resultTitle.text = "Incorrect";
        }
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


}
