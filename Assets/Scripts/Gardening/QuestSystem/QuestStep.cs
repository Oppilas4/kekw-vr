using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gardening
{
    public abstract class QuestStep : MonoBehaviour
    {
        private bool isFinished;
        public void FinishQuestStep()
        {
            if (!isFinished)
            {
                isFinished = true;
                Debug.Log("Quest Step is finished");

                // TO_DO  Add method to advance the quest to the next step
                Destroy(gameObject);
            }
        }
    }
}
