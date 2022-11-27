using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginValidation : MonoBehaviour
{
    public TMP_InputField email;

    public TMP_InputField password;

    public GameObject[] canvas;

    private void Start()
    {
        canvas[0].SetActive(true);
    }
    public void CheckValidation()
    {
        string eInput = email.text;
        string pInput = password.text;

        if (eInput == "123@aol.com" && pInput == "123")
        {
            Debug.Log("Login Successful :)");
        }
        else if (eInput == "" || pInput == "")
        {
            Debug.Log("Login failed. Please enter correct email/password");
        }
        else
        {
            Debug.Log("Please enter a email/password");
        }
    }
}
