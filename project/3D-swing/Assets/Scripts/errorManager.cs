using UnityEngine;
using UnityEngine.UI;

public class errorManager : MonoBehaviour
{
    public GameObject errorDialog;
    public Text errorMessage;
    public static string errorReason = "";
    public static bool errorFlag = false;


    void Start()
    {
        errorDialog.SetActive(false);
    }

    void Update()
    {
        if (errorFlag)
        {
            errorFlag = false;
            DisplayError();
        }
    }

    void DisplayError()
    {
        errorDialog.SetActive(true);
        errorMessage.text = errorReason;
    }

    public void HiddenError()
    {
        errorDialog.SetActive(false);
    }
}
