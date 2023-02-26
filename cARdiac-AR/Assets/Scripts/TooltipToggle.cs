using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TooltipToggle : MonoBehaviour
{
    public GameObject tooltip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleTooltipActive() 
    {
        tooltip.SetActive(!tooltip.activeSelf);

        Debug.Log("Tooltip active: " + tooltip.activeSelf);
    }
}
