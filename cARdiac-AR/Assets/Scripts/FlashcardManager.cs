using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Question
{
    public string question;
    public string correctAnswer;
    public Question( string q, string c)
    {
        question = q;
        correctAnswer = c;
    }
}

public class FlashcardManager : MonoBehaviour
{
    // Holds Flashcard object scale.
    public RectTransform r;

    public TMP_Text cardText;

    public GameObject flipButton;

    public GameObject continueButton;

    public Question[] ques = new Question[3];

    private float flipTime = 0.5f;

    // 0 is the front of the flashcard, 1 is the back of it.
    private int faceSide = 0;

    // -1 means get smaller, 1 means get bigger
    private int isShrinking = -1;

    private bool isFlipping = false;

    // Starting flashcard;
    private int cardNum = 0;

    private float distancePerTime;

    private float timeCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        continueButton.SetActive(false);

        ques[0] = new Question("Zero", "0");
        ques[1] = new Question("One", "1");
        ques[2] = new Question("Two", "2");

        distancePerTime = r.localScale.x / flipTime;

        cardNum = 0;

        cardText.text = ques[cardNum].question;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlipping)
        {
            Vector3 v = r.localScale;
            v.x += isShrinking * distancePerTime * Time.deltaTime;
            r.localScale = v;

            timeCount += Time.deltaTime;

            if ((timeCount >= flipTime) && (isShrinking < 0))
            {
                // Make the card grow.
                isShrinking = 1;
                timeCount = 0;

                if (faceSide == 0)
                {
                    // Front of the card to the back.
                    faceSide = 1;
                    cardText.text = ques[cardNum].correctAnswer;
                }
                else
                {
                    // Back of the card to front.
                    faceSide = 0;
                    cardText.text = ques[cardNum].correctAnswer;
                }
            }

            else if ((timeCount >= flipTime)  && (isShrinking == 1))
            {
                isFlipping = false;
            }
        }
    }

    public void NextCard()
    {
        faceSide = 0;
        cardNum++;
        if (cardNum >= ques.Length)
        {
            cardNum = 0;
        }
        cardText.text = ques[cardNum].question;
    }

    public void FlipCard()
    {
        continueButton.SetActive(true);
        flipButton.SetActive(false);
        timeCount = 0;
        isFlipping = true;
        isShrinking = -1;
    }
}
