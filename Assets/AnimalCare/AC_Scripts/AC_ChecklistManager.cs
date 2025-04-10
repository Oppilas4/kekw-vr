using UnityEngine;
using TMPro;

public class AC_ChecklistManager : MonoBehaviour
{
    public TextMeshProUGUI[] taskTexts;
    public GameObject objectToEnable;
    public AudioClip reward;
    public int[] taskPoints;
    public TextMeshProUGUI scoreText;

    public int rewardThreshold = 50; 

    private bool[] taskCompletion;
    private int totalScore = 0;
    private bool rewardGiven = false;

    void Start()
    {
        taskCompletion = new bool[taskTexts.Length];
        objectToEnable.SetActive(false);
        UpdateScoreDisplay();
    }

    public void CompleteTask(int taskIndex)
    {
        if (taskIndex >= 0 && taskIndex < taskTexts.Length && !taskCompletion[taskIndex])
        {
            taskCompletion[taskIndex] = true;
            taskTexts[taskIndex].color = Color.green;

            if (taskIndex < taskPoints.Length)
            {
                totalScore += taskPoints[taskIndex];
                UpdateScoreDisplay();
                CheckScoreForReward();
            }
        }
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = totalScore.ToString();
        }
    }

    private void CheckScoreForReward()
    {
        if (!rewardGiven && totalScore >= rewardThreshold)
        {
            objectToEnable.SetActive(true);
            PlayRewardAudio();
            rewardGiven = true;
        }
    }

    private void PlayRewardAudio()
    {
        if (reward != null)
        {
            AudioSource.PlayClipAtPoint(reward, transform.position);
        }
    }
}
