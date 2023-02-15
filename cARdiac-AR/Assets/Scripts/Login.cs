using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]

public class Login
{

    public string SID;
    public string Name;
    public string Section;


    public static Login CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<Login>(jsonString);
    }
}