using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Networking;

public class Case
{
    public int FID;
    public string Prompt, AnswerA, AnswerB, AnswerC, Description;

    public Case(string q, string a, string b, string c ,string d)
    {
        Prompt = q;
        AnswerA = a;
        AnswerB = b;
        AnswerC = c;
        Description = d;
    }

    public static Case CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Case>(jsonString);
    }
}

public class CasesManager : MonoBehaviour
{
    public TMP_Text caseText;
    public TMP_Text statusText;
    public TMP_Text caseNumberText;
    public TMP_Text descriptionText;

    public GameObject AnswerChoiceA; 
    public GameObject AnswerChoiceB; 
    public GameObject AnswerChoiceC; 
       
    void Start () {
        Debug.Log("Hello world!");
    }
    
    // Update is called once per frame
    void Update () {
    
    }
}
