using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashcardIntro : MonoBehaviour
{
    public GameObject flashcardContent;

    public GameObject flashcardPrompt;

    // Start is called before the first frame update
    void Start()
    {
        flashcardContent.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showFlashcards()
    {
        flashcardContent.SetActive(true);

        flashcardPrompt.SetActive(false);
    }
}
