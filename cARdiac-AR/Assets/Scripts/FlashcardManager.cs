using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Question
{
    public string question, answer;

    public Question(string q, string a)
    {
        question = q;
        answer = a;
    }
}

public class FlashcardManager : MonoBehaviour
{
    // Holds Flashcard object scale
    public RectTransform r;     
    public TMP_Text cardText;
    public TMP_Text cardCounter;
    public GameObject flipButton;
    public GameObject continueButton;
    public Question[] ques = new Question[12];

    private float flipTime = 0.5f;
    // 0 is the front of the flashcard, 1 is the back of it.
    private int faceSide = 0;
    // -1 means get smaller, 1 means get bigger.  
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

        m1Cards(ques);
        // m2Cards(ques);

        distancePerTime = r.localScale.x / flipTime;
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
                    cardText.text = ques[cardNum].answer;
                }
                else
                {
                    // Back of the card to front.
                    faceSide = 0;
                    cardText.text = ques[cardNum].answer;
                }
            }
            else if ((timeCount >= flipTime) && (isShrinking == 1))
            {
                isFlipping = false;
            }
        }
    }

    public void NextCard()
    {
        continueButton.SetActive(false);
        flipButton.SetActive(true);
        faceSide = 0;
        cardNum++;
        if (cardNum >= ques.Length)
        {
            cardNum = 0;
        }

        cardText.text = ques[cardNum].question;
        cardCounter.text = (cardNum + 1).ToString() + " / 12";
    }

    public void FlipCard()
    {
        continueButton.SetActive(true);
        flipButton.SetActive(false);
        timeCount = 0;
        isFlipping = true;
        isShrinking = -1;
    }
    public void m1Cards(Question[] cards)
    {
        cards[0] = new Question("The force that the heart must contract against to pump blood into the systemic circulation:", "Afterload");
        cards[1] = new Question("The stretch on the heart prior to contraction:", "Preload");
        cards[2] = new Question("The force and rate that the heart beats with:", "Contractility");
        cards[3] = new Question("Contractility is determined by:", "Myocyte calcium levels");
        cards[4] = new Question("The most posterior chamber of the heart:", "Left atrium");
        cards[5] = new Question("The most anterior chamber of the heart:", "Right ventricle");
        cards[6] = new Question("The P wave on an ECG represents:", "Atrial depolarization");
        cards[7] = new Question("The QRS complex on an ECG represent:", "Ventricular depolarization");
        cards[8] = new Question("The T wave on an ECG represents:", "Ventricular repolarization");
        cards[9] = new Question("Electrical conduction in the heart is slowest through which node?", "Atrioventricular Node");
        cards[10] = new Question("Conduction through the AV node is represented on an ECG as:", "The PR interval");
        cards[11] = new Question("A slowed heart rate (i.e., regularly below 60 BPM in the adult) is called:", "Bradycardia");
    }

    public void m2Cards(Question[] cards)
    {
        cards[0] = new Question("Narrow QRS complex is typical of which arrhythmia?", "AVNRT");
        cards[1] = new Question("The \"sawtooth\" ECG pattern observed with atrial flutter is formed by which ECG component?", "P waves that are continuous");
        cards[2] = new Question("Sinus tachycardia is any heart rate above how many BPM in an adult?", "100 BMP");
        cards[3] = new Question("Which arrhythmia is characterized by supraventricular tachycardia(SVT)?", "AVNRT");
        cards[4] = new Question("\"Irregularly irregular\" RR interval is descriptive of which arrhythmia?", "Atrial fibrillation");
        cards[5] = new Question("Retrograde P wave is characteristic of which arrhythmia?", "AVNRT");
        cards[6] = new Question("Define 4:1 conduction in atrial flutter.", "4 atrial contractions to every 1 ventricular contraction");
        cards[7] = new Question("Sinus bradycardia is any heart rate above how many BPM in an adult?", "60 BPM");
        cards[8] = new Question("Risk of thromboembolism related to blood pooling in the atrial appendages is greatest in which arrhythmia?", "Atrial fibrillation");
        cards[9] = new Question("Vagal maneuvers are a potential intervention for tachyarrhythmias or bradyarrhythmias?", "Tachyarrhythmias, particularly supraventricular tachycardia (SVT)");
        cards[10] = new Question("The QRS complex on an ECG represents:", "Ventricular depolarization");
        cards[11] = new Question("The T wave on an ECG represents:", "Ventricular repolarization");
    }
}