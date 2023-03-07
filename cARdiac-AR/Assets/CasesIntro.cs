using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CasesIntro : MonoBehaviour
{
    public GameObject caseContent;

    public GameObject casePrompt;

    // Start is called before the first frame update
    void Start()
    {
        caseContent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showCases()
    {
        caseContent.SetActive(true);

        casePrompt.SetActive(false);
    }
}
