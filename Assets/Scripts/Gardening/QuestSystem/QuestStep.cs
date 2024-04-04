using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gardening
{
    /// <summary>
    /// Abstract Class to creat Quest Steps
    /// </summary>
    public abstract class QuestStep : MonoBehaviour
    {
        public event Action OnStepFinished;
        protected bool _isFinished;
        public virtual void StartQuestStep()
        {
            //Add logic to follow the progress of the quest
            _isFinished = false;
        }
        protected virtual void Evaluate()
        {
            //Add logic that is responsible for cheking if the quest was complited
        }

        protected virtual void FinishQuestStep()
        {
            if (!_isFinished)
            {
                _isFinished = true;
                Debug.Log("Quest Step is finished " + this.name);
                OnStepFinished?.Invoke();
            }
            //Remove dependencies
        }
    }
}
