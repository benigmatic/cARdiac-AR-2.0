using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class NavBarManager : MonoBehaviour
{
    public TMP_Text promptTitle;

    public TMP_Text promptText;

    // Start is called before the first frame update
    void Start()
    {
        promptTitle.text = "Welcome to the UCF Cardiac Application!";
        promptText.text = "Here you can manipulate and examine a 3D augmented heart model. " + 
        "As well as learn about the determinants of cardiac output and view forms of heart rates." +
        "Furthermore, test your knowledge and understanding by navigating to the flashcards or cases tabs!";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void contractility()
    {
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
        promptTitle.text = "Preload";
        promptText.text = "Preload is the degree of stretching of cardiac muscle cells as the ventricles fill with blood. " +
        "The more cardiac muscle cells are stretched, the greater the force they can produce, and the greater the volume " +
        "of blood that is ejected." + System.Environment.NewLine + System.Environment.NewLine +
        "↑ filling = ↑ preload, ↑ force, ↑ stroke volume" + System.Environment.NewLine + System.Environment.NewLine +
        "Some variables that affect preload:" + System.Environment.NewLine + "central venous pressure, blood volume, gravity.";
    }

    public void afterload()
    {
        promptTitle.text = "Afterload";
        promptText.text = "Afterload is the stress on the left ventricular wall during contraction. Importantly, " +
        "it is proportional to the aortic pressure the heart must eject blood against. As aortic pressure increases, " +
        "LV wall stress increases, but the volume of blood can be ejected against that load decreases." +
        System.Environment.NewLine + System.Environment.NewLine + "↑ aortic pressure = ↑ afterload, ↓ stroke volume" +
        System.Environment.NewLine + System.Environment.NewLine + "Some conditions that affect afterload:" +
        System.Environment.NewLine + "hypertension, aortic valve disease, aortic stenosis.";
    }
}
