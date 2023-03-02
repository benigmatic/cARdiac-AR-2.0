using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HeartControls : MonoBehaviour
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

    private float currentTime = 0f;
    private float startingTime = 0.1f;

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
        Debug.Log("Heart reset is called!");
        heartModel.transform.position = resetAnchor.transform.position + new Vector3(0, 0, 0);
        heartModel.transform.eulerAngles = resetAnchor.transform.eulerAngles;
        heartModel.transform.localScale =  new Vector3(.025f, .025f, .025f);
        //GameObject.Find("HealthyHeart").GetComponent(Follow).enabled = true;

        //heartModel.transform.position = resetAnchor.transform.position;

    //     anchorPos = resetAnchor.transform.position;
    //     heartPos = heartModel.transform.position;
    //     aRot = resetAnchor.transform.eulerAngles;
    //     Debug.Log("After Anchor Rotation: " + aRot.ToString("F4"));
    //     Debug.Log("Anchor After Position: " + anchorPos.ToString("F4"));
    //     Debug.Log("Heart After Position: " + heartPos.ToString("F4"));
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
}
