using UnityEngine;
using System.Collections.Generic;

public class SaladBowl : MonoBehaviour
{
    //This script calculates the size of the pieces placed on the bowl and gives negative score accordingly.

    public float thresholdSize = 0.05f; // Adjust as needed
    public float negativeScore = 10.0f;

    private List<GameObject> piecesInsideBowl = new List<GameObject>();

    float CalculatePieceSize(GameObject piece)
    {
        MeshFilter meshFilter = piece.GetComponent<MeshFilter>();

        if (meshFilter != null)
        {
            Mesh mesh = meshFilter.mesh;

            Vector3[] vertices = mesh.vertices;
            int[] triangles = mesh.triangles;

            float pieceSize = 0f;

            for (int i = 0; i < triangles.Length; i += 3)
            {
                Vector3 v1 = vertices[triangles[i]];
                Vector3 v2 = vertices[triangles[i + 1]];
                Vector3 v3 = vertices[triangles[i + 2]];

                // Calculate the volume of the triangle and add it to the pieceSize
                pieceSize += Vector3.Dot(Vector3.Cross(v1, v2), v3) / 6.0f;
            }

            return Mathf.Abs(pieceSize);
        }
        else
        {
            Debug.LogWarning("No MeshFilter found on the sliced piece.");
            return 0f;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("VegetablePiece"))
        {
            piecesInsideBowl.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("VegetablePiece"))
        {
            piecesInsideBowl.Remove(other.gameObject);
        }
    }

    void Update()
    {
        foreach (var piece in piecesInsideBowl)
        {
            float pieceSize = CalculatePieceSize(piece);
            Debug.Log(pieceSize);
            if (pieceSize > thresholdSize)
            {
                // Deduct points from the player's score
                // You should have a scoring system in place to update the score here
                Debug.Log("Piece size exceeded the threshold! Deducting points...");
            }
        }
    }
}
