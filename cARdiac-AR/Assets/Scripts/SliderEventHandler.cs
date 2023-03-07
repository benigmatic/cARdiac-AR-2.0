using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;

// using Microsoft.MixedReality.Toolkit.UI to make use of SliderEventData ;

public class SliderEventHandler : MonoBehaviour
{
    public int sliderValue;
    public Dictionary<double, int> confidenceLevels = new Dictionary<double, int>() {
        {0, 1},
        {0.5, 2},
        {1, 3}
    };

    // OnInteractionEnded setting function 
    public void OnInteractionEnded(SliderEventData eventData)
    {
        sliderValue = confidenceLevels[eventData.NewValue];
        FindObjectOfType<FlashcardManager>().getConfidence(sliderValue);

        Debug.Log("OnInteractionEnded Event:" + sliderValue);
        // Debug.Log("Slider Value is: " + SliderValue);
    }

    public float SliderValue { get; set; }

    public Vector3 SliderStartPosition { get; set; }
}