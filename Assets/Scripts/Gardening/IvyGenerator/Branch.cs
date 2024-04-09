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
        private bool _wantFlowers;
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
            _wantFlowers = true;
            _blossoms = CreateBlossoms(branchNodes, isFirst);
        }

        public void Init(List<IvyNode> branchNodes, float branchRadius, Material material, Material leafMaterial, Blossom leafPrefab, bool isFirst)
        {
            _branchNodes = branchNodes;
            _branchRadius = branchRadius;
            _material = new Material(material);
            _mesh = CreateMesh(branchNodes);

            _leafMaterial = leafMaterial;
            _leafPrefab = leafPrefab;
            _wantBlossoms = true;
            _wantFlowers = false;
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
            if (!_animate) return;
            _currentAmount += Time.deltaTime * _growthSpeed;
            _material.SetFloat(AMOUNT, _currentAmount);

            if (_wantBlossoms)
            {
                var estimateNodeID = (int)Remap(_currentAmount, -.5f, .5f, 0, _branchNodes.Count - 1);

                if (_blossoms.ContainsKey(estimateNodeID))
                {
                    Blossom blossom = _blossoms[estimateNodeID];
                    if (!blossom.IsGrowing())
                    {
                        blossom.Grow(_growthSpeed);
                    }
                }
            }

            if (_currentAmount < MAX) return;
            _animate = false;
            _material.SetFloat(AMOUNT, MAX);
            MeshManager.instance.AddMesh(transform, _meshFilter.mesh, _meshRenderer.material);
        }

        private float Remap(float input, float oldLow, float oldHigh, float newLow, float newHigh)
        {
            float t = Mathf.InverseLerp(oldLow, oldHigh, input);
            return Mathf.Lerp(newLow, newHigh, t);
        }

        private Mesh CreateMesh(List<IvyNode> nodes)
        {
            var sixMeshes = _meshFaces * 6;
            Mesh branchMesh = new();

            Vector3[] vertices = new Vector3[(nodes.Count) * _meshFaces * 4];
            Vector3[] normals = new Vector3[nodes.Count * _meshFaces * 4];
            Vector2[] uv = new Vector2[nodes.Count * _meshFaces * 4];
            int[] triangles = new int[(nodes.Count - 1) * sixMeshes];

            for (int index = 0; index < nodes.Count; index++)
            {
                float vStep = 2f * Mathf.PI / _meshFaces;

                var forward = Vector3.zero;
                if (index > 0)
                {
                    forward = _branchNodes[index - 1].GetPosition() - _branchNodes[index].GetPosition();
                }

                if (index < _branchNodes.Count - 1)
                {
                    forward += _branchNodes[index].GetPosition() - _branchNodes[index + 1].GetPosition();
                }

                if (forward == Vector3.zero)
                {
                    forward = Vector3.forward;
                }

                forward.Normalize();

                var up = _branchNodes[index].GetNormal();
                up.Normalize();

                for (int faceIndex = 0; faceIndex < _meshFaces; faceIndex++)
                {
                    var orientation = Quaternion.LookRotation(forward, up);
                    Vector3 xAxis = Vector3.up;
                    Vector3 yAxis = Vector3.right;
                    Vector3 pos = _branchNodes[index].GetPosition();
                    pos += orientation * xAxis * (_branchRadius * Mathf.Sin(faceIndex * vStep));
                    pos += orientation * yAxis * (_branchRadius * Mathf.Cos(faceIndex * vStep));

                    vertices[index * _meshFaces + faceIndex] = pos;

                    var diff = pos - _branchNodes[index].GetPosition();
                    normals[index * _meshFaces + faceIndex] = diff / diff.magnitude;

                    float uvID = Remap(index, 0, nodes.Count - 1, 0, 1);
                    uv[index * _meshFaces + faceIndex] = new Vector2((float)faceIndex / _meshFaces, uvID);
                }

                if (index + 1 >= nodes.Count) continue;
                for (int faceIndex = 0; faceIndex < _meshFaces; faceIndex++)
                {
                    var sixIndices = faceIndex * 6;
                    triangles[index * sixMeshes + sixIndices] = ((faceIndex + 1) % _meshFaces) + index * _meshFaces;
                    triangles[index * sixMeshes + sixIndices + 1] = triangles[(index * sixMeshes) + sixIndices + 4] = faceIndex + index * _meshFaces;
                    // What
                    triangles[index
                        * _meshFaces
                        * 6
                        + sixIndices
                        + 2] = triangles[index * sixMeshes + sixIndices + 3] = (faceIndex + 1) % _meshFaces + _meshFaces + (index * _meshFaces);
                    triangles[index * sixMeshes + sixIndices + 5] = _meshFaces + faceIndex % _meshFaces + (index * _meshFaces);
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
            Dictionary<int, Blossom> blossoms = new();
            for (int index = 0; index < nodes.Count; index++)
            {

                var randomValue = Random.Range(0, 10);

                if ((index <= 0 && !isFirst) || randomValue <= 2) continue;

                Vector3 normal = nodes[index].GetNormal();
                Vector3 otherNormal = Vector3.up;
                Vector3 forward = Vector3.forward;
                if (index > 0)
                {
                    forward = nodes[index - 1].GetPosition() - nodes[index].GetPosition();
                    otherNormal = nodes[index - 1].GetNormal();
                }
                else if (index < nodes.Count - 1)
                {
                    forward = nodes[index].GetPosition() - nodes[index + 1].GetPosition();
                    otherNormal = nodes[index + 1].GetNormal();
                }

                var isFlower = _wantFlowers && (randomValue == 3) && Vector3.Dot(normal, otherNormal) >= .95f;

                var prefab = isFlower ? _flowerPrefab : _leafPrefab;
                if (prefab == null)
                {
                    _wantBlossoms = false;
                    Debug.LogWarning("Flower or leaf prefabs are not set even though \"Want Blossoms\" is set. Defaulting to no blossoms");
                    return null;
                }

                Quaternion rotation = Quaternion.LookRotation(forward.normalized, normal);
          
                float blossomOffset = isFlower ? 0.02f : 0;
                Blossom blossom = Instantiate(prefab, nodes[index].GetPosition() + nodes[index].GetNormal() * (_branchRadius + blossomOffset), rotation);
                blossom.Init(isFlower ? _flowerMaterial : _leafMaterial);
                blossom.transform.SetParent(transform);

                // I guess this kind of works
                if (isFlower) blossom.transform.forward = normal;

                MeshManager.instance.AddMesh(blossom.transform, blossom.GetComponent<MeshFilter>().mesh, blossom.GetComponent<MeshRenderer>().material);
                blossoms.Add(index, blossom);
            }
            return blossoms;
        }


        private void OnDrawGizmosSelected()
        {
            if (_branchNodes == null) return;
            for (int index = 0; index < _branchNodes.Count; index++)
            {
                Gizmos.DrawSphere(_branchNodes[index].GetPosition(), .002f);
                Gizmos.color = Color.blue;

                var forward = Vector3.zero;
                if (index > 0)
                {
                    forward = _branchNodes[index - 1].GetPosition() - _branchNodes[index].GetPosition();
                }

                if (index < _branchNodes.Count - 1)
                {
                    forward += _branchNodes[index].GetPosition() - _branchNodes[index + 1].GetPosition();
                }

                forward.Normalize();

                var up = _branchNodes[index].GetNormal();
                up.Normalize();

                Vector3.OrthoNormalize(ref up, ref forward);

                float vStep = (2f * Mathf.PI) / _meshFaces;
                for (int faceIndex = 0; faceIndex < _meshFaces; faceIndex++)
                {

                    Gizmos.DrawLine(_branchNodes[index].GetPosition(), _branchNodes[index].GetPosition() + forward * .05f);

                    var orientation = Quaternion.LookRotation(forward, up);
                    Vector3 pos = _branchNodes[index].GetPosition();
                    pos += orientation * Vector3.up * (_branchRadius * Mathf.Sin(faceIndex * vStep));
                    pos += orientation * Vector3.right * (_branchRadius * Mathf.Cos(faceIndex * vStep));

                    Gizmos.color = new Color(
                        (float)faceIndex / _meshFaces,
                        (float)faceIndex / _meshFaces,
                        1f
                    );
                    Gizmos.DrawSphere(pos, .002f);
                }
            }
        }
    }
}
