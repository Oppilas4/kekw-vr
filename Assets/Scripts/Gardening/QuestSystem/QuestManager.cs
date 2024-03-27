using Gardening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private List<Quest> _quests;
    private void Awake()
    {
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Gardening");
    }
}
