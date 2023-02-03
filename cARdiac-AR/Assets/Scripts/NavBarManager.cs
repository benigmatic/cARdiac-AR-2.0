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
        promptText.text = "Here you can manipulate a 3D augmented.";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
