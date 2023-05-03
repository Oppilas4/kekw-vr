using Kekw.Manager;
using UnityEngine;

namespace Kekw.Interaction
{
    /// <summary>
    /// Eatable item uses this component.<br></br><br></br>
    /// Eatabe items consists of multiple meshes some are full and some are "eaten".
    /// Component toggles between these meshes and finally destroys object if meshes end.
    /// Object is also destroyed if it hits the floor tag collider.
    /// </summary>
    class Edible: MonoBehaviour, ISpawnAble
    {
        /// <summary>
        /// Array of eating stage meshes. 0 element is full mesh and n is final bit.
        /// </summary>
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
        
        /// <summary>
        /// Destroy food when it hits floor.
        /// </summary>
        public void DestroyOnFloor()
        {
            _spawner.Spawn();
            Destroy(this.transform.parent.gameObject);
        }

        /// <summary>
        /// <seealso cref="ISpawnAble"/>
        /// </summary>
        /// <param name="simpleSpawn"></param>
        public void SetSpawner(SimpleSpawn simpleSpawn)
        {
            _spawner = simpleSpawn;
        }
    }
}
