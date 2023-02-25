using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class M2HeartRhythms : MonoBehaviour
{

    public TMP_Text promptTitle;

    public TMP_Text promptText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void atrialFibr()
    {
        promptTitle.text = "Symptoms:";
        promptText.text = "• Palpitations" + System.Environment.NewLine + "• Tachycardia"
        + System.Environment.NewLine + "• Fatigue" + System.Environment.NewLine + "• Dizziness";
    }

    public void atrialFlut()
    {
        promptTitle.text = "Symptoms:";
        promptText.text = "• Palpitations" + System.Environment.NewLine + "• Fatigue"
        + System.Environment.NewLine + "• Lightheadedness" + System.Environment.NewLine + "• Shortness of breath";
    }

    public void avnrt()
    {
        promptTitle.text = "Symptoms:";
        promptText.text = "• Palpitations" + System.Environment.NewLine + "• Dizziness"
        + System.Environment.NewLine + "• Shortness of breath" + System.Environment.NewLine + "• Syncope";
    }

    public void sinus()
    {
        promptTitle.text = "Symptoms:";
        promptText.text = "• Healthy function";
    }
}
