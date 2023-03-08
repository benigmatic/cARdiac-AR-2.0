using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

public class M2HeartControls : MonoBehaviour
{
    public GameObject sinusHeartModel;

    public GameObject aFibHeartModel;

    public GameObject aFlutHeartModel;

    public GameObject avnrtHeartModel;

    private GameObject activeHeartModel;

    // This will be a game object we will base the default heart position off of.
    public GameObject resetAnchor;

    public GameObject sinusAnatomy;

    public GameObject aFibAnatomy;

    public GameObject aFlutAnatomy;

    public GameObject avnrtAnatomy;

    public GameObject tutorialWarning;

    public GameObject sinusEKG;

    public GameObject aFibEKG;

    public GameObject aFlutEKG;

    public GameObject avnrtEKG;

    public Animator sinusAnim;

    public Animator aFibAnim;

    public Animator aFlutAnim;

    public Animator avnrtAnim;

    public VideoPlayer sinusVideo;

    public VideoPlayer aFibVideo;

    public VideoPlayer aFlutVideo;

    public VideoPlayer avnrtVideo;

    public TMP_Text promptTitle;

    public TMP_Text promptText;

    private Vector3 heartPos;

    private Vector3 anchorPos;

    private Vector3 aRot;

    private Vector3 hRot;

    private float currentTime = 0f;
    private float startingTime = 0.1f;

    // 0 - slow, 1 - normal, 2 - fast
    private int speedState = 1;

    private bool startReset = true;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;

        sinusAnatomy.SetActive(false);
        aFlutAnatomy.SetActive(false);
        aFibAnatomy.SetActive(false);
        avnrtAnatomy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // This is so the heart starts out in reset position when the scene loads.
        // resetHeartPosition doesn't work in Start() because there's buffer time.
        currentTime -= 1 * Time.deltaTime;

        if (currentTime <= 0 && startReset)
        {
            currentTime = 0;
            startReset = false;
            Debug.Log("Heart reset start!");
            resetHeartPosition();
        }
    }

    public void resetHeartPosition()
    {
        Debug.Log("Heart reset is called!");
        activeHeartModel.transform.position = resetAnchor.transform.position + new Vector3(0, 0, 0);
        activeHeartModel.transform.eulerAngles = resetAnchor.transform.eulerAngles;
        activeHeartModel.transform.localScale =  new Vector3(4.8f, 4.8f, 4.8f);

        if (speedState == 0)
        {
            slow();
        }
        else if (speedState == 1)
        {
            normal();
        }
        else if (speedState == 2)
        {
            fast();
        }
    }

    public void anatomy()
    {

        sinusAnatomy.SetActive(!sinusAnatomy.activeSelf);
        aFlutAnatomy.SetActive(!aFlutAnatomy.activeSelf);
        aFibAnatomy.SetActive(!aFibAnatomy.activeSelf);
        avnrtAnatomy.SetActive(!avnrtAnatomy.activeSelf);

    }

    // Toggles the tutorial warning that can access the tutorial scene.
    public void tutorial()
    {
        tutorialWarning.SetActive(!tutorialWarning.activeSelf);
    }

    public void atrialFibr()
    {

        activeHeartModel = aFibHeartModel;
 
        activeHeartModel.SetActive(true);

        aFibEKG.SetActive(true);

        // Disable the other heart models.
        sinusHeartModel.SetActive(false);
        aFlutHeartModel.SetActive(false);
        avnrtHeartModel.SetActive(false);

        sinusEKG.SetActive(false);
        aFlutEKG.SetActive(false);
        avnrtEKG.SetActive(false);


        resetHeartPosition();

        if (speedState == 0)
        {
            slow();
        }
        else if (speedState == 1)
        {
            normal();
        }
        else if (speedState == 2)
        {
            fast();
        }

        promptTitle.text = "Symptoms:";
        promptText.text = "• Palpitations" + System.Environment.NewLine + "• Tachycardia"
        + System.Environment.NewLine + "• Fatigue" + System.Environment.NewLine + "• Dizziness";
    }

    public void atrialFlut()
    {

        activeHeartModel = aFlutHeartModel;
 
        activeHeartModel.SetActive(true);

        aFlutEKG.SetActive(true);

        // Disable the other heart models.
        sinusHeartModel.SetActive(false);
        aFibHeartModel.SetActive(false);
        avnrtHeartModel.SetActive(false);

        sinusEKG.SetActive(false);
        aFibEKG.SetActive(false);
        avnrtEKG.SetActive(false);

        resetHeartPosition();

        if (speedState == 0)
        {
            slow();
        }
        else if (speedState == 1)
        {
            normal();
        }
        else if (speedState == 2)
        {
            fast();
        }

        promptTitle.text = "Symptoms:";
        promptText.text = "• Palpitations" + System.Environment.NewLine + "• Fatigue"
        + System.Environment.NewLine + "• Lightheadedness" + System.Environment.NewLine + "• Shortness of breath";
    }

    public void avnrt()
    {

        activeHeartModel = avnrtHeartModel;
 
        activeHeartModel.SetActive(true);

        avnrtEKG.SetActive(true);

        // Disable the other heart models.
        sinusHeartModel.SetActive(false);
        aFibHeartModel.SetActive(false);
        aFlutHeartModel.SetActive(false);

        sinusEKG.SetActive(false);
        aFibEKG.SetActive(false);
        aFlutEKG.SetActive(false);

        resetHeartPosition();

        if (speedState == 0)
        {
            slow();
        }
        else if (speedState == 1)
        {
            normal();
        }
        else if (speedState == 2)
        {
            fast();
        }

        promptTitle.text = "Symptoms:";
        promptText.text = "• Palpitations" + System.Environment.NewLine + "• Dizziness"
        + System.Environment.NewLine + "• Shortness of breath" + System.Environment.NewLine + "• Syncope";
    }

    public void sinus()
    {
        //activeHeartModel.SetActive(false);

        activeHeartModel = sinusHeartModel;
 
        activeHeartModel.SetActive(true);

        sinusEKG.SetActive(true);

        // Disable the other heart models.
        aFibHeartModel.SetActive(false);
        aFlutHeartModel.SetActive(false);
        avnrtHeartModel.SetActive(false);

        aFibEKG.SetActive(false);
        aFlutEKG.SetActive(false);
        avnrtEKG.SetActive(false);

        resetHeartPosition();

        if (speedState == 0)
        {
            slow();
        }
        else if (speedState == 1)
        {
            normal();
        }
        else if (speedState == 2)
        {
            fast();
        }

        promptTitle.text = "Symptoms:";
        promptText.text = "• Healthy function";
    }

    public void slow()
    {
        speedState = 0;

        sinusAnim.SetFloat("Speed", 0.5f);
        aFibAnim.SetFloat("Speed", 0.5f);
        aFlutAnim.SetFloat("Speed", 0.5f);
        avnrtAnim.SetFloat("Speed", 0.5f);

        sinusVideo.playbackSpeed  = 0.5f;
        aFibVideo.playbackSpeed  = 0.5f;
        aFlutVideo.playbackSpeed  = 0.5f;
        avnrtVideo.playbackSpeed  = 0.5f;
    }

    public void normal()
    {
        speedState = 1;

        sinusAnim.SetFloat("Speed", 1f);
        aFibAnim.SetFloat("Speed", 1f);
        aFlutAnim.SetFloat("Speed", 1f);
        avnrtAnim.SetFloat("Speed", 1f);

        sinusVideo.playbackSpeed  = 1f;
        aFibVideo.playbackSpeed  = 1f;
        aFlutVideo.playbackSpeed  = 1f;
        avnrtVideo.playbackSpeed  = 1f;
    }

    public void fast()
    {
        speedState = 2;

        sinusAnim.SetFloat("Speed", 1.5f);
        aFibAnim.SetFloat("Speed", 1.5f);
        aFlutAnim.SetFloat("Speed", 1.5f);
        avnrtAnim.SetFloat("Speed", 1.5f);

        sinusVideo.playbackSpeed  = 1.5f;
        aFibVideo.playbackSpeed  = 1.5f;
        aFlutVideo.playbackSpeed  = 1.5f;
        avnrtVideo.playbackSpeed  = 1.5f;
    }

    public void setHeartInactive()
    {
        if (activeHeartModel != null)
        {
            activeHeartModel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("activeHeartModel is null!");
        }
    }

    public void setHeartActive()
    {
        if (activeHeartModel != null)
        {
            activeHeartModel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("activeHeartModel is null!");
        }
    }
}