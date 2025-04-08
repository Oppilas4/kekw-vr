using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class AC_VacuumWire : MonoBehaviour
{
    public Transform vacuumBase;
    public Transform vacuumHead;

    public GameObject wireSegmentPrefab;
    public int segmentCount = 20;
    public float segmentSpacing = 0.2f;

    public int radialSegments = 6; // for cylinder smoothness
    public float wireRadius = 0.03f;

    private List<Transform> segments = new List<Transform>();
    private Mesh mesh;

    void Start()
    {
        if (!vacuumBase || !vacuumHead || !wireSegmentPrefab)
        {
            Debug.LogError("Missing references. Please assign vacuumBase, vacuumHead, and wireSegmentPrefab.");
            return;
        }

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Vacuum Wire Mesh";

        GenerateWire();
    }

    void GenerateWire()
    {
        Vector3 start = vacuumBase.position;
        Vector3 end = vacuumHead.position;
        float totalLength = Vector3.Distance(start, end);
        float spacing = totalLength / (segmentCount - 1);
        Rigidbody previousRb = null;

        for (int i = 0; i < segmentCount; i++)
        {
            Vector3 position = Vector3.Lerp(start, end, (float)i / (segmentCount - 1));
            GameObject segment = Instantiate(wireSegmentPrefab, position, Quaternion.identity);
            segment.transform.localScale = Vector3.one * 0.05f;
            Rigidbody rb = segment.GetComponent<Rigidbody>();
            rb.mass = 0.1f;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            segments.Add(segment.transform);

            if (i == 0)
            {
                FixedJoint joint = segment.AddComponent<FixedJoint>();
                joint.connectedBody = vacuumBase.GetComponent<Rigidbody>();
            }
            else
            {
                ConfigurableJoint joint = segment.AddComponent<ConfigurableJoint>();
                joint.connectedBody = previousRb;
                joint.xMotion = joint.yMotion = joint.zMotion = ConfigurableJointMotion.Limited;
                joint.linearLimit = new SoftJointLimit { limit = spacing };
                joint.enableCollision = false;
            }

            previousRb = rb;
        }

        FixedJoint endJoint = segments[^1].gameObject.AddComponent<FixedJoint>();
        endJoint.connectedBody = vacuumHead.GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        UpdateMesh();
    }

    void UpdateMesh()
    {
        if (segments.Count < 2) return;

        int vertsPerRing = radialSegments + 1;
        int vertexCount = vertsPerRing * segments.Count;
        int triangleCount = (segments.Count - 1) * radialSegments * 2 * 3;

        Vector3[] vertices = new Vector3[vertexCount];
        Vector3[] normals = new Vector3[vertexCount];
        int[] triangles = new int[triangleCount];

        for (int i = 0; i < segments.Count; i++)
        {
            Transform seg = segments[i];
            Vector3 center = seg.position;

            Vector3 forward;
            if (i == segments.Count - 1)
                forward = (center - segments[i - 1].position).normalized;
            else
                forward = (segments[i + 1].position - center).normalized;

            Vector3 up = Vector3.up;
            if (Vector3.Dot(forward, up) > 0.9f)
                up = Vector3.right;

            Vector3 right = Vector3.Cross(forward, up).normalized;
            up = Vector3.Cross(right, forward).normalized;

            for (int j = 0; j < vertsPerRing; j++)
            {
                float angle = (j / (float)radialSegments) * Mathf.PI * 2f;
                Vector3 offset = right * Mathf.Cos(angle) + up * Mathf.Sin(angle);
                vertices[i * vertsPerRing + j] = transform.InverseTransformPoint(center + offset * wireRadius);
                normals[i * vertsPerRing + j] = offset.normalized;
            }
        }

        int triIndex = 0;
        for (int i = 0; i < segments.Count - 1; i++)
        {
            for (int j = 0; j < radialSegments; j++)
            {
                int current = i * vertsPerRing + j;
                int next = current + vertsPerRing;

                triangles[triIndex++] = current;
                triangles[triIndex++] = current + 1;
                triangles[triIndex++] = next;

                triangles[triIndex++] = next;
                triangles[triIndex++] = current + 1;
                triangles[triIndex++] = next + 1;
            }
        }

        mesh.Clear();
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.triangles = triangles;
    }
}
