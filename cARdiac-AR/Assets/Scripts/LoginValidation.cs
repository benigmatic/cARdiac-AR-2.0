using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoginValidation : MonoBehaviour
{
    public TMP_InputField email;

    public TMP_InputField password;

    public TextAsset textAssetData;

    public TMP_Text errorText;

    public GameObject[] canvas;

    string filename = "";

    [System.Serializable]
    public class User
    {
        public string SID;
        public string lastName;
        public string firstName;
        public string classSection;
        public string AppSettings;
        public string password;
        public string email;
        public int logins;
    }

    [System.Serializable]
    public class UserList
    {
        public User[] user;
    }

    public UserList userList = new UserList();

    public int tableSize;

    private void Start()
    {

        canvas[0].SetActive(true);
        filename = Application.dataPath + "/Scripts/StudentData.csv";
        ReadCSV();

        errorText.enabled = false;
    }

    void ReadCSV()
    {
        string[] data = textAssetData.text.Split(new string[] { ",", "\n" }, StringSplitOptions.None);

        tableSize = data.Length / 8 - 1;
        userList.user = new User[tableSize];

        for (int i = 0; i < tableSize; i++)
        {
            userList.user[i] = new User();
            userList.user[i].SID = data[8 * (i + 1)];
            userList.user[i].lastName = data[8 * (i + 1) + 1];
            userList.user[i].firstName = data[8 * (i + 1) + 2];
            userList.user[i].classSection = data[8 * (i + 1) + 3];
            userList.user[i].AppSettings = data[8 * (i + 1) + 4];
            userList.user[i].password = data[8 * (i + 1) + 5];
            userList.user[i].email = data[8 * (i + 1) + 6];
            userList.user[i].logins = int.Parse(data[8 * (i + 1) + 7]);
        }
    }

    public void WriteCSV()
    {
        if(userList.user.Length > 0)
        {
            TextWriter tw = new StreamWriter(filename, false);
            tw.WriteLine("SID,LastName,FirstName,ClassSection,AppSettings,Password,Email,Logins");
            tw.Close();

            tw = new StreamWriter(filename, true);

            for (int i = 0; i < userList.user.Length; i++)
            {
                tw.WriteLine(userList.user[i].SID + "," + userList.user[i].lastName + "," + userList.user[i].firstName + "," + userList.user[i].classSection + "," +
                    userList.user[i].AppSettings + "," + userList.user[i].password + "," + userList.user[i].email + "," + userList.user[i].logins);
            }

            tw.Close();
        }
    }

    public void CheckValidation()
    {
        string eInput = email.text;
        string pInput = password.text;

        if (eInput == "" || pInput == "")
        {
            Debug.Log("Please enter a email/password");
            errorText.enabled = true;
        }

        for (int i = 0; i < tableSize; i++)
        {
            if (eInput == userList.user[i].email && pInput == userList.user[i].password)
            {
                Debug.Log("Login Successful :)");
                userList.user[i].logins = userList.user[i].logins + 1;
                WriteCSV();

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                return;
            }
        }

        Debug.Log("Login failed. Please enter correct email/password");
        errorText.enabled = true;
    }
}
