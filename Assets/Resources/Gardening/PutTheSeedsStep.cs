using Gardening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutTheSeedsStep : QuestStep
{
    public override void StartQuestStep()
    {
        base.StartQuestStep();
        PlantManager.OnSeedWasPlanted += Evaluate;
    }
    protected override void Evaluate()
    {
        FinishQuestStep();
    }
    protected override void FinishQuestStep()
    {
        PlantManager.OnSeedWasPlanted -= FinishQuestStep;
        base.FinishQuestStep();
    }
}
