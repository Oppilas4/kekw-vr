using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gardening
{
    [CreateAssetMenu(fileName = "QuestInfoSO", menuName = "Quest System/QuestInfoSO", order = 1)]
    public class QuestInfoSO : ScriptableObject
    {
        [field: SerializeField] public int id {  get; private set; }
        [Header("General")]
        public string QuestDescription;

        [Header("Requirements")]
        public QuestInfoSO[] prerequisiteQuests;

        [Header("Steps")]
        public QuestStep[] questSteps;

        // [Header("Rewards")]

    }
}
