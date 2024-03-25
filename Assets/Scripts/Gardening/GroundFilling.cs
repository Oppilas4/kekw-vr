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
        }

        public void StartFillingAndCoroutine()
        {
            StartFilling();
            StartFillCoroutine();
        }
        
        public void StopFillingAndCoroutine()
        {
            StopFillCoroutine();
            StopFilling();
        }

        private void StartFillCoroutine()
        {
            _currentCoroutine = FillCoroutine();
            StartCoroutine(_currentCoroutine);
        }

        private void StopFillCoroutine()
        {
            StopCoroutine(_currentCoroutine);
        }

        private void StartFilling() => isCurrentlyFilling = true;
        private void StopFilling() => isCurrentlyFilling = false;
        
        private IEnumerator FillCoroutine()
        {
            while (true)
            {
                if (HasFillLimitBeenReached()) 
                { 
                    StopFilling();

                    if (IsReachedTopBoundary())
                    {
                        IsFilledUp(true);
                    } else
                    {
                        IsFilledUp(false);
                    }

                    yield break;
                }

                MoveDirt();

                if (isPotShapedCircle)
                {
                    AdjustRadius();
                }
            
                yield return new WaitForSeconds(fillRate);
            }
        }

        private bool HasFillLimitBeenReached()
        {
            bool hasTopLimitBeenReached = _direction > 0 && IsReachedTopBoundary();
            bool hadBottomLimitBeenReached = _direction < 0 && IsReachedBottomBoundary();

            return hasTopLimitBeenReached || hadBottomLimitBeenReached;
        }

        private bool IsReachedTopBoundary()
        {
            bool isReachedTopBoundary 
                = groundTransform.localPosition.y > upperBound.localPosition.y;

            return isReachedTopBoundary;
        }

        private bool IsReachedBottomBoundary()
        {
            bool isReachedBottomBoundary
                = groundTransform.localPosition.y < lowerBound.localPosition.y;

            return isReachedBottomBoundary;
        }

        private void IsFilledUp(bool isFilled) => this.isFilled = isFilled;

        private void MoveDirt()
        {
            groundTransform.localPosition += Vector3.up * (stepSize * _direction);
        }

        private void AdjustRadius()
        {
            float currentHeight = groundTransform.localPosition.y - lowerBound.localPosition.y;
            float scale = startRadius + _radiusDiff * currentHeight / _potHeight;
            Vector3 newScale = _startGroundTransform * scale;
            groundTransform.localScale = newScale;
        }

    }
}
