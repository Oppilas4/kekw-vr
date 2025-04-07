using UnityEngine;
using System.Collections.Generic;

public class AC_VacuumWire : MonoBehaviour
{
    public Transform vacuumBase;       // Attach the vacuum body Transform here
    public Transform vacuumHead;       // Attach the vacuum head (movable part)

    public GameObject wireSegmentPrefab;
    public int segmentCount = 20;
    public float segmentSpacing = 0.2f;

    private List<Transform> segments = new List<Transform>();
    private LineRenderer lineRenderer;

    void Start()
    {
        if (!vacuumBase || !vacuumHead || !wireSegmentPrefab)
        {
            Debug.LogError("Missing references. Please assign vacuumBase, vacuumHead, and wireSegmentPrefab.");
            return;
        }

        lineRenderer = GetComponent<LineRenderer>();
        if (!lineRenderer)
        {
            lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        GenerateWire();
    }

    void GenerateWire()
    {
        Vector3 start = vacuumBase.position;
        Vector3 end = vacuumHead.position;
        Vector3 direction = (end - start).normalized;
        float totalLength = Vector3.Distance(start, end);
        float spacing = totalLength / (segmentCount - 1);

        Rigidbody previousRb = null;

        for (int i = 0; i < segmentCount; i++)
        {
            Vector3 position = Vector3.Lerp(start, end, (float)i / (segmentCount - 1));
            GameObject segment = Instantiate(wireSegmentPrefab, position, Quaternion.identity);
            segment.transform.localScale = Vector3.one * 0.05f; // small size
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

        // Attach the last segment to the vacuum head
        FixedJoint endJoint = segments[^1].gameObject.AddComponent<FixedJoint>();
        endJoint.connectedBody = vacuumHead.GetComponent<Rigidbody>();

        lineRenderer.positionCount = segmentCount;
        lineRenderer.widthMultiplier = 0.06f;
        lineRenderer.useWorldSpace = true;
    }

    void LateUpdate()
    {
        for (int i = 0; i < segments.Count; i++)
        {
            lineRenderer.SetPosition(i, segments[i].position);
        }
    }
}
