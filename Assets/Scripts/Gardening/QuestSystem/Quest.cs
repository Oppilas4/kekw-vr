using Gardening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
   public event Action OnQuestFinished;

   private bool _isStepInProgress;
   private QuestInfoSO questInfo;
   private Queue<QuestStep> questStepsQueue;

   private QuestStep currentStep;
   public Quest(QuestInfoSO questInfo)
    {
        this.questInfo = questInfo;
        foreach(QuestStep step in questInfo.questSteps){
            questStepsQueue.Enqueue(step);
        }
    }
    public void StartQuest()
    {
        AdvanceQuest();
    }
    private void AdvanceQuest()
    {
        if (_isStepInProgress)
            return;

        currentStep = questStepsQueue.Dequeue();
        currentStep.StartQuestStep();
        _isStepInProgress = true;
        currentStep.OnStepFinished += HandleFinishedStep;
    }
    private void HandleFinishedStep()
    {
        if (!NextStepAvailable())
        {
            OnQuestFinished?.Invoke();
        }

        currentStep.OnStepFinished -= HandleFinishedStep;
        currentStep.FinishQuestStep();
        _isStepInProgress = false;
        AdvanceQuest();
    }
    private bool NextStepAvailable()
    {
        if (questStepsQueue.Count >= 1)
            return true;
        else 
            return false;
    }
}
