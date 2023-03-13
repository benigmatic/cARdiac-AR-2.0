using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using TMPro;

public class DataManager : MonoBehaviour
{
    // Start() and Update() methods deleted - we don't need them right now

    public static DataManager Instance;
    public Login data;

    private void Awake()
    {
        
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        

        if (SceneManager.GetActiveScene().name != "LoginScene" && data.SID == "")
        {
            StartCoroutine(GetRequest());
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    IEnumerator GetRequest()
    {
        string error1 = "Invalid password";
        string error2 = "No Students found in the table";
        string uri = "https://hemo-cardiac.azurewebsites.net/login.php?var1=guest&var2=guest";
        Debug.Log("Checking request for https://hemo-cardiac.azurewebsites.net/login.php?var1=guest&var2=guest");
        Debug.Log("No active user in scene, loading guest");
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ConnectionError || webRequest.result == UnityWebRequest.Result.ProtocolError || webRequest.downloadHandler.text == error1)
            {
                Debug.Log(webRequest.downloadHandler.text);
                Debug.Log(webRequest.result);
                Debug.Log(webRequest.error);
                Debug.Log("Bad Website");
            }
            else if (webRequest.downloadHandler.text == error2)
            {
                Debug.Log(webRequest.downloadHandler.text);
                Debug.Log(webRequest.result);
                Debug.Log(webRequest.error);
                Debug.Log("Bad Website");
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);
                data = Login.CreateFromJSON(webRequest.downloadHandler.text);
                Debug.Log("Good website");
                Debug.Log("Login Successful :)");
            }
        }
    }
}