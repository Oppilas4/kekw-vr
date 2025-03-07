using UnityEngine;
using TMPro;

public class AC_ChecklistManager : MonoBehaviour
{
    public TextMeshProUGUI[] taskTexts;  // Assign task texts in the Inspector
    public GameObject objectToEnable;    // Object to enable when all tasks are completed
    private bool[] taskCompletion;       // Track which tasks are completed

    void Start()
    {
        taskCompletion = new bool[taskTexts.Length];
        objectToEnable.SetActive(false); // Ensure the object starts disabled
    }

    public void CompleteTask(int taskIndex)
    {
        if (taskIndex >= 0 && taskIndex < taskTexts.Length)
        {
            taskCompletion[taskIndex] = true;
            taskTexts[taskIndex].color = Color.green; // Change text color to green
            CheckAllTasksCompleted();
        }
    }

    private void CheckAllTasksCompleted()
    {
        foreach (bool task in taskCompletion)
        {
            if (!task) return; // If any task is false, return without enabling the object
        }
        objectToEnable.SetActive(true); // Enable the object when all tasks are complete
    }
}
