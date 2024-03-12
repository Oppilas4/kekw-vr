using System;
using System.Collections;
using UnityEngine;

namespace Gardening
{
    public class GroundFilling : MonoBehaviour
    {
        [SerializeField] private float fillRate, stepSize;
        [SerializeField] private Transform groundTransform;
        [SerializeField] private Transform lowerBound, upperBound;
        [SerializeField] private float startRadius, endRadius;
        [SerializeField] private bool isPotShapedCircle;
    
        private Vector3 _startGroundTransform;
        private float _potHeight;
        private float _radiusDiff;

        private float _direction = 1;

        private IEnumerator _currentCoroutine;

        private void Start()
        {
            _startGroundTransform = groundTransform.localScale;
            _potHeight = upperBound.localPosition.y - lowerBound.localPosition.y;
            _radiusDiff = endRadius - startRadius;
        }

        public void SetDirection(float direction) => _direction = direction;

        public void StartFill()
        {
            _currentCoroutine = FillCoroutine();
            StartCoroutine(_currentCoroutine);
        }
        public void StopFill() => StopCoroutine(_currentCoroutine);
        
        private IEnumerator FillCoroutine()
        {
            while (true)
            {
                switch (_direction)
                {
                    case > 0 when groundTransform.localPosition.y > upperBound.localPosition.y:
                    case < 0 when groundTransform.localPosition.y < lowerBound.localPosition.y:
                        yield break;
                }

                groundTransform.localPosition += Vector3.up * (stepSize * _direction);
            
                if (isPotShapedCircle)
                {
                    float currentHeight = groundTransform.localPosition.y - lowerBound.localPosition.y;
                    float scale = startRadius + _radiusDiff * currentHeight / _potHeight;
                    Vector3 newScale = _startGroundTransform * scale;
                    groundTransform.localScale = newScale;
                }
            
                yield return new WaitForSeconds(fillRate);
            }
        }
    }
}
