using UnityEngine;
using System;
using System.Collections;

namespace Kekw.Mission
{
    /// <summary>
    /// CoDing mission base class contains required components.
    /// </summary>
    public class CodingMissionManager : MonoBehaviour, IMissionManager
    {
        [SerializeField]
        [Tooltip("Material")]
        LineRenderer _lineRenderer;

        Material _material;

        [SerializeField]
        [Tooltip("Fill speed")]
        float _speed;

        [SerializeField]
        [Tooltip("Fill rate")]
        float _fillRate;

        [SerializeField]
        [Tooltip("Success texture")]
        Texture _successTexture;

        [SerializeField]
        [Tooltip("Fail texture")]
        Texture _FailTexture;

        [SerializeField]
        [Tooltip("Game running texture")]
        Texture _IgTexture;

        [SerializeField]
        [Tooltip("Standby texture")]
        Texture _OriginalTexture;

        [SerializeField]
        [Tooltip("Fail and success target texture display")]
        GameObject _targetDisplay;
        MeshRenderer _targetDisplayMeshRenderer;

        private bool _isActive;

        Coroutine _originalTextureSetter;

        private void Awake()
        {
            // Get reference to material/shader.
            _material = _lineRenderer.materials[0];
            _targetDisplayMeshRenderer = _targetDisplay.GetComponent<MeshRenderer>();
            _isActive = false;
        }

        private void Update()
        {
            if (_isActive)
            {
                // Shader _OffsetX aka fill value is greater than 0. 
                if (_material.GetFloat("_OffsetX") >= 0.0f)
                {
                    _material.SetFloat("_OffsetX", _material.GetFloat("_OffsetX") - Time.deltaTime * _fillRate);
                }
                else
                {
                    OnMissionStop();
                }
            }
        }

        /// <summary>
        /// <seealso cref="IMissionManager"/>
        /// </summary>
        public void OnMissionStart()
        {
            if (!_isActive)
            {
                if (_originalTextureSetter != null) StopCoroutine(_originalTextureSetter);
                _material.SetFloat("_TimeMultiplier", _speed);
                _isActive = true;
                SetDisplayTexture(_IgTexture);
            }
        }

        /// <summary>
        /// <seealso cref="IMissionManager"/>
        /// </summary>
        public void OnMissionStop()
        {
            if (_isActive)
            {
                _isActive = false;
                // if shader _OffsetX is in last 20% mission is success
                if (_material.GetFloat("_OffsetX") > .01 && _material.GetFloat("_OffsetX") < .2f)
                {
                    OnMissionSuccess();
                }
                else
                {
                    OnMissionFail();
                }
                _material.SetFloat("_OffsetX", 1.1f);
                _material.SetFloat("_TimeMultiplier", 0f);
            }
        }

        /// <summary>
        /// <seealso cref="IMissionManager"/>
        /// </summary>
        public void OnMissionFail()
        {
            SetDisplayTexture(_FailTexture);
            _originalTextureSetter = StartCoroutine(SetOriginalTexture());
        }

        /// <summary>
        /// <seealso cref="IMissionManager"/>
        /// </summary>
        public void OnMissionSuccess()
        {
            SetDisplayTexture(_successTexture);
            _originalTextureSetter = StartCoroutine(SetOriginalTexture());
        }

        IEnumerator SetOriginalTexture()
        {
            yield return new WaitForSeconds(5);
            SetDisplayTexture(_OriginalTexture);
        }

        /// <summary>
        /// Set display texture.
        /// </summary>
        /// <param name="texture">Texture to set</param>
        private void SetDisplayTexture(Texture texture)
        {
            _targetDisplayMeshRenderer.material.mainTexture = texture;
            _targetDisplayMeshRenderer.material.SetTexture("_EmissionMap", texture);
        }
    }
}
