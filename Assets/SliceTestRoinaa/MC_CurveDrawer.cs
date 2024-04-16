using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Unity.Mathematics;
using static UnityEngine.GraphicsBuffer;

[ExecuteInEditMode]
public class MC_CurveDrawer : MonoBehaviour
{
    public int numberOfPoints = 5;
    public float spacing = 1.0f;
    public Vector3[] waypoints;
    public bool loop = false;
    public List<Vector3> curvePoints = new List<Vector3>();
    public float noiseAmount = 0.3f;

    public GameObject trainTrackPrefab;
    public List<GameObject> trainTrackSegments = new List<GameObject>();


#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        for (int i = 0; i < waypoints.Length; i++)
        {
            Vector3 worldPosition = transform.TransformPoint(waypoints[i]);
            Gizmos.DrawWireSphere(worldPosition, 0.1f);
        }

        if (waypoints.Length >= 3)
        {
            DrawCurve();
        }
    }
    public void GenerateTrainTrack()
    {
        if (trainTrackPrefab == null)
        {
            return;
        }

        // Iterate over the curve points
        for (int i = 0; i < curvePoints.Count; i++)
        {
            // If we have fewer segments than curve points, create a new segment
            if (i >= trainTrackSegments.Count)
            {
                // Determine the direction of the curve at this point
                Vector3 direction = GetCurveDirectionAtPoint(i);

                // Instantiate a train track segment at this point, aligned with the curve direction
                GameObject segment = Instantiate(trainTrackPrefab, curvePoints[i], Quaternion.LookRotation(direction));
                trainTrackSegments.Add(segment);
            }
            else if (i >= curvePoints.Count)
            {
                Destroy(trainTrackSegments[i]);
                trainTrackSegments.RemoveAt(i);
            }

        }
    }
    public void ClearTrainTracks()
    {
        foreach (var segment in trainTrackSegments)
        {
            DestroyImmediate(segment);
        }
        trainTrackSegments.Clear();
    }
    public void UpdateTrack()
    {
        for (int i = 0; i < curvePoints.Count; i++)
        {
            if (trainTrackSegments.Count > 0)
            {
                // Update the position and orientation of the existing segment
                trainTrackSegments[i].transform.position = curvePoints[i];
                trainTrackSegments[i].transform.rotation = Quaternion.LookRotation(GetCurveDirectionAtPoint(i));
            }

        }

    }
    // This method calculates the direction of the curve at a given point
    Vector3 GetCurveDirectionAtPoint(int index)
    {
        // If it's the first or last point, use the next or previous point respectively
        if (index == 0)
            return (curvePoints[1] - curvePoints[0]).normalized;
        else if (index == curvePoints.Count - 1)
            return (curvePoints[curvePoints.Count - 1] - curvePoints[curvePoints.Count - 2]).normalized;

        // Otherwise, use the average direction towards the next and previous points
        Vector3 nextDirection = (curvePoints[index + 1] - curvePoints[index]).normalized;
        Vector3 prevDirection = (curvePoints[index] - curvePoints[index - 1]).normalized;
        return (nextDirection + prevDirection).normalized;
    }
    public void DrawCurve()
    {
        curvePoints.Clear();

        // Draw the curve for the first segment (connecting the first three points)
        DrawCatmullRomSpline(transform.TransformPoint(waypoints[0]), transform.TransformPoint(waypoints[0]), transform.TransformPoint(waypoints[1]), transform.TransformPoint(waypoints[2]));

        for (int i = 0; i < waypoints.Length - 3; i++)
        {
            DrawCatmullRomSpline(transform.TransformPoint(waypoints[i]), transform.TransformPoint(waypoints[i + 1]), transform.TransformPoint(waypoints[i + 2]), transform.TransformPoint(waypoints[i + 3]));
        }

        // Draw the curve for the last segment (connecting the last three points)
        DrawCatmullRomSpline(transform.TransformPoint(waypoints[waypoints.Length - 3]), transform.TransformPoint(waypoints[waypoints.Length - 2]), transform.TransformPoint(waypoints[waypoints.Length - 1]), transform.TransformPoint(waypoints[waypoints.Length - 1]));

        // If loop is true, draw a curve from the last point to the first point
        if (loop)
        {
            DrawCatmullRomSpline(transform.TransformPoint(waypoints[waypoints.Length - 1]), transform.TransformPoint(waypoints[waypoints.Length - 1]), transform.TransformPoint(waypoints[0]), transform.TransformPoint(waypoints[1]));
        }

    }

    public void DrawCatmullRomSpline(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float precision = 0.05f;

        for (float t = 0; t < 1; t += precision)
        {
            Vector3 point = CatmullRomSplinePoint(t, p0, p1, p2, p3);
            Handles.DrawSolidDisc(point, Vector3.forward, 0.05f);
            curvePoints.Add(point);
        }
    }

    public Vector3 CatmullRomSplinePoint(float t, Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        float tt = t * t;
        float ttt = tt * t;

        Vector3 p = 0.5f * ((2f * p1) +
                           (-p0 + p2) * t +
                           (2f * p0 - 5f * p1 + 4f * p2 - p3) * tt +
                           (-p0 + 3f * p1 - 3f * p2 + p3) * ttt);

        return p;
    }

    public void RandomizeYAxisWithPerlin()
    {
        for (int i = 0; i < waypoints.Length; i++)
        {
            float highFrequencyNoise = Mathf.PerlinNoise(i * 1f, Time.time) * noiseAmount;

            waypoints[i].y = (highFrequencyNoise) * 2 - 1; // Scale noise from [-1, 1] to [0, 1]
        }
    }

    public Vector3 GetClosestPointOnCurve(Vector3 playerPosition)
    {
        Vector3 closestPoint = curvePoints[0];
        float closestDistance = Vector3.Distance(playerPosition, closestPoint);

        foreach (Vector3 point in curvePoints)
        {
            float distance = Vector3.Distance(playerPosition, point);
            if (distance < closestDistance)
            {
                closestPoint = point;
                closestDistance = distance;
            }
        }

        return closestPoint;
    }

    [CustomEditor(typeof(MC_CurveDrawer))]
    public class CurveDrawerEditor : Editor
    {
        private void OnSceneGUI()
        {
            MC_CurveDrawer curveDrawer = (MC_CurveDrawer)target;
            curveDrawer.UpdateTrack();

            EditorGUI.BeginChangeCheck();

            for (int i = 0; i < curveDrawer.waypoints.Length; i++)
            {
                Vector3 handlePosition = curveDrawer.transform.InverseTransformPoint(Handles.PositionHandle(curveDrawer.transform.TransformPoint(curveDrawer.waypoints[i]), Quaternion.identity));

                if (EditorGUI.EndChangeCheck())
                {
                    Undo.RecordObject(curveDrawer, "Move Point");
                    curveDrawer.waypoints[i] = handlePosition;
                    EditorUtility.SetDirty(curveDrawer);
                }
            }
        }


        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            MC_CurveDrawer curveDrawer = (MC_CurveDrawer)target;

            int oldNumberOfPoints = curveDrawer.numberOfPoints;
            float oldSpacing = curveDrawer.spacing;

            curveDrawer.numberOfPoints = EditorGUILayout.IntSlider("Number of Points", curveDrawer.numberOfPoints, 3, 20);
            curveDrawer.spacing = EditorGUILayout.FloatField("Spacing", curveDrawer.spacing);

            if (oldNumberOfPoints != curveDrawer.numberOfPoints || oldSpacing != curveDrawer.spacing)
            {
                UpdateCurve(curveDrawer);
            }

            curveDrawer.noiseAmount = EditorGUILayout.FloatField("NoiseValue", curveDrawer.noiseAmount);
            curveDrawer.loop = EditorGUILayout.Toggle("Loop", curveDrawer.loop);
            curveDrawer.trainTrackPrefab = (GameObject)EditorGUILayout.ObjectField("GameObject", curveDrawer.trainTrackPrefab, typeof(GameObject), false);

            if (GUILayout.Button("Randomize in Y Axis"))
            {
                curveDrawer.RandomizeYAxisWithPerlin();
            }

            if (GUILayout.Button("Reset Curve"))
            {
                ResetCurve(curveDrawer);
            }
            if (GUILayout.Button("Create Train Tracks"))
            {
                curveDrawer.GenerateTrainTrack();
            }

            if (GUILayout.Button("Clear Train Tracks"))
            {
                curveDrawer.ClearTrainTracks();
            }


            serializedObject.ApplyModifiedProperties();
        }


        public void ResetCurve(MC_CurveDrawer curveDrawer)
        {
            int defaultNumberOfPoints = 10;
            float defaultSpacing = 2f;

            curveDrawer.numberOfPoints = defaultNumberOfPoints;
            curveDrawer.spacing = defaultSpacing;

            curveDrawer.ClearTrainTracks();
            curveDrawer.waypoints = new Vector3[defaultNumberOfPoints];

            // Use the parent object's local position as a starting point
            Vector3 startPosition = curveDrawer.transform.localPosition;

            if (curveDrawer.loop)
            {
                // Calculate the radius of the circle
                float radius = (2 * Mathf.PI * defaultSpacing) / (defaultNumberOfPoints - 1);

                // For each point, calculate its angle and position
                for (int i = 0; i < defaultNumberOfPoints; i++)
                {
                    float theta = (2 * Mathf.PI * i) / (defaultNumberOfPoints);
                    float x = radius * Mathf.Cos(theta);
                    float y = radius * Mathf.Sin(theta);
                    curveDrawer.waypoints[i] = startPosition + new Vector3(x, 0, y);
                }
            }
            else
            {
                // Place the waypoints in a straight line
                for (int i = 0; i < defaultNumberOfPoints; i++)
                {
                    curveDrawer.waypoints[i] = new Vector3(1, 0, 0) + new Vector3(i * defaultSpacing, 0, 0); // Add local offset
                }
            }
        }

        private void UpdateCurve(MC_CurveDrawer curveDrawer)
        {
            // Calculate the new waypoints
            Vector3[] newWaypoints = new Vector3[curveDrawer.numberOfPoints];

            if (curveDrawer.loop)
            {
                // Calculate the radius of the circle
                float radius = (2 * Mathf.PI * curveDrawer.spacing) / (curveDrawer.numberOfPoints - 1);

                // For each point, calculate its angle and position
                for (int i = 0; i < curveDrawer.numberOfPoints; i++)
                {
                    float theta = (2 * Mathf.PI * i) / (curveDrawer.numberOfPoints);
                    float x = radius * Mathf.Cos(theta);
                    float y = radius * Mathf.Sin(theta);
                    newWaypoints[i] = curveDrawer.transform.localPosition + new Vector3(x, 0, y);
                }
            }
            else
            {
                // Place the waypoints in a straight line
                for (int i = 0; i < curveDrawer.numberOfPoints; i++)
                {
                    newWaypoints[i] = new Vector3(1, 0, 0) + new Vector3(i * curveDrawer.spacing, 0, 0); // Add local offset
                }
            }

            // Update the waypoints
            curveDrawer.waypoints = newWaypoints;

            // Redraw the curve
            curveDrawer.DrawCurve();
        }



    }
#endif
}