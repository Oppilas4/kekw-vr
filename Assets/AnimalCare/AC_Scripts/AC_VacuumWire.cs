using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class AC_VacuumWire : MonoBehaviour
{
    public Transform vacuumBase;
    public Transform vacuumHead;
    public GameObject wireSegmentPrefab;

    public int segmentCount = 10;
    public float segmentSpacing = 0.2f;

    public int radialSegments = 6;
    public float wireRadius = 0.03f;

    private List<Transform> segments = new List<Transform>();
    private Mesh mesh;

    private float maxHoseLength;
    private Vector3 hoseOrigin;

    private ConfigurableJoint endJoint;
    private XRGrabInteractable grabInteractable;

    void Start()
    {
        if (!vacuumBase || !vacuumHead || !wireSegmentPrefab)
        {
            Debug.LogError("Missing references. Please assign vacuumBase, vacuumHead, and wireSegmentPrefab.");
            return;
        }

        hoseOrigin = vacuumBase.position;
        maxHoseLength = segmentCount * segmentSpacing * 0.95f;

        grabInteractable = vacuumHead.GetComponent<XRGrabInteractable>();

        Rigidbody headRb = vacuumHead.GetComponent<Rigidbody>();
        if (headRb != null)
        {
            headRb.mass = 10f;
            headRb.drag = 0.1f;
            headRb.angularDrag = 0.05f;
            headRb.interpolation = RigidbodyInterpolation.Interpolate;
        }

        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Vacuum Wire Mesh";

        GenerateWire();
    }

    public void ResetHose()
    {
        // Reset head position and velocity
        Rigidbody headRb = vacuumHead.GetComponent<Rigidbody>();
        if (headRb != null)
        {
            headRb.velocity = Vector3.zero;
            headRb.angularVelocity = Vector3.zero;
            headRb.MovePosition(vacuumBase.position + Vector3.forward * 0.5f); // or wherever you want it to reset
        }
        else
        {
            vacuumHead.position = vacuumBase.position + Vector3.forward * 0.5f;
        }
    }

    void GenerateWire()
    {
        Vector3 start = vacuumBase.position;
        Vector3 end = vacuumHead.position;
        float spacing = Vector3.Distance(start, end) / (segmentCount - 1);
        Rigidbody previousRb = null;

        for (int i = 0; i < segmentCount; i++)
        {
            Vector3 position = Vector3.Lerp(start, end, (float)i / (segmentCount - 1));
            GameObject segment = Instantiate(wireSegmentPrefab, position, Quaternion.identity);
            segment.transform.localScale = Vector3.one * 0.05f;

            Rigidbody rb = segment.GetComponent<Rigidbody>();
            rb.mass = 5f;
            rb.drag = 0.1f;
            rb.angularDrag = 0.05f;
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
                joint.angularXMotion = joint.angularYMotion = joint.angularZMotion = ConfigurableJointMotion.Locked;

                joint.linearLimit = new SoftJointLimit { limit = spacing * 0.6f };

                JointDrive drive = new JointDrive
                {
                    positionSpring = 3000f,
                    positionDamper = 100f,
                    maximumForce = Mathf.Infinity
                };

                joint.xDrive = joint.yDrive = joint.zDrive = drive;
                joint.configuredInWorldSpace = false;
                joint.slerpDrive = drive;
                joint.enableCollision = false;
            }

            previousRb = rb;
        }

        endJoint = segments[^1].gameObject.AddComponent<ConfigurableJoint>();
        endJoint.connectedBody = vacuumHead.GetComponent<Rigidbody>();

        endJoint.xMotion = endJoint.yMotion = endJoint.zMotion = ConfigurableJointMotion.Limited;
        endJoint.angularXMotion = endJoint.angularYMotion = endJoint.angularZMotion = ConfigurableJointMotion.Locked;

        endJoint.linearLimit = new SoftJointLimit { limit = segmentSpacing * 0.75f };

        JointDrive endDrive = new JointDrive
        {
            positionSpring = 3000f,
            positionDamper = 100f,
            maximumForce = Mathf.Infinity
        };

        endJoint.xDrive = endJoint.yDrive = endJoint.zDrive = endDrive;
        endJoint.configuredInWorldSpace = false;
    }

    void FixedUpdate()
    {
        ClampVacuumHeadPosition();
    }

    void LateUpdate()
    {
        UpdateMesh();
    }

    void ClampVacuumHeadPosition()
    {
        Vector3 toHead = vacuumHead.position - hoseOrigin;
        float distance = toHead.magnitude;

        // Set a drop threshold so the vacuum head is dropped if pulled too far
        float dropThreshold = maxHoseLength * 0.55f; // Adjust this to control the drop distance (lower means it drops sooner)

        if (distance > dropThreshold)
        {
            // Force drop if held by XR
            if (grabInteractable && grabInteractable.isSelected)
            {
                var interactor = grabInteractable.selectingInteractor;
                if (interactor != null && interactor.interactionManager != null)
                {
                    interactor.interactionManager.SelectExit(interactor, grabInteractable);
                    Debug.Log("Vacuum head pulled too far — forced early drop.");
                }
            }

            Rigidbody headRb = vacuumHead.GetComponent<Rigidbody>();
            if (headRb)
            {
                headRb.velocity = Vector3.zero;
                headRb.angularVelocity = Vector3.zero;
            }
        }
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
