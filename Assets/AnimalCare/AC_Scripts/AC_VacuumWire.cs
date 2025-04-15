using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class AC_VacuumWire : MonoBehaviour
{
    // References to the start and end points of the hose
    public Transform vacuumBase; // The base of the vacuum where the hose starts
    public Transform vacuumHead; // The end point of the hose (usually the handle)

    // The prefab used for each wire segment
    public GameObject wireSegmentPrefab;

    // Hose settings
    public int segmentCount = 10;           // Number of segments making up the wire
    public float segmentSpacing = 0.2f;     // Distance between segments

    // Mesh visual settings
    public int radialSegments = 6;          // How smooth the circular hose mesh is
    public float wireRadius = 0.03f;        // Radius of the visual wire

    // Internal storage
    private List<Transform> segments = new List<Transform>(); // All segments in order
    private Mesh mesh; // The visual mesh

    // Clamping values
    private float maxHoseLength; // Max allowed hose length (calculated)
    private Vector3 hoseOrigin;  // Where the hose starts (vacuum base)

    void Start()
    {
        // Validate references
        if (!vacuumBase || !vacuumHead || !wireSegmentPrefab)
        {
            Debug.LogError("Missing references. Please assign vacuumBase, vacuumHead, and wireSegmentPrefab.");
            return;
        }

        // Set initial values for hose limit
        hoseOrigin = vacuumBase.position;
        maxHoseLength = segmentCount * segmentSpacing * 0.95f; // Slightly less than total possible length

        // Optional: configure head's Rigidbody for smoother physics
        Rigidbody headRb = vacuumHead.GetComponent<Rigidbody>();
        if (headRb != null)
        {
            headRb.mass = 10f;
            headRb.drag = 0.1f;
            headRb.angularDrag = 0.05f;
            headRb.interpolation = RigidbodyInterpolation.Interpolate;
        }

        // Setup the mesh
        GetComponent<MeshFilter>().mesh = mesh = new Mesh();
        mesh.name = "Vacuum Wire Mesh";

        // Create the hose chain
        GenerateWire();
    }

    void GenerateWire()
    {
        // Generate evenly spaced segments between base and head
        Vector3 start = vacuumBase.position;
        Vector3 end = vacuumHead.position;
        float spacing = Vector3.Distance(start, end) / (segmentCount - 1);
        Rigidbody previousRb = null;

        for (int i = 0; i < segmentCount; i++)
        {
            // Calculate this segment's position
            Vector3 position = Vector3.Lerp(start, end, (float)i / (segmentCount - 1));
            GameObject segment = Instantiate(wireSegmentPrefab, position, Quaternion.identity);
            segment.transform.localScale = Vector3.one * 0.05f;

            // Setup Rigidbody
            Rigidbody rb = segment.GetComponent<Rigidbody>();
            rb.mass = 5f;
            rb.drag = 0.1f;
            rb.angularDrag = 0.05f;
            rb.interpolation = RigidbodyInterpolation.Interpolate;

            // Add to list
            segments.Add(segment.transform);

            // Attach to base or previous segment
            if (i == 0)
            {
                // First segment is fixed to the base
                FixedJoint joint = segment.AddComponent<FixedJoint>();
                joint.connectedBody = vacuumBase.GetComponent<Rigidbody>();
            }
            else
            {
                // Other segments use ConfigurableJoints for flexibility
                ConfigurableJoint joint = segment.AddComponent<ConfigurableJoint>();
                joint.connectedBody = previousRb;

                // Limit movement along all axes
                joint.xMotion = joint.yMotion = joint.zMotion = ConfigurableJointMotion.Limited;
                joint.angularXMotion = joint.angularYMotion = joint.angularZMotion = ConfigurableJointMotion.Locked;

                // Limit how far segments can move from each other
                joint.linearLimit = new SoftJointLimit { limit = spacing * 0.6f };

                // Add springy resistance to movement
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

        // Connect last segment to vacuum head
        ConfigurableJoint endJoint = segments[^1].gameObject.AddComponent<ConfigurableJoint>();
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
        // Prevent hose from stretching too far
        ClampVacuumHeadPosition();
    }

    void LateUpdate()
    {
        // Update the visual mesh based on current segment positions
        UpdateMesh();
    }

    void ClampVacuumHeadPosition()
    {
        // Distance vector from base to head
        Vector3 toHead = vacuumHead.position - hoseOrigin;
        float distance = toHead.magnitude;

        if (distance > maxHoseLength)
        {
            // Clamp the head position within the max range
            Vector3 clampedPosition = hoseOrigin + toHead.normalized * maxHoseLength;

            // Move the rigidbody safely within bounds
            Rigidbody headRb = vacuumHead.GetComponent<Rigidbody>();
            if (headRb)
            {
                headRb.velocity = Vector3.zero;
                headRb.angularVelocity = Vector3.zero;
                headRb.MovePosition(clampedPosition); // Directly move the head to clamp position
            }
            else
            {
                vacuumHead.position = clampedPosition; // If no rigidbody, set position directly
            }
        }
    }

    void UpdateMesh()
    {
        if (segments.Count < 2) return;

        // One ring per segment, each with radial segments + 1 for wrapping
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

            // Get the forward direction along the hose
            Vector3 forward = (i == segments.Count - 1)
                ? (center - segments[i - 1].position).normalized
                : (segments[i + 1].position - center).normalized;

            // Choose a stable up vector
            Vector3 up = Vector3.up;
            if (Vector3.Dot(forward, up) > 0.9f)
                up = Vector3.right;

            // Create a rotation frame
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

        // Connect rings with quads (two triangles each)
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

        // Update the mesh
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.normals = normals;
        mesh.triangles = triangles;
    }
}
