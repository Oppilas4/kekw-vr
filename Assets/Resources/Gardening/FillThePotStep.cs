using Gardening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillThePotStep : QuestStep
{
    private bool _isPotFilled = false;
    private void OnEnable()
    {
        PlantManager.OnPotWasFilled += delegate { _isPotFilled = true; FinishQuestStep(); };
    }
}
