using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

// using Microsoft.MixedReality.Toolkit.UI to make use of SliderEventData ;

public class SliderEventHandler : MonoBehaviour
{
    public Dictionary<double, string> confidenceLevels = new Dictionary<double, string>() {
        {0, "No Clue" },
        {0.5, "Guesstimate"},
        {1, "Certain"}
    };

    // OnInteractionEnded setting function 
    public void OnInteractionEnded(SliderEventData eventData)
    {
        Debug.Log("OnInteractionEnded Event:" + confidenceLevels[eventData.NewValue]);

        Debug.Log("Slider Value is: " + SliderValue);
    }

    public float SliderValue { get; set; }

    public Vector3 SliderStartPosition { get; set; }
}