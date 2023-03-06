using UnityEngine;
using Microsoft.MixedReality.Toolkit.UI;
using System.Collections;

public class SliderController : MonoBehaviour
{
    private PinchSlider slider;
    private float initialSliderValue;

    private void Start()
    {
        slider = GetComponent<PinchSlider>();
        initialSliderValue = slider.SliderValue;
    }


    public void ResetSliderValue()
    {
        slider.SliderValue = initialSliderValue;
    }

    public bool HasSliderChanged()
    {
        return Mathf.Abs(slider.SliderValue - initialSliderValue) > 0.01f;
    }
}