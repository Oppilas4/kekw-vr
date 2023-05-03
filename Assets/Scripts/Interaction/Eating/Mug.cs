using UnityEngine;
using Kekw.Manager;

namespace Kekw.Interaction
{
    /// <summary>
    /// Coffee mug. Can be filled and emptied.
    /// </summary>
    class Mug : MonoBehaviour, ISpawnAble, IDestroyable
    {
        /// <summary>
        /// Game object that represents liquid inside mug.
        /// </summary>
        [SerializeField]
        [Tooltip("Mug filling mesh")]
        GameObject _mugFilling;

        /// <summary>
        /// <seealso cref="Vatupassi"/>
        /// </summary>
        [SerializeField]
        [Tooltip("Vatupassi gameobject")]
        Vatupassi _vatupassi;

        /// <summary>
        /// How fast the mug fills.
        /// </summary>
        [SerializeField]
        [Tooltip("Filling speed")]
        float _speed = 1;

        /// <summary>
        /// Scale for filling, when gameobject is considered to be full.
        /// </summary>
        [SerializeField]
        [Tooltip("Max fill scale (1-100)")]
        float _maxFillScale;

        private float _fillPercentage = 0f;
        bool _filling = false;

        private SimpleSpawn _spawner;

        private void Awake()
        {
            _vatupassi.enabled = false;
            AdjustFillMesh(true);
        }

        private void Update()
        {
            // When filling
            if(_filling && _fillPercentage <= _maxFillScale)
            {
                if (!_vatupassi.enabled) _vatupassi.enabled = true;
                _fillPercentage += Time.deltaTime * _speed;
                AdjustFillMesh();
            }

            // When pouring
            if(_vatupassi.enabled && !_filling && _vatupassi.IsPouring)
            {
                _fillPercentage -= Time.deltaTime * _speed;
                AdjustFillMesh();
            }

            // when empty
            if(_fillPercentage <= 0f)
            {
                AdjustFillMesh(true);
                _fillPercentage = 0f;
                _vatupassi.IsPouring = false;
                _vatupassi.DestroyTrackedVFX();
                _vatupassi.enabled = false;
            }
        }

        /// <summary>
        /// Adjust mesh to match fill amount
        /// </summary>
        /// <param name="reset"></param>
        private void AdjustFillMesh(bool reset = false)
        {
            if (reset)
            {
                _mugFilling.transform.localScale = new Vector3(_maxFillScale * .01f, _maxFillScale * .01f, 0f);
                return;
            }
            _mugFilling.transform.localScale = new Vector3(_maxFillScale * .01f, _maxFillScale * .01f, _fillPercentage * .01f);
        }

        /// <summary>
        /// Mug knows to start filling it self.
        /// </summary>
        public void StartFill() => _filling = true;

        /// <summary>
        /// Mug should stop filling it self.
        /// </summary>
        public void StopFill() => _filling = false;

        /// <summary>
        /// <seealso cref="ISpawnAble"/>
        /// </summary>
        /// <param name="simpleSpawn"></param>
        public void SetSpawner(SimpleSpawn simpleSpawn) => _spawner = simpleSpawn;


        /// <summary>
        /// <seealso cref="IDestroyable"/>
        /// </summary>
        public void OnDestroyRequested()
        {
            if (_spawner != null)
            {
                _spawner.Spawn();
                Destroy(this.transform.parent.gameObject);
            }
        }
    }
}
