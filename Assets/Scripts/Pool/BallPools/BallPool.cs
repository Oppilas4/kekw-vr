using UnityEngine;

namespace Kekw.Pool.Ball
{
    /// <summary>
    /// Vuoksi hoop related pool.
    /// </summary>
    public class BallPool : APool
    {
        /// <summary>
        /// Secondary item to spawn
        /// </summary>
        [SerializeField]
        [Tooltip("Secondary item")]
        GameObject _secondaryPoolItem;

        /// <summary>
        /// How many balls to spawn in beginning.
        /// </summary>
        [SerializeField]
        [Tooltip("Number of items to queue at start")]
        int _numOfItemsAtStart;

        protected override void InitializePool()
        {
            // initialize pool
            for (int i = 0; i < _size; i++)
            {
                GameObject temp;
                if (i % 2 == 0)
                {
                    temp = Instantiate(_prefab);
                }
                else
                {
                    temp = Instantiate(_secondaryPoolItem);
                }
                
                temp.SetActive(false);
                temp.transform.position = this.transform.position;
                _pool.Enqueue(temp.GetComponent<APoolMember>());
            }
        }

        private void Start()
        {
            for (int i = 0; i < _numOfItemsAtStart; i++)
            {
                if(i < _size)
                {
                    APoolMember x = GetFromPool();
                    x.transform.position = this.transform.position;
                }
            }
        }
    }
}
