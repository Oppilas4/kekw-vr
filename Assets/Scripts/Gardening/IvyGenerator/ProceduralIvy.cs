using System.Collections.Generic;
using UnityEngine;

namespace Gardening
{
    public class ProceduralIvy : MonoBehaviour
    {

        //public Camera cam;
        //[Space]
        //public float recycleInterval = 30;
        [Space]
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
                // call this method when you are ready to group your meshes
                CombineAndClear();
            }

            //if (Input.GetMouseButtonDown(0))
            //{
            //    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            //    RaycastHit hit;
            //    if (Physics.Raycast(ray, out hit, 100))
            //    {
            //        CreateIvy(hit);
            //    }
            //}
        }

        private Vector3 FindTangentFromArbitraryNormal(Vector3 normal)
        {
            Vector3 t1 = Vector3.Cross(normal, Vector3.forward);
            Vector3 t2 = Vector3.Cross(normal, Vector3.up);
            if (t1.magnitude > t2.magnitude)
            {
                return t1;
            }
            return t2;
        }

        public void CreateIvy(RaycastHit hit)
        {
            Vector3 tangent = FindTangentFromArbitraryNormal(hit.normal);
            GameObject ivy = new("Ivy " + _ivyCount);
            ivy.transform.SetParent(transform);
            for (int i = 0; i < branches; i++)
            {
                Vector3 dir = Quaternion.AngleAxis(360 / branches * i + Random.Range(0, 360 / branches), hit.normal) * tangent;
                List<IvyNode> nodes = CreateBranch(maxPointsForBranch, hit.point, hit.normal, dir);
                GameObject branch = new("Branch " + i);
                Branch b = branch.AddComponent<Branch>();
                if (!wantBlossoms)
                {
                    b.Init(nodes, branchRadius, branchMaterial);
                }
                else
                {
                    b.Init(nodes, branchRadius, branchMaterial, leafMaterial, leafPrefab, flowerMaterial, flowerPrefab, i == 0);
                }
                branch.transform.SetParent(ivy.transform);
            }

            _ivyCount++;
        }

        private Vector3 CalculateTangent(Vector3 p0, Vector3 p1, Vector3 normal)
        {
            var heading = p1 - p0;
            var distance = heading.magnitude;
            var direction = heading / distance;
            return Vector3.Cross(normal, direction).normalized;
        }

        private Vector3 ApplyCorrection(Vector3 p, Vector3 normal)
        {
            return p + normal * 0.01f;
        }

        private bool IsOccluded(Vector3 from, Vector3 to)
        {
            Ray ray = new Ray(from, (to - from) / (to - from).magnitude);
            return Physics.Raycast(ray, (to - from).magnitude);
        }

        private bool IsOccluded(Vector3 from, Vector3 to, Vector3 normal)
        {
            return IsOccluded(ApplyCorrection(from, normal), ApplyCorrection(to, normal));
        }

        private Vector3 CalculateMiddlePoint(Vector3 p0, Vector3 p1, Vector3 normal)
        {
            Vector3 middle = (p0 + p1) / 2;
            var h = p0 - p1;
            var distance = h.magnitude;
            var dir = h / distance;
            return middle + normal * distance;
        }

        private List<IvyNode> CreateBranch(int count, Vector3 pos, Vector3 normal, Vector3 dir)
        {

            if (count == maxPointsForBranch)
            {
                IvyNode rootNode = new IvyNode(pos, normal);
                return new List<IvyNode> { rootNode }.join(CreateBranch(count - 1, pos, normal, dir));
            }
            else if (count < maxPointsForBranch && count > 0)
            {

                if (count % 2 == 0)
                {
                    dir = Quaternion.AngleAxis(Random.Range(-20.0f, 20.0f), normal) * dir;
                }

                RaycastHit hit;
                Ray ray = new(pos, normal);
                Vector3 p1 = pos + normal * segmentLength;

                if (Physics.Raycast(ray, out hit, segmentLength))
                {
                    p1 = hit.point;
                }
                ray = new Ray(p1, dir);

                if (Physics.Raycast(ray, out hit, segmentLength))
                {
                    Vector3 p2 = hit.point;
                    IvyNode p2Node = new(p2, -dir);
                    return new List<IvyNode> { p2Node }.join(CreateBranch(count - 1, p2, -dir, normal));
                }
                else
                {
                    Vector3 p2 = p1 + dir * segmentLength;
                    ray = new Ray(ApplyCorrection(p2, normal), -normal);
                    if (Physics.Raycast(ray, out hit, segmentLength))
                    {
                        Vector3 p3 = hit.point;
                        IvyNode p3Node = new(p3, normal);

                        if (IsOccluded(p3, pos, normal))
                        {
                            Vector3 middle = CalculateMiddlePoint(p3, pos, (normal + dir) / 2);

                            Vector3 m0 = (pos + middle) / 2;
                            Vector3 m1 = (p3 + middle) / 2;

                            IvyNode m0Node = new(m0, normal);
                            IvyNode m1Node = new(m1, normal);

                            return new List<IvyNode> { m0Node, m1Node, p3Node }.join(CreateBranch(count - 3, p3, normal, dir));
                        }

                        return new List<IvyNode> { p3Node }.join(CreateBranch(count - 1, p3, normal, dir));
                    }
                    else
                    {
                        Vector3 p3 = p2 - normal * segmentLength;
                        ray = new Ray(ApplyCorrection(p3, normal), -normal);

                        if (Physics.Raycast(ray, out hit, segmentLength))
                        {
                            Vector3 p4 = hit.point;
                            IvyNode p4Node = new(p4, normal);

                            if (IsOccluded(p4, pos, normal))
                            {
                                Vector3 middle = CalculateMiddlePoint(p4, pos, (normal + dir) / 2);
                                Vector3 m0 = (pos + middle) / 2;
                                Vector3 m1 = (p4 + middle) / 2;

                                IvyNode m0Node = new(m0, normal);
                                IvyNode m1Node = new(m1, normal);

                                return new List<IvyNode> { m0Node, m1Node, p4Node }.join(CreateBranch(count - 3, p4, normal, dir));
                            }

                            return new List<IvyNode> { p4Node }.join(CreateBranch(count - 1, p4, normal, dir));
                        }
                        else
                        {
                            Vector3 p4 = p3 - normal * segmentLength;
                            IvyNode p4Node = new IvyNode(p4, dir);

                            if (IsOccluded(p4, pos, normal))
                            {
                                Vector3 middle = CalculateMiddlePoint(p4, pos, (normal + dir) / 2);

                                Vector3 m0 = (pos + middle) / 2;
                                Vector3 m1 = (p4 + middle) / 2;

                                IvyNode m0Node = new(m0, dir);
                                IvyNode m1Node = new(m1, dir);

                                return new List<IvyNode> { m0Node, m1Node, p4Node }.join(CreateBranch(count - 3, p4, dir, -normal));
                            }
                            return new List<IvyNode> { p4Node }.join(CreateBranch(count - 1, p4, dir, -normal));
                        }
                    }
                }

            }
            return null;
        }

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
