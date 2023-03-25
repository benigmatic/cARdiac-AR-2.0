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

    public GameObject slicedHeartModel;

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

    public GameObject sinusBloodContainer;

    public GameObject aFibBloodContainer;

    public GameObject aFlutBloodContainer;

    public GameObject avnrtBloodContainer;

    public GameObject sinusElectricContainer;

    public GameObject aFibElectricContainer;

    public GameObject aFlutElectricContainer;

    public GameObject avnrtElectricContainer;

    public GameObject[] upcomingSlicedHeartFeatures;

    public Animator sinusAnim;

    public Animator aFibAnim;

    public Animator aFlutAnim;

    public Animator avnrtAnim;

    public Animator[] sinusBlood;

    public Animator[] aFibBlood;

    public Animator[] aFlutBlood;

    public Animator[] avnrtBlood;

    public Animator[] sinusElectric;

    public Animator[] aFibElectric;

    public Animator[] aFlutElectric;

    public Animator[] avnrtElectric;

    public VideoPlayer sinusVideo;

    public VideoPlayer aFibVideo;

    public VideoPlayer aFlutVideo;

    public VideoPlayer avnrtVideo;

    public TMP_Text promptTitle;

    public TMP_Text promptText;

    public TMP_Text[] comingSoonText;

    private Vector3 heartPos;

    private Vector3 anchorPos;

    private Vector3 aRot;

    private Vector3 hRot;

    public Material transparentMaterial;

    public Material originalMaterial;

    private bool isBloodTransparent = false;

    private bool isElectricTransparent = false;

    private bool isSliced = false;

    private float currentTime = 0f;
    private float startingTime = 0.1f;


    // Speed states refer to the speed of the heart rate animation.
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

        sinusBloodContainer.SetActive(false);
        aFibBloodContainer.SetActive(false);
        aFlutBloodContainer.SetActive(false);
        avnrtBloodContainer.SetActive(false);

        sinusElectricContainer.SetActive(false);
        aFibElectricContainer.SetActive(false);
        aFlutElectricContainer.SetActive(false);
        avnrtElectricContainer.SetActive(false);

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

        // This is the reset values for the slice heart models. The heart model needs to be positioned higher if it's sliced.
        if (isSliced)
        {
            slicedHeartModel.transform.position = resetAnchor.transform.position + new Vector3(0, 0.1f, 0);
            slicedHeartModel.transform.eulerAngles = resetAnchor.transform.eulerAngles;
            slicedHeartModel.transform.localScale = new Vector3(4.8f, 4.8f, 4.8f);
        }
        else
        {
            // This is the reset values for the default heart models.
            activeHeartModel.transform.position = resetAnchor.transform.position + new Vector3(0, 0, 0);
            activeHeartModel.transform.eulerAngles = resetAnchor.transform.eulerAngles;
            activeHeartModel.transform.localScale =  new Vector3(4.8f, 4.8f, 4.8f);

            // If the heart was transparent before switching, make it transparent again.
            if (isBloodTransparent || isElectricTransparent)
            {
                activeHeartModel.GetComponent<Renderer>().material = transparentMaterial;
            }
            else
            {
                activeHeartModel.GetComponent<Renderer>().material = originalMaterial;
            }
        }
        
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


    // Toggle the anatomy labels for the heart models.
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

        foreach (Animator animator in sinusBlood)
        {
            animator.SetFloat("Speed", 0.5f);
        }

        foreach (Animator animator in aFibBlood)
        {
            animator.SetFloat("Speed", 0.5f);
        }

        foreach (Animator animator in aFlutBlood)
        {
            animator.SetFloat("Speed", 0.5f);
        }

        foreach (Animator animator in avnrtBlood)
        {
            animator.SetFloat("Speed", 0.5f);
        }

        foreach (Animator animator in sinusElectric)
        {
            animator.SetFloat("Speed", 0.5f);
        }

        foreach (Animator animator in aFibElectric)
        {
            animator.SetFloat("Speed", 0.5f);
        }

        foreach (Animator animator in aFlutElectric)
        {
            animator.SetFloat("Speed", 0.5f);
        }

        foreach (Animator animator in avnrtElectric)
        {
            animator.SetFloat("Speed", 0.5f);
        }
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

        foreach (Animator animator in sinusBlood)
        {
            animator.SetFloat("Speed", 1f);
        }

        foreach (Animator animator in aFibBlood)
        {
            animator.SetFloat("Speed", 1f);
        }

        foreach (Animator animator in aFlutBlood)
        {
            animator.SetFloat("Speed", 1f);
        }

        foreach (Animator animator in avnrtBlood)
        {
            animator.SetFloat("Speed", 1f);
        }

        foreach (Animator animator in sinusElectric)
        {
            animator.SetFloat("Speed", 1f);
        }

        foreach (Animator animator in aFibElectric)
        {
            animator.SetFloat("Speed", 1f);
        }

        foreach (Animator animator in aFlutElectric)
        {
            animator.SetFloat("Speed", 1f);
        }

        foreach (Animator animator in avnrtElectric)
        {
            animator.SetFloat("Speed", 1f);
        }
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

        foreach (Animator animator in sinusBlood)
        {
            animator.SetFloat("Speed", 1.5f);
        }

        foreach (Animator animator in aFibBlood)
        {
            animator.SetFloat("Speed", 1.5f);
        }

        foreach (Animator animator in aFlutBlood)
        {
            animator.SetFloat("Speed", 1.5f);
        }

        foreach (Animator animator in avnrtBlood)
        {
            animator.SetFloat("Speed", 1.5f);
        }

        foreach (Animator animator in sinusElectric)
        {
            animator.SetFloat("Speed", 1.5f);
        }

        foreach (Animator animator in aFibElectric)
        {
            animator.SetFloat("Speed", 1.5f);
        }

        foreach (Animator animator in aFlutElectric)
        {
            animator.SetFloat("Speed", 1.5f);
        }

        foreach (Animator animator in avnrtElectric)
        {
            animator.SetFloat("Speed", 1.5f);
        }
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

    public void blood()
    {
        // Toggle the transparency of the heart model for blood.
        if (!isBloodTransparent)
        {
            activeHeartModel.GetComponent<Renderer>().material = transparentMaterial;
            
            isBloodTransparent = true;
        }
        else
        {
            isBloodTransparent = false;
        }

        // If both blood and electric are not transparent, set the material back to the original unless its the slice model.
        if (!isBloodTransparent && !isElectricTransparent)
        {
            activeHeartModel.GetComponent<Renderer>().material = originalMaterial;
        }

        sinusBloodContainer.SetActive(!sinusBloodContainer.activeSelf);
        aFibBloodContainer.SetActive(!aFibBloodContainer.activeSelf);
        aFlutBloodContainer.SetActive(!aFlutBloodContainer.activeSelf);
        avnrtBloodContainer.SetActive(!avnrtBloodContainer.activeSelf);
    }

    public void electric()
    {
        // Toggle the transparency of the heart model for electric. Unless its the slice model.
        if (!isElectricTransparent)
        {
            activeHeartModel.GetComponent<Renderer>().material = transparentMaterial;

            isElectricTransparent = true;
        }
        else
        {
            isElectricTransparent = false;
        }

        // If both blood and electric are not transparent, set the material back to the original unless its the slice model..
        if (!isBloodTransparent && !isElectricTransparent)
        {
            activeHeartModel.GetComponent<Renderer>().material = originalMaterial;
        }

        sinusElectricContainer.SetActive(!sinusElectricContainer.activeSelf);
        aFibElectricContainer.SetActive(!aFibElectricContainer.activeSelf);
        aFlutElectricContainer.SetActive(!aFlutElectricContainer.activeSelf);
        avnrtElectricContainer.SetActive(!avnrtElectricContainer.activeSelf);
    }

    public void slice()
    {
        // Toggle slice model and set hide active heart model.
        if (!isSliced)
        {
            isSliced = true;
            activeHeartModel.SetActive(false);
            slicedHeartModel.SetActive(true);
            
            // Hide all the upcoming features for the sliced heart model.
            foreach (GameObject feature in upcomingSlicedHeartFeatures)
            {
                feature.SetActive(false);
            }

            // Show coming soon text for the sliced heart model.
            foreach (TMP_Text text in comingSoonText)
            {
                text.gameObject.SetActive(true);
            }
        }
        else
        {
            isSliced = false;
            activeHeartModel.SetActive(true);
            slicedHeartModel.SetActive(false);

            // Show features for the other heart models.
            foreach (GameObject feature in upcomingSlicedHeartFeatures)
            {
                feature.SetActive(true);
            }

            // Hide coming soon text for the other heart models.
            foreach (TMP_Text text in comingSoonText)
            {
                text.gameObject.SetActive(false);
            }
        }

        resetHeartPosition();

    }

}
