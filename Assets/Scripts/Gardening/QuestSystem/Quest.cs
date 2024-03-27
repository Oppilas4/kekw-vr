using Gardening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    private QuestInfoSO questInfo;
   public Quest(QuestInfoSO questInfo)
    {
        this.questInfo = questInfo;
    }
}
