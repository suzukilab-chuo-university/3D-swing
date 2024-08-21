using UnityEngine;

public class SwingManager : MonoBehaviour
{
    private Quaternion defaultRotation;

    private float angleValue = 40f;  // スイングの最大角度

    public static float speedValue = 0.2f;  // 周期
    public static float beforeTime;

    void Start()
    {
        defaultRotation = transform.rotation;
        beforeTime = Time.time;
    }

    void Update()
    {
        float time = Time.time - beforeTime;
        float sin = Mathf.Sin(2 * Mathf.PI * speedValue * time);

        transform.rotation = Quaternion.AngleAxis(sin * angleValue, Vector3.up) * defaultRotation;
    }
}
