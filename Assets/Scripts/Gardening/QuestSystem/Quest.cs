using Gardening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
   public event Action OnQuestFinished;

   public QuestInfoSO questInfo;   //TODO make private
   private Queue<QuestStep> questStepsQueue = new Queue<QuestStep>();

   private QuestStep currentStep;

   public Quest(QuestInfoSO questInfo)
    {
        this.questInfo = questInfo;

        foreach (QuestStep step in questInfo.questSteps){
            questStepsQueue.Enqueue(step);
        }
    }

    public void StartQuest()
    {
        if(NextStepAvailable())
            AdvanceQuest();
    }

    private void AdvanceQuest()
    {
        currentStep = questStepsQueue.Dequeue();
        currentStep.StartQuestStep();
        currentStep.OnStepFinished += HandleStepFinished;
    }

    private void HandleStepFinished()
    {
        if (!NextStepAvailable())
        {
            OnQuestFinished?.Invoke();
            return;
        }

        currentStep.OnStepFinished -= HandleStepFinished;
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
