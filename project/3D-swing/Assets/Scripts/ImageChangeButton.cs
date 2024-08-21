using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChangeButton : MonoBehaviour
{
    void Start()
    {
        //this.GetComponentInChildren<Image>().color = Color.white;
        this.GetComponent<Image>().color = Color.gray;
    }

    public void OnClickLeftButton()
    {
        return;
    }

    public void OnClickRightButton()
    {
        return;
    }
}
