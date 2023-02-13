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

    private Vector3 heartPos;

    private Vector3 anchorPos;

    private Vector3 aRot;

    private Vector3 hRot;

    private float currentTime = 0f;
    private float startingTime = .01f;


    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        // This is so the heart starts out in reset position when the scene loads.
        // resetHeartPosition doesn't work in Start() because there's buffer time.
        currentTime -= 1 * Time.deltaTime;

        if (currentTime == 0)
        {
            currentTime = 0;

            resetHeartPosition();
        }
    }

    public void resetHeartPosition()
    {
        heartModel.transform.position = resetAnchor.transform.position + new Vector3(0, 0, 0);
        heartModel.transform.eulerAngles = resetAnchor.transform.eulerAngles;
        heartModel.transform.localScale =  new Vector3(.2f, .2f, .2f);
        //GameObject.Find("HealthyHeart").GetComponent(Follow).enabled = true;

        //heartModel.transform.position = resetAnchor.transform.position;

    //     anchorPos = resetAnchor.transform.position;
    //     heartPos = heartModel.transform.position;
    //     aRot = resetAnchor.transform.eulerAngles;
    //     Debug.Log("After Anchor Rotation: " + aRot.ToString("F4"));
    //     Debug.Log("Anchor After Position: " + anchorPos.ToString("F4"));
    //     Debug.Log("Heart After Position: " + heartPos.ToString("F4"));
    }
}
