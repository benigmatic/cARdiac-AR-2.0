using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroToScene : MonoBehaviour
{
    public GameObject sceneContent;

    public GameObject introPrompt;

    // Start is called before the first frame update
    void Start()
    {
        sceneContent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showScene()
    {
        sceneContent.SetActive(true);

        introPrompt.SetActive(false);
    }
}
