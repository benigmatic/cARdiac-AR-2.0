using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class NavBarManager : MonoBehaviour
{
    private string sceneName;

    public GameObject flashcardSection;

    public GameObject casesSection;

    public GameObject heartSection;

    public GameObject heartModel;

    public TMP_Text promptTitle;

    public TMP_Text promptText;

    // Start is called before the first frame update
    void Start()
    {
        flashcardSection.SetActive(false);

        casesSection.SetActive(false);
        
        StartPrompt();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPrompt()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        sceneName = currentScene.name;
        string m1SceneName = "M1HeartScene";
        string m2SceneName = "M2HeartScene";

        if(String.Equals(sceneName, m1SceneName))
        {
            promptTitle.text = "Welcome to the UCF Cardiac M1 Section!";
            promptText.text = "Here you can manipulate and examine a 3D augmented heart model. " + 
            "As well as learn about the determinants of cardiac output and view forms of heart rates. " +
            "Furthermore, test your knowledge and understanding by navigating to the flashcards or cases tabs!";
        }
        // In case there is ever a start prompt for M2.
        // else if(String.Equals(sceneName, m2SceneName))
        // {
            
        // } 

    }

    public void contractility()
    {
        heartSection.SetActive(true);
        flashcardSection.SetActive(false);
        heartModel.SetActive(true);
        FindObjectOfType<M1HeartControls>().resetHeartPosition();

        promptTitle.text = "Contractility";
        promptText.text = "The amount of calcium entering a cardiac muscle cell affects its contractility, " +
        "i.e., the rate and amount of tension it produces. The greater the calcium influx, " + 
        "the faster and stronger the contraction, and the greater the volume of blood that is ejected." +
        System.Environment.NewLine + System.Environment.NewLine + "↑ calcium influx = ↑ contractility, ↑ stroke volume" +
        System.Environment.NewLine + System.Environment.NewLine + "Some factors that affect contractility:" +
        System.Environment.NewLine + "autonomic activity, pharmacological agents, hypercalcemia.";
    }

    public void preload()
    {
        heartSection.SetActive(true);
        flashcardSection.SetActive(false);
        heartModel.SetActive(true);
        FindObjectOfType<M1HeartControls>().resetHeartPosition();

        promptTitle.text = "Preload";
        promptText.text = "Preload is the degree of stretching of cardiac muscle cells as the ventricles fill with blood. " +
        "The more cardiac muscle cells are stretched, the greater the force they can produce, and the greater the volume " +
        "of blood that is ejected." + System.Environment.NewLine + System.Environment.NewLine +
        "↑ filling = ↑ preload, ↑ force, ↑ stroke volume" + System.Environment.NewLine + System.Environment.NewLine +
        "Some variables that affect preload:" + System.Environment.NewLine + "central venous pressure, blood volume, gravity.";
    }

    public void afterload()
    {
        heartSection.SetActive(true);
        flashcardSection.SetActive(false);
        heartModel.SetActive(true);
        FindObjectOfType<M1HeartControls>().resetHeartPosition();

        promptTitle.text = "Afterload";
        promptText.text = "Afterload is the stress on the left ventricular wall during contraction. Importantly, " +
        "it is proportional to the aortic pressure the heart must eject blood against. As aortic pressure increases, " +
        "LV wall stress increases, but the volume of blood can be ejected against that load decreases." +
        System.Environment.NewLine + System.Environment.NewLine + "↑ aortic pressure = ↑ afterload, ↓ stroke volume" +
        System.Environment.NewLine + System.Environment.NewLine + "Some conditions that affect afterload:" +
        System.Environment.NewLine + "hypertension, aortic valve disease, aortic stenosis.";
    }

    public void flashcards()
    {
        heartSection.SetActive(false);
        flashcardSection.SetActive(true);
        casesSection.SetActive(false);
        heartModel.SetActive(false);
    }

    public void cases()
    {
        heartSection.SetActive(false);
        flashcardSection.SetActive(false);
        casesSection.SetActive(true);
        heartModel.SetActive(true);
    }


    public void rhythms()
    {
        heartSection.SetActive(true);
        flashcardSection.SetActive(false);
        casesSection.SetActive(false);
        heartModel.SetActive(true);
        FindObjectOfType<M2HeartControls>().resetHeartPosition();
    }

    public void aorta()
    {
        promptTitle.text = "Aorta";
        promptText.text = "The main artery of the body, supplying oxygenated blood to the circulatory system.";
    }

    public void sinoatrialNode()
    {
        promptTitle.text = "Sinoatrial Node";
        promptText.text = "A small body of specialized muscle fibers, located in the right atrium of the heart, " +
        "whose activity is responsible for initiating the heartbeat.";
    }

    public void rightAtrium()
    {
        promptTitle.text = "Right Atrium";
        promptText.text = "Receives blood low in oxygen from the body and then empties the blood into the right ventricle.";
    }

    public void leftAtrium()
    {
        promptTitle.text = "Left Atrium";
        promptText.text = "Receives blood full of oxygen from the lungs and then empties the blood into the left ventricle.";        
    }

    public void antrioventricularNode()
    {
        promptTitle.text = "Atrioventricular Node";
        promptText.text = "A small structure in the heart, located in the Koch triangle, " + 
        "near the coronary sinus on the interatrial septum.";        
    }

    public void rightVentricle()
    {
        promptTitle.text = "Right Ventricle";
        promptText.text = "Pumps blood low in oxygen to the lungs.";        
    }

    public void leftVentricle()
    {
        promptTitle.text = "Left Ventricle";
        promptText.text = "Pumps blood full of oxygen out to the body.";  
    }

}
