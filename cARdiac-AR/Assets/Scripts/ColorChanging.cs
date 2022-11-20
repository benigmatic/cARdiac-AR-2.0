using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanging : MonoBehaviour
{
    public Material GreenMaterial;
    public Material RedMaterial;
    public Material BlueMaterial;
    public Material LightBlueMaterial;
    private Material PreviousMaterial;
    
    // Start is called before the first frame update
    void Start()
    {
        PreviousMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnRed()
    {
        GetComponent<Renderer>().material = RedMaterial;

        PreviousMaterial = RedMaterial;
    }

    public void TurnBlue()
    {
        GetComponent<Renderer>().material = BlueMaterial;

        PreviousMaterial = BlueMaterial;
    }

    public void TurnGreen()
    {
        GetComponent<Renderer>().material = GreenMaterial;

        PreviousMaterial = GreenMaterial;
    }

    public void TurnLightBlue()
    {
        PreviousMaterial = GetComponent<Renderer>().material;

        GetComponent<Renderer>().material = LightBlueMaterial;
    }

    public void TurnBack()
    {
        GetComponent<Renderer>().material = PreviousMaterial;
    }
}
