using Gardening;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Queue<Quest> _quests = new Queue<Quest>();
    private int _currentQuestIndex;
    private Quest _currentQuest;
    private bool _isQuestPlaying;
    private void Start()
    {
        Initialize();
    }
    private void Initialize()
    {
        QuestInfoSO[] QuestInfoObjects = Resources.LoadAll<QuestInfoSO>("Gardening");  //Loading scriptable objects data from "Resources" folder
        Dictionary<int, Quest> unsortedQuests = new Dictionary<int, Quest>();  //Dictionary is used to help the checking for the dublicates

        for (int i = 0; i < QuestInfoObjects.Length; i++)
        {
            if (unsortedQuests.ContainsKey(QuestInfoObjects[i].id))
            {
                Debug.LogWarning("The QuestInfo with the ID" 
                    + QuestInfoObjects[i].id + " already exists");
                continue;
            }
            unsortedQuests.Add(QuestInfoObjects[i].id, new Quest(QuestInfoObjects[i]));  //creaitng quest objects
        }

        var sortedQuests = unsortedQuests.OrderByDescending(kvp => kvp.Key);   // sorting quests by their ID
        foreach(KeyValuePair<int, Quest> kvp in  sortedQuests)  // Converting Dictionary to the List
        {
            _quests.Enqueue(kvp.Value);
        }

        foreach(Quest quest in _quests)
        {
            Debug.Log(quest.questInfo.id);
        }

        _currentQuestIndex = -1;
        if (NextQuestAvailable())
            PlayNextQuest();

    }
    private void PlayNextQuest()
    {
        if (!NextQuestAvailable())
        {
            Debug.Log("All quests are finished!");
            return;
        }

        _currentQuestIndex++;
        _currentQuest = _quests.Dequeue();
        _currentQuest.OnQuestFinished += HandleQuestFinished;
        _currentQuest.StartQuest();
    }
    private void HandleQuestFinished()
    {
        _currentQuest.OnQuestFinished -= HandleQuestFinished;
        PlayNextQuest();
    }
    private bool NextQuestAvailable()
    {
        if(_quests.Count >= 1)
            return true;
        else 
            return false;
    }
}
