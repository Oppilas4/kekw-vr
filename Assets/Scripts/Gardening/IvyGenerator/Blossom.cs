using System.Collections.Generic;
using UnityEngine;

namespace Gardening
{
    public class Blossom : MonoBehaviour
    {
        const string AMOUNT = "_Amount";
        const float MAX = 0.5f;

        Material material;
        List<MeshRenderer> renderers;

        bool animate;
        float growthSpeed = 2;
        float currentAmount = -1;

        public void Init(Material material)
        {
            this.material = new Material(material);
        }

        private void Start()
        {
            renderers = new List<MeshRenderer>();
            foreach (Transform t in transform)
            {
                MeshRenderer r = t.GetComponent<MeshRenderer>();
                renderers.Add(r);
                r.material = material;
            }

            material.SetFloat(AMOUNT, currentAmount);
            animate = false;
        }

        public void Grow(float growthSpeed)
        {
            this.growthSpeed = growthSpeed;
            animate = true;
        }

        public bool IsGrowing()
        {
            return animate || currentAmount >= MAX;
        }

        private void Update()
        {
            if (animate)
            {
                currentAmount += Time.deltaTime * growthSpeed;
                material.SetFloat(AMOUNT, currentAmount);
                if (currentAmount >= MAX)
                {
                    animate = false;
                    material.SetFloat(AMOUNT, MAX);
                    foreach (var r in renderers)
                    {
                        MeshManager.instance.AddMesh(r.transform, r.GetComponent<MeshFilter>().mesh, r.GetComponent<MeshRenderer>().sharedMaterial);
                    }
                }
            }
        }
    }
}
