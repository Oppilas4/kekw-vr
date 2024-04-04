using System.Collections.Generic;
using UnityEngine;

namespace Gardening
{
    public class ProceduralIvy : MonoBehaviour
    {
        public int branches = 3;
        public int maxPointsForBranch = 20;
        public float segmentLength = .002f;
        public float branchRadius = 0.02f;
        [Space]
        public Material branchMaterial;
        public Material leafMaterial;
        public Material flowerMaterial;
        [Space]
        public Blossom leafPrefab;
        public Blossom flowerPrefab;
        [Space]
        public bool wantBlossoms;

        private int _ivyCount = 0;

        private void Start()
        {
            GameObject[] spawners = GameObject.FindGameObjectsWithTag("IvySpawner");
            foreach (var spawner in spawners)
            {
                if (Physics.Raycast(spawner.transform.position, spawner.transform.forward, out RaycastHit hit, 10f))
                {
                    CreateIvy(hit);
                }
            }
            //CombineAndClear();
        }

        private void Update()
        {

            if (Input.GetKeyUp(KeyCode.Space))
            {
                CombineAndClear();
            }
        }

        private Vector3 FindTangentFromArbitraryNormal(Vector3 normal)
        {
            Vector3 t1 = Vector3.Cross(normal, Vector3.forward);
            Vector3 t2 = Vector3.Cross(normal, Vector3.up);
            return t1.magnitude > t2.magnitude ? t1 : t2;
        }

        public void CreateIvy(RaycastHit hit)
        {
            Vector3 tangent = FindTangentFromArbitraryNormal(hit.normal);
            GameObject ivy = new("Ivy " + _ivyCount);
            ivy.transform.SetParent(transform);
            for (int index = 0; index < branches; index++)
            {
                Vector3 dir = Quaternion.AngleAxis(360 / branches * index + Random.Range(0, 360 / branches), hit.normal) * tangent;
                List<IvyNode> nodes = CreateBranch(maxPointsForBranch, hit.point, hit.normal, dir);
                GameObject branch = new("Branch " + index);
                Branch branchScript = branch.AddComponent<Branch>();
                if (!wantBlossoms)
                {
                    branchScript.Init(nodes, branchRadius, branchMaterial);
                }
                else
                {
                    branchScript.Init(nodes, branchRadius, branchMaterial, leafMaterial, leafPrefab, flowerMaterial, flowerPrefab, index == 0);
                }
                branch.transform.SetParent(ivy.transform);
            }

            _ivyCount++;
        }

        private Vector3 ApplyCorrection(Vector3 point, Vector3 normal)
        {
            return point + normal * 0.01f;
        }

        private bool IsOccluded(Vector3 from, Vector3 to)
        {
            Ray ray = new(from, (to - from) / (to - from).magnitude);
            return Physics.Raycast(ray, (to - from).magnitude);
        }

        private bool IsOccluded(Vector3 from, Vector3 to, Vector3 normal)
        {
            return IsOccluded(ApplyCorrection(from, normal), ApplyCorrection(to, normal));
        }

        private Vector3 CalculateMiddlePoint(Vector3 firstPoint, Vector3 secondPoint, Vector3 normal)
        {
            Vector3 middle = (firstPoint + secondPoint) / 2;
            var distance = (firstPoint - secondPoint).magnitude;
            return middle + normal * distance;
        }

        private List<IvyNode> CreateBranch(int count, Vector3 pos, Vector3 normal, Vector3 dir)
        {

            if (count == maxPointsForBranch)
            {
                IvyNode rootNode = new(pos, normal);
                return new List<IvyNode> { rootNode }.join(CreateBranch(count - 1, pos, normal, dir));
            }
            if (count >= maxPointsForBranch || count <= 0) return null;

            if (count % 2 == 0)
            {
                dir = Quaternion.AngleAxis(Random.Range(-20.0f, 20.0f), normal) * dir;
            }

            RaycastHit hit;
            Ray ray = new(pos, normal);
            Vector3 firstPosition = pos + normal * segmentLength;

            if (Physics.Raycast(ray, out hit, segmentLength))
            {
                firstPosition = hit.point;
            }

            ray = new Ray(firstPosition, dir);
            if (Physics.Raycast(ray, out hit, segmentLength))
            {
                Vector3 secondPosition = hit.point;
                IvyNode secondNode = new(secondPosition, -dir);
                return new List<IvyNode> { secondNode }.join(CreateBranch(count - 1, secondPosition, -dir, normal));
            }
            else
            {
                Vector3 secondPosition = firstPosition + dir * segmentLength;
                ray = new Ray(ApplyCorrection(secondPosition, normal), -normal);
                if (Physics.Raycast(ray, out hit, segmentLength))
                {
                    Vector3 thirdPosition = hit.point;
                    IvyNode thirdNode = new(thirdPosition, normal);

                    if (!IsOccluded(thirdPosition, pos, normal))
                    {
                        return new List<IvyNode> { thirdNode }.join(CreateBranch(count - 1, thirdPosition, normal, dir));
                    }

                    var middleNodes = CalculateMiddleNodes(pos, thirdPosition, normal, dir);

                    return new List<IvyNode> { middleNodes.Item1, middleNodes.Item2, thirdNode }.join(CreateBranch(count - 3, thirdPosition, normal, dir));
                }
                else
                {
                    Vector3 thirdPosition = secondPosition - normal * segmentLength;
                    ray = new Ray(ApplyCorrection(thirdPosition, normal), -normal);

                    if (Physics.Raycast(ray, out hit, segmentLength))
                    {
                        Vector3 fourthPosition = hit.point;
                        IvyNode fourthNode = new(fourthPosition, normal);

                        if (!IsOccluded(fourthPosition, pos, normal))
                        {
                            return new List<IvyNode> { fourthNode }.join(CreateBranch(count - 1, fourthPosition, normal, dir));
                        }

                        var middleNodes = CalculateMiddleNodes(pos, fourthPosition, normal, dir);
                        return new List<IvyNode> { middleNodes.Item1, middleNodes.Item2, fourthNode }.join(CreateBranch(count - 3, fourthPosition, normal, dir));
                    }
                    else
                    {
                        Vector3 fourthPosition = thirdPosition - normal * segmentLength;
                        IvyNode fourthNode = new(fourthPosition, dir);

                        if (!IsOccluded(fourthPosition, pos, normal))
                        {
                            return new List<IvyNode> { fourthNode }.join(CreateBranch(count - 1, fourthPosition, dir, -normal));
                        }

                        var middleNodes = CalculateMiddleNodes(pos, fourthPosition, normal, dir);
                        return new List<IvyNode> { middleNodes.Item1, middleNodes.Item2, fourthNode }.join(CreateBranch(count - 3, fourthPosition, dir, -normal));
                    }
                }
            }
        }

        private System.Tuple<IvyNode, IvyNode> CalculateMiddleNodes(Vector3 position, Vector3 secondaryPosition, Vector3 normal, Vector3 direction)
        {
            Vector3 middle = CalculateMiddlePoint(secondaryPosition, position, (normal + direction) / 2);

            Vector3 smallerMiddle = (position + middle) / 2;
            Vector3 secondSmallerMiddle = (secondaryPosition + middle) / 2;

            IvyNode smallerMiddleNode = new(smallerMiddle, direction);
            IvyNode secondSmallerMiddleNode = new(secondSmallerMiddle, direction);
            return new System.Tuple<IvyNode, IvyNode>(smallerMiddleNode, secondSmallerMiddleNode);
        }

        // Group meshes to improve performance
        private void CombineAndClear()
        {
            MeshManager.instance.CombineAll();
            foreach (Transform t in transform)
            {
                Destroy(t.gameObject);
            }
        }
    }
}
