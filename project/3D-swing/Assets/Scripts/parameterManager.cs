using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class parameterManager : MonoBehaviour
{
    public Slider depthSlider;
    public Slider angleSlider;
    public Slider speedSlider;
    public Slider sizeSlider;
    public GameObject Mesh;

    void Start()
    {
        depthSlider.maxValue = 1.5f;
        depthSlider.value = 0.75f;

        angleSlider.maxValue = 40;
        angleSlider.value = 20;

        speedSlider.maxValue = 0.4f;
        speedSlider.value = 0.2f;

        sizeSlider.maxValue = 0.5f;
        sizeSlider.value = 0.5f;
    }

    public void depthUpdate()
    {
        generateMesh.depthValue = 1.9f - depthSlider.value;
    }

    public void angleUpdate()
    {
        swingManager.angleValue = angleSlider.value;
    }

    public void speedUpdate()
    {
        swingManager.speedValue = speedSlider.value + 0.01f;
    }

    public void sizeUpdate()
    {
        Mesh.transform.localScale = new Vector3(sizeSlider.value+0.5f, sizeSlider.value+0.5f, 1);
    }
}
