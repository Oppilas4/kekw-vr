using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class AC_CountdownTimer : MonoBehaviour
{
    public float totalTime = 300f; // Total time in seconds
    public TextMeshProUGUI countdownText; // Text to display the countdown
    private float currentTime; // Current time remaining
    private bool timerStarted = false; // Flag to check if the timer has been started
    public AC_ChecklistManager checklist;
    private void Update()
    {
        // Check if the Hi object is inactive and the timer hasn't been started yet
        if (!timerStarted)
        {
            StartCountdown();
        }
    }

    private void StartCountdown()
    {
        currentTime = totalTime;
        UpdateCountdownText();
        StartCoroutine(Countdown());
        timerStarted = true;
    }

    private IEnumerator Countdown()
    {
        while (currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;
            UpdateCountdownText();
        }

        EndTime();
    }

    private void UpdateCountdownText()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        string timeString = string.Format("{0:00}:{1:00}", minutes, seconds);
        countdownText.text = "Time Left: " + timeString;
    }

    private void EndTime()
    {
        checklist.CheckScoreForReward();
    }
}