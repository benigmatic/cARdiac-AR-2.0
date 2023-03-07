using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class M2HeartRhythms : MonoBehaviour
{
    public GameObject gameObjectA;

    public GameObject gameObjectB;

    private GameObject activeGameObject;

    public TMP_Text promptTitle;

    public TMP_Text promptText;

    // Start is called before the first frame update
    void Start()
    {
        // Set the initial active game object
        activeGameObject = gameObjectA;
        activeGameObject.SetActive(true);

        // Disable the other two game objects
        gameObjectB.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void atrialFibr()
    {
        activeGameObject.SetActive(false);

        activeGameObject = gameObjectB;

        // Enable the new active game object
        activeGameObject.SetActive(true);

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
        activeGameObject.SetActive(false);

        activeGameObject = gameObjectA;

        // Enable the new active game object
        activeGameObject.SetActive(true);

        promptTitle.text = "Symptoms:";
        promptText.text = "• Healthy function";
    }

    public void slow()
    {

    }

    public void normal()
    {

    }

    public void fast()
    {

    }
}
