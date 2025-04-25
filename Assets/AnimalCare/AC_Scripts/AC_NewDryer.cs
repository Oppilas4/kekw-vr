using UnityEngine;

public class AC_NewDryer : MonoBehaviour
{
    public Transform vacuumBase;
    public Transform vacuumHead;
    public int curveResolution = 20;
    public float curveTightness = 0.1f; // Lower value = straighter

    private LineRenderer lineRenderer;

    void Start()
    {
        if (!vacuumBase || !vacuumHead)
        {
            Debug.LogError("Missing references. Please assign vacuumBase and vacuumHead.");
            return;
        }

        lineRenderer = GetComponent<LineRenderer>();
        if (!lineRenderer)
            lineRenderer = gameObject.AddComponent<LineRenderer>();

        lineRenderer.positionCount = curveResolution;
        lineRenderer.widthMultiplier = 0.06f;
        lineRenderer.useWorldSpace = true;
    }

    void LateUpdate()
    {
        Vector3 start = vacuumBase.position;
        Vector3 end = vacuumHead.position;
        Vector3 up = Vector3.up; // Use this to slightly lift the middle of the hose

        for (int i = 0; i < curveResolution; i++)
        {
            float t = (float)i / (curveResolution - 1);
            Vector3 point = Vector3.Lerp(start, end, t);

            // Apply subtle curvature
            float offset = Mathf.Sin(t * Mathf.PI) * curveTightness;
            point += up * offset;

            lineRenderer.SetPosition(i, point);
        }
    }
}
