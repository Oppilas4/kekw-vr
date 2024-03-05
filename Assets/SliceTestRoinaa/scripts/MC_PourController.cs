using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class MC_PourController : MonoBehaviour
{
    public List<Transform> pourEffectPositions; // List of points around the rim for pouring effect
    private Vector3 lowestPosition;
    private float yRotation;
    private float pourThreshold = 30f;

    public bool IsContainerTilted()
    {
        // Check if the pot is tilted forward or to the side based on the x and z-axis rotations
        float xRotation = transform.parent.rotation.eulerAngles.x;
        float zRotation = transform.parent.rotation.eulerAngles.z;

        // Normalize the rotation angles to be between -180 and 180 degrees
        xRotation = (xRotation > 180f) ? xRotation - 360f : xRotation;
        zRotation = (zRotation > 180f) ? zRotation - 360f : zRotation;

        // Check if the pot is tilted beyond the pour threshold in either x or z direction
        return (Mathf.Abs(xRotation) > pourThreshold || Mathf.Abs(zRotation) > pourThreshold);
    }

    public void PourLiquid(VisualEffect pourEffect, Renderer liquidRenderer, float fillLevel)
    {
        // Decrease the fill level gradually
        fillLevel = Mathf.MoveTowards(fillLevel, 0f, Time.deltaTime * 0.2f);
        liquidRenderer.material.SetFloat("_Fill", fillLevel);

        // Check if the pot is empty
        if (fillLevel == 0f)
        {
            pourEffect.SendEvent("Stop");
        }
    }

    public void SetPourPosition(VisualEffect pourEffect)
    {
        // Choose the lowest point among the pourEffectPositions
        Vector3 pourPosition = GetLowestPourPosition();

        // Set the pour effect position
        pourEffect.transform.position = pourPosition;

        pourEffect.transform.localRotation = Quaternion.Euler(125f, yRotation, 0f);

        pourEffect.SendEvent("Pour");
    }

    private Vector3 GetLowestPourPosition()
    {
        lowestPosition = pourEffectPositions[0].position; // Initialize with the position of the first pour effect position

        for (int i = 1; i < pourEffectPositions.Count; i++)
        {
            Transform pourTransform = pourEffectPositions[i];

            // Compare pourTransform.position.y with the y-coordinate of lowestPosition
            if (pourTransform.position.y < lowestPosition.y)
            {
                lowestPosition = pourTransform.position;
                yRotation = pourTransform.localRotation.eulerAngles.y;
            }
        }

        return lowestPosition;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float lineLength = 0.2f; // The desired length of the line
        // Draw gizmos for all pourEffectPositions
        foreach (Transform pourTransform in pourEffectPositions)
        {
            Gizmos.DrawSphere(pourTransform.position, 0.01f);

            // Draw a red line from the pour position to the direction it's facing
            Vector3 direction = pourTransform.forward; // The direction it's facing
            Vector3 lineEndPoint = pourTransform.position + direction.normalized * lineLength; // The end point of the line, scaled by the desired length
            Gizmos.DrawLine(pourTransform.position, lineEndPoint);
        }

        // Highlight the lowest pour position in green
        Gizmos.color = Color.green;
        Vector3 lowestPourPosition = GetLowestPourPosition();
        Gizmos.DrawSphere(lowestPourPosition, 0.01f);
    }
}
