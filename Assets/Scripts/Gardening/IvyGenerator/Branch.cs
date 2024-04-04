using System.Collections.Generic;
using UnityEngine;

namespace Gardening
{
    public class Branch : MonoBehaviour
    {
        private const string AMOUNT = "_Amount";
        private const string RADIUS = "_Radius";
        private const float MAX = 0.5f;

        private List<IvyNode> _branchNodes;

        private Mesh _mesh;
        private Material _material;
        private MeshFilter _meshFilter;
        private MeshRenderer _meshRenderer;

        private Material _leafMaterial;
        private Material _flowerMaterial;
        private Blossom _leafPrefab;
        private Blossom _flowerPrefab;
        private bool _wantBlossoms;
        private Dictionary<int, Blossom> _blossoms;

        private float _branchRadius = 0.02f;
        private int _meshFaces = 8;

        private bool _animate;
        private float _growthSpeed = 2;
        private float _currentAmount = -1;

        public void Init(List<IvyNode> branchNodes, float branchRadius, Material material)
        {
            _branchNodes = branchNodes;
            _branchRadius = branchRadius;
            _material = new Material(material);
            _mesh = CreateMesh(branchNodes);
        }

        public void Init(List<IvyNode> branchNodes, float branchRadius, Material material, Material leafMaterial, Blossom leafPrefab, Material flowerMaterial, Blossom flowerPrefab, bool isFirst)
        {
            _branchNodes = branchNodes;
            _branchRadius = branchRadius;
            _material = new Material(material);
            _mesh = CreateMesh(branchNodes);

            _leafMaterial = leafMaterial;
            _flowerMaterial = flowerMaterial;
            _leafPrefab = leafPrefab;
            _flowerPrefab = flowerPrefab;
            _wantBlossoms = true;
            _blossoms = CreateBlossoms(branchNodes, isFirst);
        }

        private void Start()
        {
            _meshFilter = gameObject.AddComponent<MeshFilter>();
            _meshRenderer = gameObject.AddComponent<MeshRenderer>();
            if (_material == null)
            {
                _material = new Material(Shader.Find("Specular"));
            }

            _leafMaterial = _material;
            _meshRenderer.material = _material;
            if (_mesh != null)
            {
                _meshFilter.mesh = _mesh;
            }

            _material.SetFloat(RADIUS, _branchRadius);
            _material.SetFloat(AMOUNT, _currentAmount);
            _animate = true;
        }

        private void Update()
        {
            if (_animate)
            {
                _currentAmount += Time.deltaTime * _growthSpeed;
                _material.SetFloat(AMOUNT, _currentAmount);

                if (_wantBlossoms)
                {
                    var estimateNodeID = (int)Remap(_currentAmount, -.5f, .5f, 0, _branchNodes.Count - 1);

                    if (_blossoms.ContainsKey(estimateNodeID))
                    {
                        Blossom b = _blossoms[estimateNodeID];
                        if (!b.IsGrowing())
                        {
                            b.Grow(_growthSpeed);
                        }
                    }
                }

                if (_currentAmount < MAX) return;
                _animate = false;
                _material.SetFloat(AMOUNT, MAX);
                MeshManager.instance.AddMesh(transform, _meshFilter.mesh, _meshRenderer.sharedMaterial);
            }

        }

        private float Remap(float input, float oldLow, float oldHigh, float newLow, float newHigh)
        {
            float t = Mathf.InverseLerp(oldLow, oldHigh, input);
            return Mathf.Lerp(newLow, newHigh, t);
        }

        private Mesh CreateMesh(List<IvyNode> nodes)
        {
            Mesh branchMesh = new();

            Vector3[] vertices = new Vector3[(nodes.Count) * _meshFaces * 4];
            Vector3[] normals = new Vector3[nodes.Count * _meshFaces * 4];
            Vector2[] uv = new Vector2[nodes.Count * _meshFaces * 4];
            int[] triangles = new int[(nodes.Count - 1) * _meshFaces * 6];

            for (int i = 0; i < nodes.Count; i++)
            {
                float vStep = 2f * Mathf.PI / _meshFaces;

                var fw = Vector3.zero;
                if (i > 0)
                {
                    fw = _branchNodes[i - 1].GetPosition() - _branchNodes[i].GetPosition();
                }

                if (i < _branchNodes.Count - 1)
                {
                    fw += _branchNodes[i].GetPosition() - _branchNodes[i + 1].GetPosition();
                }

                if (fw == Vector3.zero)
                {
                    fw = Vector3.forward;
                }

                fw.Normalize();

                var up = _branchNodes[i].GetNormal();
                up.Normalize();

                for (int v = 0; v < _meshFaces; v++)
                {
                    var orientation = Quaternion.LookRotation(fw, up);
                    Vector3 xAxis = Vector3.up;
                    Vector3 yAxis = Vector3.right;
                    Vector3 pos = _branchNodes[i].GetPosition();
                    pos += orientation * xAxis * (_branchRadius * Mathf.Sin(v * vStep));
                    pos += orientation * yAxis * (_branchRadius * Mathf.Cos(v * vStep));

                    vertices[i * _meshFaces + v] = pos;

                    var diff = pos - _branchNodes[i].GetPosition();
                    normals[i * _meshFaces + v] = diff / diff.magnitude;

                    float uvID = Remap(i, 0, nodes.Count - 1, 0, 1);
                    uv[i * _meshFaces + v] = new Vector2((float)v / _meshFaces, uvID);
                }

                if (i + 1 >= nodes.Count) continue;
                for (int v = 0; v < _meshFaces; v++)
                {
                    triangles[i * _meshFaces * 6 + v * 6] = ((v + 1) % _meshFaces) + i * _meshFaces;
                    triangles[i * _meshFaces * 6 + v * 6 + 1] = triangles[i * _meshFaces * 6 + v * 6 + 4] = v + i * _meshFaces;
                    triangles[i * _meshFaces * 6 + v * 6 + 2] = triangles[i * _meshFaces * 6 + v * 6 + 3] = ((v + 1) % _meshFaces + _meshFaces) + i * _meshFaces;
                    triangles[i * _meshFaces * 6 + v * 6 + 5] = (_meshFaces + v % _meshFaces) + i * _meshFaces;
                }
            }

            branchMesh.vertices = vertices;
            branchMesh.triangles = triangles;
            branchMesh.normals = normals;
            branchMesh.uv = uv;
            return branchMesh;
        }

        private Dictionary<int, Blossom> CreateBlossoms(List<IvyNode> nodes, bool isFirst)
        {
            Dictionary<int, Blossom> bls = new();
            for (int i = 0; i < nodes.Count; i++)
            {

                var r = Random.Range(0, 10);

                if ((i <= 0 && !isFirst) || r <= 2) continue;

                Vector3 n = nodes[i].GetNormal();
                Vector3 otherNormal = Vector3.up;
                Vector3 fw = Vector3.forward;
                if (i > 0)
                {
                    fw = nodes[i - 1].GetPosition() - nodes[i].GetPosition();
                    otherNormal = nodes[i - 1].GetNormal();
                }
                else if (i < nodes.Count - 1)
                {
                    fw = nodes[i].GetPosition() - nodes[i + 1].GetPosition();
                    otherNormal = nodes[i + 1].GetNormal();
                }

                var isFlower = (r == 3) && Vector3.Dot(n, otherNormal) >= .95f;

                var prefab = _leafPrefab;
                if (isFlower)
                {
                    prefab = _flowerPrefab;
                }

                Quaternion rotation = Quaternion.LookRotation(fw.normalized, n);
          
                float flowerOffset = isFlower ? 0.02f : 0;
                //float uvID = Remap(i, 0, nodes.Count - 1, 0, 1);
                Blossom b = Instantiate(prefab, nodes[i].GetPosition() + nodes[i].GetNormal() * (_branchRadius + flowerOffset), rotation);
                b.Init(isFlower ? _flowerMaterial : _leafMaterial);
                b.transform.SetParent(transform);
                //b.transform.forward = n;
                MeshManager.instance.AddMesh(b.transform, b.GetComponent<MeshFilter>().mesh, b.GetComponent<MeshRenderer>().material);
                bls.Add(i, b);
            }
            return bls;
        }


        private void OnDrawGizmosSelected()
        {
            if (_branchNodes == null) return;
            for (int i = 0; i < _branchNodes.Count; i++)
            {
                Gizmos.DrawSphere(_branchNodes[i].GetPosition(), .002f);
                Gizmos.color = Color.white;

                Gizmos.color = Color.blue;

                var fw = Vector3.zero;
                if (i > 0)
                {
                    fw = _branchNodes[i - 1].GetPosition() - _branchNodes[i].GetPosition();
                }

                if (i < _branchNodes.Count - 1)
                {
                    fw += _branchNodes[i].GetPosition() - _branchNodes[i + 1].GetPosition();
                }

                fw.Normalize();

                var up = _branchNodes[i].GetNormal();
                up.Normalize();

                Vector3.OrthoNormalize(ref up, ref fw);

                float vStep = (2f * Mathf.PI) / _meshFaces;
                for (int v = 0; v < _meshFaces; v++)
                {

                    Gizmos.DrawLine(_branchNodes[i].GetPosition(), _branchNodes[i].GetPosition() + fw * .05f);

                    var orientation = Quaternion.LookRotation(fw, up);
                    Vector3 xAxis = Vector3.up;
                    Vector3 yAxis = Vector3.right;
                    Vector3 pos = _branchNodes[i].GetPosition();
                    pos += orientation * xAxis * (_branchRadius * Mathf.Sin(v * vStep));
                    pos += orientation * yAxis * (_branchRadius * Mathf.Cos(v * vStep));

                    Gizmos.color = new Color(
                        (float)v / _meshFaces,
                        (float)v / _meshFaces,
                        1f
                    );
                    Gizmos.DrawSphere(pos, .002f);
                }
            }
        }
    }
}
