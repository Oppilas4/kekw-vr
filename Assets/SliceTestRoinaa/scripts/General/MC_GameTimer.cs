//©Jooa järviluoma tehnyt hienon scriptin tätä projektia varten

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class MC_GameTimer : MonoBehaviour
{
    public static MC_GameTimer Instance { get; private set; }

    public GameObject minuteHand;
    public GameObject secondHand;
    public float duration = 120f;

    public Image image;

    private bool isCountdownActive = false;

    public bool isTimerStopped = false;

    public UnityEvent timerStopped;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        StartCountdown();
    }

    public void StartCountdown()
    {
        if (!isCountdownActive)
        {
            isCountdownActive = true;
            StartCoroutine(CountdownRoutine());
        }
    }

    IEnumerator CountdownRoutine()
    {
        float elapsedTime = 0f;
        float secondHandRotationSpeed = 360f;
        float minuteHandRotationSpeed = 360f / duration;

        Color startColor = Color.green;
        Color orangeColor = Color.Lerp(startColor, Color.red, 0.5f);
        Color endColor = Color.red;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float secondRotation = secondHandRotationSpeed * Time.deltaTime;
            float minuteRotation = minuteHandRotationSpeed * Time.deltaTime;

            secondHand.transform.Rotate(0, 0, -secondRotation);

            minuteHand.transform.Rotate(0, 0, -minuteRotation);

            // Calculate the fill amount based on the elapsed time
            float fillAmount = 1 - elapsedTime / duration;
            image.fillAmount = fillAmount;

            float colorTransitionTime = elapsedTime / duration;
            if (colorTransitionTime <= 0.5f)
            {
                image.color = Color.Lerp(startColor, orangeColor, colorTransitionTime * 2);
            }
            else
            {
                float adjustedTime = (colorTransitionTime - 0.5f) * 2;
                image.color = Color.Lerp(orangeColor, endColor, adjustedTime);
            }

            yield return null;
        }

        minuteHand.transform.Rotate(-360f, 0, 0);

        image.fillAmount = 0;
        image.color = endColor;
        isCountdownActive = false;
        isTimerStopped = true;

        timerStopped?.Invoke();

    }

}
