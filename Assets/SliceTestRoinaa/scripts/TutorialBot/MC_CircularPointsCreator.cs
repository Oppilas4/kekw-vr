using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_CircularPointsCreator : MonoBehaviour
{
    public int numberOfPoints = 5;
    public float radius = 5f;
    public Vector3[] circlePoints;
    private List<GameObject> pointObjects = new List<GameObject>();

    private void Start()
    {
        GenerateCirclePoints();
    }

    public void GenerateCirclePoints()
    {
        circlePoints = new Vector3[numberOfPoints];
        float angleIncrement = 360f / numberOfPoints;

        for (int i = 0; i < numberOfPoints; i++)
        {
            float angle = i * angleIncrement;
            float x = transform.position.x + radius * Mathf.Cos(Mathf.Deg2Rad * angle);
            float z = transform.position.z + radius * Mathf.Sin(Mathf.Deg2Rad * angle);
            circlePoints[i] = new Vector3(x, transform.position.y, z);

            // Instantiate empty GameObject at the calculated position
            GameObject pointObject = new GameObject("Point" + i);
            pointObject.transform.position = circlePoints[i];
            pointObject.transform.parent = transform;
            pointObjects.Add(pointObject);
        }
    }

    public GameObject FindClosestObject(Vector3 playerPosition)
    {
        GameObject closestObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject pointObject in pointObjects)
        {
            float distance = Vector3.Distance(pointObject.transform.position, playerPosition);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = pointObject;
            }
        }

        return closestObject;
    }
}
