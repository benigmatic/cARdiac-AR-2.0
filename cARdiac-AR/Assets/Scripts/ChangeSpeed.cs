using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpeed : MonoBehaviour
{
    public Animator anim;

    private float animationSpeed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHalfSpeed()
    {
        anim.SetFloat("Speed", animationSpeed);
        Debug.Log("Animation speed set to: " + anim.speed);
    }
}
