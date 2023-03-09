using System;
using System.Collections;
using UnityEngine;
using Kekw.Common;

namespace Kekw.VuoksiBotti
{
    /// <summary>
    /// VUoksi botti animation manager.
    /// </summary>
    class AnimationManager : MonoBehaviour, IPause
    {
        /// <summary>
        /// Helper struct to identify animation layers
        /// </summary>
        [Serializable]
        internal struct LayerData{
            public int indexBegin;
            public int indexEnd;
        }


        [SerializeField]
        [Tooltip("Animator to control")]
        Animator _animator;

        [SerializeField]
        [Tooltip("Base animation layers index range")]
        LayerData _baseLayerRange;

        [SerializeField]
        [Tooltip("Random dance move layer index range")]
        LayerData _danceLayerRange;

        [SerializeField]
        [Tooltip("Talking layer index range")]
        LayerData _talkingLayerRange;

        [SerializeField]
        [Tooltip("Random dance change %")]
        int _randomDanceChance = 30;

        [SerializeField]
        [Tooltip("Random dance time in seconds")]
        Vector2Int _danceTimeDelta;

        /// <summary>
        /// Is character talking to player
        /// </summary>
        public bool IsTalking { get; set; } = false;


        Coroutine _activeTimer;
        bool _isPLayingRandomDance = false;
        bool _isPaused = false;

        private void Start()
        {
            // Reset all additional layers to 0 except base.
            this.SetLayerWeights(_danceLayerRange, 0);
            this.SetLayerWeights(_talkingLayerRange, 0);
        }

        private void Update()
        {
            if (!_isPaused)
            {
                // Randomize character animation if not talking.
                if (!IsTalking)
                {
                    if (!_isPLayingRandomDance)
                    {
                        _isPLayingRandomDance = true;
                        if (_activeTimer != null) StopCoroutine(_activeTimer);
                        // Chance to play random dance default 30%
                        if (UnityEngine.Random.Range(1, 101) <= _randomDanceChance)
                        {
                            this.SetRandomDanceWeight();
                            _activeTimer = StartCoroutine(ResetDance(UnityEngine.Random.Range(_danceTimeDelta.x, _danceTimeDelta.y)));
                        }
                        else
                        {
                            this.SetLayerWeights(_danceLayerRange, 0);
                            _activeTimer = StartCoroutine(ResetDance(UnityEngine.Random.Range(_danceTimeDelta.x, _danceTimeDelta.y)));
                        }
                    }
                }
                else
                {
                    this.SetTalkMode();
                }
            }
        }

        /// <summary>
        /// <seealso cref="IPause"/>
        /// </summary>
        public void SetPause()
        {
            if (!_isPaused)
            {
                if (_activeTimer != null)
                {
                    StopCoroutine(_activeTimer);
                    _activeTimer = null;
                }
                // Stop all but base swing animation.
                this.SetLayerWeights(_danceLayerRange, 0);
                this.SetLayerWeights(_talkingLayerRange, 0);
                this.SetLayerWeights(_baseLayerRange, 0);
                _isPLayingRandomDance = false;
                _isPaused = true;
            }
            else
            {
                UnPause();
            }
        }

        /// <summary>
        /// <seealso cref="IPause"/>
        /// </summary>
        public void UnPause()
        {
            if (_isPaused)
            {
                // Continue with base animations.
                this.SetLayerWeights(_baseLayerRange, 1);
                _isPaused = false;
            }
        }

        /// <summary>
        /// Set character to talking mode
        /// </summary>
        private void SetTalkMode()
        {
            this.SetLayerWeights(_talkingLayerRange, 1);
        }

        /// <summary>
        /// Sets random dance layer to weight of 1. Character picks up random dance animation.
        /// </summary>
        private void SetRandomDanceWeight()
        {
            this.SetLayerWeights(_danceLayerRange, 0);
            int danceIndex = UnityEngine.Random.Range(_danceLayerRange.indexBegin, _danceLayerRange.indexEnd + 1);
            _animator.SetLayerWeight(danceIndex, 1);
        }

        /// <summary>
        /// Set weight all layers in range
        /// </summary>
        /// <param name="layers"></param>
        /// <param name="weight"></param>
        private void SetLayerWeights(LayerData layers ,int weight)
        {
            for (int i = layers.indexBegin; i <= layers.indexEnd; i++)
            {
                _animator.SetLayerWeight(i, weight);
            }
        }

        /// <summary>
        /// Coroutine to reset dance back to base after "time" seconds;
        /// </summary>
        /// <param name="time">Seconds before reset</param>
        /// <returns></returns>
        IEnumerator ResetDance(int time)
        {
            yield return new WaitForSeconds(time);
            this.SetLayerWeights(_danceLayerRange, 0);
            _isPLayingRandomDance = false;
        }

    }
}
