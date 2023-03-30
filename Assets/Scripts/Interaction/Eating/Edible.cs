using Kekw.Manager;
using System;
using UnityEngine;

namespace Kekw.Interaction
{
    /// <summary>
    /// Eatable item uses this component
    /// </summary>
    class Edible: MonoBehaviour, ISpawnAble
    {
        [SerializeField]
        [Tooltip("meshes for different eating stages [0-n]. 0 Is full mesh, n is final eating bit.")]
        GameObject[] _eatingStages;

        int currentState = 0;

        SimpleSpawn _spawner;

        /// <summary>
        /// "eats chunk" from food. Enables disables child meshes.
        /// </summary>
        public void EatChunk()
        {
            if(currentState + 1 >= _eatingStages.Length)
            {
                _spawner.Spawn();
                Destroy(this.transform.parent.gameObject);
            }
            else
            {
                currentState++;
                for (int i = 0; i < _eatingStages.Length; i++)
                {
                    if (i == currentState)
                    {
                        _eatingStages[i].SetActive(true);
                    }
                    else
                    {
                        _eatingStages[i].SetActive(false);
                    }
                }
            }
        }

        public void SetSpawner(SimpleSpawn simpleSpawn)
        {
            _spawner = simpleSpawn;
        }
    }
}
