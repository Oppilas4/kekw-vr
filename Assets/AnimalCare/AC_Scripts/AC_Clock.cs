using UnityEngine;

public class AC_Clock : MonoBehaviour
{
    public Transform minuteHand;
    public Transform hourHand;

    void Update()
    {
        // Use timeInSeconds from AC_TimeManager instead of Time
        float minuteRotation = (AC_TimeManager.timeInSeconds / 60f) * 360f;
        minuteHand.localRotation = Quaternion.Euler(0, 0, -minuteRotation);  // Negative to rotate clockwise

        float hourRotation = (AC_TimeManager.timeInSeconds / 3600f) * 360f;
        hourHand.localRotation = Quaternion.Euler(0, 0, -hourRotation);  // Negative to rotate clockwise
    }
}
