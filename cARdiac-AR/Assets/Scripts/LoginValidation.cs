using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
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
