using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class AC_NewDryer : MonoBehaviour
{
    [Header("References")]
    public Transform vacuumBase;
    public Transform vacuumHead;
    public GameObject wireSegmentPrefab;

    [Header("Settings")]
    public int segmentCount = 20;
    public float segmentSpacing = 0.2f;
    public float wireRadius = 0.03f;
    public int radialSegments = 6;

    [Header("Physics")]
    public float segmentMass = 2f;
    public float jointLimitFactor = 0.6f;
    public float jointSpring = 4000f;
    public float jointDamper = 200f;

    private List<Transform> segments = new List<Transform>();
    private Mesh mesh;
    private float maxHoseLength;

    void Start()
    {
        if (!vacuumBase || !vacuumHead || !wireSegmentPrefab)
        {
            Debug.LogError("Missing references.");
            return;
        }

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Vacuum Hose Mesh";

        maxHoseLength = segmentSpacing * (segmentCount - 1) * 1.1f; // Clamp safety margin
        GenerateHose();
    }

    void GenerateHose()
    {
        segments.Clear();
        Rigidbody previousRb = null;

        for (int i = 0; i < segmentCount; i++)
        {
            Vector3 pos = Vector3.Lerp(vacuumBase.position, vacuumHead.position, i / (float)(segmentCount - 1));
            GameObject segment = Instantiate(wireSegmentPrefab, pos, Quaternion.identity, transform);
            segment.name = $"Hose Segment {i}";
            segment.transform.localScale = Vector3.one * 0.05f;

            Rigidbody rb = segment.GetComponent<Rigidbody>();
            rb.mass = segmentMass;
            rb.interpolation = RigidbodyInterpolation.Interpolate;
            rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
            rb.useGravity = false;

            segments.Add(segment.transform);

            if (i == 0)
            {
                FixedJoint baseJoint = segment.AddComponent<FixedJoint>();
                baseJoint.connectedBody = vacuumBase.GetComponent<Rigidbody>();
            }
            else
            {
                ConfigurableJoint joint = segment.AddComponent<ConfigurableJoint>();
                joint.connectedBody = previousRb;
                joint.autoConfigureConnectedAnchor = false;
                joint.anchor = Vector3.zero;
                joint.connectedAnchor = Vector3.zero;

                joint.xMotion = joint.yMotion = joint.zMotion = ConfigurableJointMotion.Limited;
                joint.angularXMotion = joint.angularYMotion = joint.angularZMotion = ConfigurableJointMotion.Locked;

                joint.linearLimit = new SoftJointLimit { limit = segmentSpacing * jointLimitFactor };

                JointDrive drive = new JointDrive
                {
                    positionSpring = jointSpring,
                    positionDamper = jointDamper,
                    maximumForce = Mathf.Infinity
                };

                joint.xDrive = joint.yDrive = joint.zDrive = drive;
            }

            previousRb = rb;
        }

        // Connect last segment to the vacuum head
        ConfigurableJoint endJoint = segments[^1].gameObject.AddComponent<ConfigurableJoint>();
        endJoint.connectedBody = vacuumHead.GetComponent<Rigidbody>();
        endJoint.autoConfigureConnectedAnchor = false;
        endJoint.anchor = Vector3.zero;
        endJoint.connectedAnchor = Vector3.zero;

        endJoint.xMotion = endJoint.yMotion = endJoint.zMotion = ConfigurableJointMotion.Limited;
        endJoint.angularXMotion = endJoint.angularYMotion = endJoint.angularZMotion = ConfigurableJointMotion.Locked;
        endJoint.linearLimit = new SoftJointLimit { limit = segmentSpacing * jointLimitFactor };

        JointDrive endDrive = new JointDrive
        {
            positionSpring = jointSpring,
            positionDamper = jointDamper,
            maximumForce = Mathf.Infinity
        };

        endJoint.xDrive = endJoint.yDrive = endJoint.zDrive = endDrive;
    }

    void FixedUpdate()
    {
        ClampVacuumHeadDistance();
    }

    void ClampVacuumHeadDistance()
    {
        Vector3 direction = vacuumHead.position - vacuumBase.position;
        float distance = direction.magnitude;

        if (distance > maxHoseLength)
        {
            Rigidbody rb = vacuumHead.GetComponent<Rigidbody>();
            Vector3 clampedPos = vacuumBase.position + direction.normalized * maxHoseLength;

            if (rb)
            {
                rb.velocity = Vector3.zero;
                rb.MovePosition(clampedPos);
            }
            else
            {
                vacuumHead.position = clampedPos;
            }
        }
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

            Vector3 forward = (i == segments.Count - 1)
                ? (center - segments[i - 1].position).normalized
                : (segments[i + 1].position - center).normalized;

            Vector3 up = Vector3.up;
            if (Vector3.Dot(forward, up) > 0.9f) up = Vector3.right;

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
