using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using TMPro;

public class M1HeartControls : MonoBehaviour
{
    public GameObject heartModel;
    // This will be a game object we will base the default heart position off of.
    public GameObject resetAnchor;

    public GameObject heartAnatomy;

    public GameObject tutorialWarning;

    private Vector3 heartPos;

    private Vector3 anchorPos;

    private Vector3 aRot;

    private Vector3 hRot;

    public Animator heartAnim;

    public VideoPlayer ekg;

    private float currentTime = 0f;
    private float startingTime = 0.1f;

    // 0 - slow, 1 - normal, 2 - fast
    private int speedState = 1;

    private bool startReset = true;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
        heartAnatomy.SetActive(false);
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
        Debug.Log("M1 Heart reset is called!");
        heartModel.transform.position = resetAnchor.transform.position + new Vector3(0, 0, 0);
        heartModel.transform.eulerAngles = resetAnchor.transform.eulerAngles;
        heartModel.transform.localScale =  new Vector3(0.1f, 0.1f, 0.1f);

        Debug.Log("Speed state = " + speedState);

        if (speedState == 0)
        {
            bradycardia();
        }
        else if (speedState == 1)
        {
            normal();
        }
        else if (speedState == 2)
        {
            tachycardia();
        }
    }

    public void anatomy()
    {
        heartAnatomy.SetActive(!heartAnatomy.activeSelf);
    }

    // Toggles the tutorial warning that can access the tutorial scene.
    public void tutorial()
    {
        tutorialWarning.SetActive(!tutorialWarning.activeSelf);
    }

    public void bradycardia()
    {
        speedState = 0;
        heartAnim.SetFloat("Speed", 0.5f);
        ekg.playbackSpeed = 0.5f;
    }

    public void normal()
    {
        speedState = 1;
        heartAnim.SetFloat("Speed", 1f);
        ekg.playbackSpeed = 1f;
    }

    public void tachycardia()
    {
        speedState = 2;
        heartAnim.SetFloat("Speed", 1.5f);
        ekg.playbackSpeed = 1.5f;
    }
}
