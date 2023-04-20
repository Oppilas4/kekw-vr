using System;
using UnityEngine;

namespace Kekw.Interaction
{
    /// <summary>
    /// Fillable mug
    /// </summary>
    class Mug : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Mug filling mesh")]
        GameObject _mugFilling;

        [SerializeField]
        [Tooltip("Vatupassi gameobject")]
        Vatupassi _vatupassi;

        [SerializeField]
        [Tooltip("Filling speed")]
        float _speed = 1;

        [SerializeField]
        [Tooltip("Max fill scale (1-100)")]
        float _maxFillScale;

        private float _fillPercentage = 0f;
        bool _filling = false;

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

        public void StartFill() => _filling = true;

        public void StopFill() => _filling = false;
    }
}
