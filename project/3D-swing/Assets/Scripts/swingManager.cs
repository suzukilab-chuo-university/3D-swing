using UnityEngine;

public class swingManager : MonoBehaviour
{
    private Quaternion defaultRotation;
    private int flagRotation;
    //private float f = 0.15f;  // 周期
    private float beforeTime;
    private bool flagSin;
    private bool flagRotate;


    public static float angleValue = 20f;
    public static float speedValue = 0.2f;  // 周期

    void Start()
    {
        defaultRotation = transform.rotation;
        flagRotation = 0;
        beforeTime = Time.time;
        flagSin = true;
        flagRotate = true;
    }

    void Update()
    {
        float time = Time.time;

        if (time >= beforeTime + 1.0f / speedValue)
        {
            beforeTime += 1.0f / speedValue;
            flagRotation++;
            flagRotation %= 2;
        }

        float sin = Mathf.Sin(2 * Mathf.PI * speedValue * time);
        if(sin > 0 && flagSin == false)
        {
            flagSin = true;
            if (flagRotate) flagRotate = false;
            else flagRotate = true;
        }
        else if (sin < 0)
        {
            flagSin = false;
        }

        //if (flagRotation == 0)

        if (flagRotate)
            {
                transform.rotation = Quaternion.AngleAxis(sin * angleValue, Vector3.up) * defaultRotation;
        }
        else
        {
            transform.rotation = Quaternion.AngleAxis(sin * angleValue, Vector3.left) * defaultRotation;
        }

        
    }
}
