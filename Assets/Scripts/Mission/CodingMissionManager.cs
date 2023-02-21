using UnityEngine;

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
        [Tooltip("")]
        float _speed;

        [SerializeField]
        [Tooltip("Fill rate")]
        float _fillRate;

        private bool _isActive;


        private void Awake()
        {
            _material = _lineRenderer.materials[0];
        }

        private void Update()
        {
            if (_isActive)
            {
                // Mission is running
                if(_material.GetFloat("_OffsetX") >= 0.0f)
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
            _material.SetFloat("_TimeMultiplier", _speed);
            _isActive = true;
        }


        /// <summary>
        /// <seealso cref="IMissionManager"/>
        /// </summary>
        public void OnMissionStop()
        {
            if(_material.GetFloat("_OffsetX") > .01 && _material.GetFloat("_OffsetX") < .2f)
            {
                OnMissionSuccess();
            }
            else
            {
                OnMissionFail();
            }
            _isActive = false;
            _material.SetFloat("_OffsetX", 1.1f);
            _material.SetFloat("_TimeMultiplier", 0f);
        }

        /// <summary>
        /// <seealso cref="IMissionManager"/>
        /// </summary>
        public void OnMissionFail()
        {
            Debug.Log("Mission failed");
        }

        /// <summary>
        /// <seealso cref="IMissionManager"/>
        /// </summary>
        public void OnMissionSuccess()
        {
            Debug.Log("Mission succeess");
        }
    } 
}
