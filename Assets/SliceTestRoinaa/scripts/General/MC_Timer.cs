using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.Events;

public class MC_Timer : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Pause = !Pause;
    }

    [SerializeField] private Image uiFill;
    [SerializeField] private TMP_Text uiText;

    public int Duration;

    private int remainingDuration;

    private bool Pause;

    public UnityEvent TimerComplete;

    private Coroutine timerCoroutine;

    public void StartTimer(int duration)
    {
        if (timerCoroutine != null)
        {
            // Stop the current coroutine if it's already running
            StopCoroutine(timerCoroutine);
        }

        // Start a new coroutine with the specified duration
        Begin(duration);
    }

    private void Begin(int seconds)
    {
        remainingDuration = seconds;
        timerCoroutine = StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingDuration >= 0)
        {
            if (!Pause)
            {
                uiText.text = $"{remainingDuration % 60}";
                uiFill.fillAmount = Mathf.InverseLerp(0, Duration, remainingDuration);
                remainingDuration--;
                yield return new WaitForSeconds(1f);
            }
            yield return null;
        }
        OnEnd();
    }

    private void OnEnd()
    {
        TimerComplete.Invoke();
    }
}
