using UnityEngine;

public class AC_TimeManager : MonoBehaviour
{
    public static float timeInSeconds = 0f;  // This will keep track of the time in seconds.

    void Update()
    {
        timeInSeconds += Time.deltaTime;  // Increase time by deltaTime each frame.
    }
}