using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;

public class LoginValidation : MonoBehaviour
{
    public TMP_InputField access;

    public TMP_InputField password;

    public TextAsset textAssetData;

    public DataManager savedData;

    public TMP_Text errorText;

    public GameObject[] canvas;

    public Login data;


    private void Start()
    {

        canvas[0].SetActive(true);

    }
    private void Awake()
    {
        errorText.enabled = false;
        errorText.gameObject.SetActive(false);
    }

    IEnumerator GetRequest(string uri)
    {
        string error1 = "Invalid password";
        string error2 = "No Students found in the table";
        Debug.Log("Checking request for https://hemo-cardiac.azurewebsites.net/login.php?var1=" + access.text + "&var2=" + password.text);
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.downloadHandler.text == error1)
            {
                errorText.text = webRequest.downloadHandler.text;
                errorText.gameObject.SetActive(true);
                errorText.enabled = true;
                Debug.Log(webRequest.downloadHandler.text);
                Debug.Log(webRequest.result);
                Debug.Log(webRequest.error);
                Debug.Log("Bad Website");
            }
            else if (webRequest.downloadHandler.text == error2)
            {
                errorText.text = "Access Code not recognized";
                errorText.gameObject.SetActive(true);
                errorText.enabled = true;
                Debug.Log(webRequest.downloadHandler.text);
                Debug.Log(webRequest.result);
                Debug.Log(webRequest.error);
                Debug.Log("Bad Website");
            }
            else
            {
                errorText.enabled = false;
                Debug.Log(webRequest.downloadHandler.text);
                data = Login.CreateFromJSON(webRequest.downloadHandler.text);

                savedData.data.SID = data.SID;
                savedData.data.Section = data.Section;
                savedData.data.LoggedIn = data.LoggedIn;
                Debug.Log("Good website");
                Debug.Log("Login Successful :)");

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }

    public void CheckValidation()
    {
        string aInput = access.text;
        string pInput = password.text;

        // Checks if website is valid.
        StartCoroutine(GetRequest("https://hemo-cardiac.azurewebsites.net/login.php?var1=" + aInput + "&var2=" + pInput));


        if (aInput == "" || pInput == "")
        {
            Debug.Log("Please enter an email/password");
            errorText.enabled = true;
        }
    }

    public void guestAccess()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    
}
