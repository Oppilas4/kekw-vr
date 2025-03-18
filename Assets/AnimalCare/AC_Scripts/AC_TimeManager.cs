using UnityEngine;

public class AC_TimeManager : MonoBehaviour
{
    public static float timeInSeconds = 0f;  

    void Update()
    {
        timeInSeconds += Time.deltaTime;  
    }
}