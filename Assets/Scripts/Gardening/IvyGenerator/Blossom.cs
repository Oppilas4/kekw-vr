using System.Collections.Generic;
using UnityEngine;

namespace Gardening
{
    public class Blossom : MonoBehaviour
    {
        private const string AMOUNT = "_Amount";
        private const float MAX = 0.5f;

        private Material _material;

        private bool _animate;
        private float _growthSpeed = 2;
        private float _currentAmount = -1;

        public void Init(Material material)
        {
            _material = new Material(material);
        }

        private void Start()
        {
            GetComponent<MeshRenderer>().material = _material;

            _material.SetFloat(AMOUNT, _currentAmount);
            _animate = false;
        }

        public void Grow(float growthSpeed)
        {
            _growthSpeed = growthSpeed;
            _animate = true;
        }

        public bool IsGrowing()
        {
            return _animate || _currentAmount >= MAX;
        }

        private void Update()
        {
            if (!_animate) return;
            _currentAmount += Time.deltaTime * _growthSpeed;
            _material.SetFloat(AMOUNT, _currentAmount);
            if (_currentAmount < MAX) return;
            _animate = false;
            _material.SetFloat(AMOUNT, MAX);
        }
    }
}
