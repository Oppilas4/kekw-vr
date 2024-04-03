using System.Collections.Generic;
using UnityEngine;

namespace Gardening
{
    public class Blossom : MonoBehaviour
    {
        const string AMOUNT = "_Amount";
        const float MAX = 0.5f;

        private Material _material;
        private List<MeshRenderer> _renderers;

        private bool _animate;
        private float _growthSpeed = 2;
        private float _currentAmount = -1;

        public void Init(Material material)
        {
            _material = new Material(material);
        }

        private void Start()
        {
            _renderers = new List<MeshRenderer>();
            foreach (Transform t in transform)
            {
                MeshRenderer r = t.GetComponent<MeshRenderer>();
                _renderers.Add(r);
                r.material = _material;
            }

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
            if (_animate)
            {
                _currentAmount += Time.deltaTime * _growthSpeed;
                _material.SetFloat(AMOUNT, _currentAmount);
                if (_currentAmount >= MAX)
                {
                    _animate = false;
                    _material.SetFloat(AMOUNT, MAX);
                    foreach (var r in _renderers)
                    {
                        MeshManager.instance.AddMesh(r.transform, r.GetComponent<MeshFilter>().mesh, r.GetComponent<MeshRenderer>().sharedMaterial);
                    }
                }
            }
        }
    }
}
