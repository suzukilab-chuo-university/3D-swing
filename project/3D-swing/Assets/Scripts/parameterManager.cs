using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ParameterManager : MonoBehaviour
{
    private Slider speedSlider;

    void Start()
    {
        speedSlider = GetComponent<Slider>();
        speedSlider.maxValue = 0.4f;
        speedSlider.value = 0.2f;
    }

    public void speedUpdate()
    {
        SwingManager.speedValue = speedSlider.value;
    }
}
