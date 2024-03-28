using Gardening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<int, Quest> _quests;
    private int _currentQuestIndex;
    private int _currentQuest;
    private bool _isQuestPlaying;
    private void Initialize()
    {
        QuestInfoSO[] allQuests = Resources.LoadAll<QuestInfoSO>("Gardening");

        for (int i = 0; i < allQuests.Length; i++)
        {
            if (_quests.ContainsKey(allQuests[i].id))
                continue;
            _quests.Add(allQuests[i].id, new Quest(allQuests[i]));  //creaitng quest objects
        }
        var sortedQuests = _quests.OrderByDescending(kvp => kvp.Key);
        _quests = sortedQuests.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        foreach (var kvp in _quests)
        {
            Debug.Log(kvp.Key + " " + kvp.Value);
        }
        _currentQuestIndex = 0;
        PlayNextQuest();

    }
    private void PlayNextQuest()
    {
        if (_currentQuestIndex >= _quests.Count())
        {
            Debug.Log("No more quests! ");
            return;
        }
        _currentQuestIndex++;
    }
}
