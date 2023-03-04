using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class M1HeartRhythms : MonoBehaviour
{
    public Animator heartAnim;

    public VideoPlayer ekg;

    // Start is called before the first frame update
    void Start()
    {
        normal();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void bradycardia()
    {
        heartAnim.SetFloat("Speed", 0.5f);
        ekg.playbackSpeed = 0.5f;
    }

    public void normal()
    {
        heartAnim.SetFloat("Speed", 1f);

        ekg.playbackSpeed = 1f;
    }

    public void tradycardia()
    {
        heartAnim.SetFloat("Speed", 1.5f);

        ekg.playbackSpeed = 1.5f;
    }
}
