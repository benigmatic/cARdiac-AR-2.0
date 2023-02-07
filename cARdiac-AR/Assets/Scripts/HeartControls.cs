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


    // Start is called before the first frame update
    void Start()
    {
        aRot = GameObject.Find("M1SceneContent").transform.eulerAngles;
        Debug.Log("Start Anchor Rotation: " + aRot.ToString("F4"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resetHeartPosition()
    {
        heartModel.transform.position = resetAnchor.transform.position + new Vector3(0, 0, 0);
        heartModel.transform.eulerAngles = resetAnchor.transform.eulerAngles;
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
