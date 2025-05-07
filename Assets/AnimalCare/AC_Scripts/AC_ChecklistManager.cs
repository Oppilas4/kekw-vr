using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class AC_ChecklistManager : MonoBehaviour
{
    public TextMeshProUGUI[] taskTexts;
    public GameObject objectToEnable;
    public int[] taskPoints;
    public TextMeshProUGUI scoreText;
    public int rewardThreshold = 300;

    private bool[] taskCompletion;
    private int totalScore = 0;
    private bool rewardGiven = false;

    private List<int> validTaskIndices = new List<int>(); // Only tasks requested by customer

    void Start()
    {
        taskCompletion = new bool[taskTexts.Length];
        objectToEnable.SetActive(false);
        UpdateScoreDisplay();
    }

    public void SetValidTasks(List<int> taskIndices)
    {
        validTaskIndices = taskIndices;
        ResetChecklist();
    }

    public void CompleteTask(int taskIndex)
    {
        if (taskIndex >= 0 && taskIndex < taskTexts.Length && !taskCompletion[taskIndex])
        {
            if (!validTaskIndices.Contains(taskIndex))
            {
                Debug.LogWarning("Trying to complete a task not requested by customer.");
                return;
            }

            taskCompletion[taskIndex] = true;
            taskTexts[taskIndex].color = Color.green;

            if (taskIndex < taskPoints.Length)
            {
                totalScore += taskPoints[taskIndex];
                UpdateScoreDisplay();
            }
        }
    }

    private void ResetChecklist()
    {
        for (int i = 0; i < taskCompletion.Length; i++)
        {
            taskCompletion[i] = false;
            taskTexts[i].color = Color.black;
        }
    }

    private void UpdateScoreDisplay()
    {
        if (scoreText != null)
        {
            scoreText.text = totalScore.ToString();
        }
    }

    public void CheckScoreForReward()
    {
        if (!rewardGiven)
        {
            if(totalScore >= rewardThreshold)
            {
                objectToEnable.SetActive(true);
                rewardGiven = true;
            }
        }
    }
}
