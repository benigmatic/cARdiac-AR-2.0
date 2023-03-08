using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using TMPro;

public class CaseManager : MonoBehaviour
{
    public GameObject correctPrompt;

    public GameObject incorrectPrompt;

    public GameObject resultPrompt;

    public GameObject submitButton;

    public TMP_Text caseTitle;

    public TMP_Text resultTitle;

    public TMP_Text resultDescription;

    private int answerChoice = 0;

    private int correctAnswer = 1;

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
        if (caseNum == 1)
        {
            caseTitle.text = "Case 1";
        }
        else if (caseNum == 2)
        {
            caseTitle.text = "Case 2";
        }
        else if (caseNum == 3)
        {
            caseTitle.text = "Case 3";
        }
    }

    public void getCorrectAnswer()
    {
        correctAnswer = 2;
    }

    public void getAnswer(int answer)
    {
        answerChoice = answer;
    }

    public void demoSubmit()
    {
        submitButton.SetActive(false);

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

    public void demoNext()
    {

    }
}
