using System;
using System.Collections;
using UnityEngine;

namespace Gardening
{
    public class GroundFilling : MonoBehaviour
    {
        public bool isFilled = false;
        public bool isCurrentlyFilling = false;

        [SerializeField] private float fillRate, stepSize;
        [SerializeField] private Transform groundTransform;
        [SerializeField] private Transform lowerBound, upperBound;
        [SerializeField] private float startRadius, endRadius;

        [Tooltip("Will adjust its scale to pot's scale if toggled ")]
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

        /// <summary>
        /// Set mode to "Fill" or "Take out"
        /// </summary>
        /// <param name="direction"></param>
        public void SetFillingMode(GroundFillingMode direction)
        {
            _direction = direction switch
            {
                GroundFillingMode.Fill => 1,
                GroundFillingMode.TakeOut => -1,
                _ => throw new NotSupportedException()
            };
            //switch(direction)
            //{
            //    case GroundFillingMode.Fill:
            //        _direction = 1f;
            //        break;
            //    case GroundFillingMode.TakeOut:
            //        _direction = -1f;
            //        break;
            //}
        }

        public void StartFill()
        {
            _currentCoroutine = FillCoroutine();
            StartCoroutine(_currentCoroutine);
        }
        public void StopFill()
        {
            StopCoroutine(_currentCoroutine);
            isCurrentlyFilling = false;
        }
        
        private IEnumerator FillCoroutine()
        {
            isCurrentlyFilling = true;
            while (true)
            {
                switch (_direction)
                {
                    case > 0 when groundTransform.localPosition.y > upperBound.localPosition.y:
                    case < 0 when groundTransform.localPosition.y < lowerBound.localPosition.y:
                    {
                        isFilled = true;
                        isCurrentlyFilling = false;
                        yield break;
                    }
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
