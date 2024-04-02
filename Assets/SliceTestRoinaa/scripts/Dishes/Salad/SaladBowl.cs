using UnityEngine;
using System.Collections.Generic;

public class SaladBowl : MonoBehaviour
{
    public float thresholdSize = 0.05f; // Adjust as needed
    public float missingComponentDeduction = 33f; // Deduction for each missing component
    public float oversizedPieceDeduction = 0.1f; // Deduction per oversized piece
    public float totalAvailableSpace = 100f; // Adjust as needed

    private List<GameObject> piecesInsideBowl = new List<GameObject>();
    private List<GameObject> invalidIngredients = new List<GameObject>();

    // List of valid vegetable names
    public List<string> validVegetableNames = new List<string>();

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
        // Check if the piece has any of the valid vegetable names as tags
        if (IsValidVegetablePiece(other.gameObject))
        {
            piecesInsideBowl.Add(other.gameObject);
        }
        else
        {
            invalidIngredients.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (IsValidVegetablePiece(other.gameObject))
        {
            piecesInsideBowl.Remove(other.gameObject);
        }
    }

    bool IsValidVegetablePiece(GameObject piece)
    {
        // Check if the piece has any of the valid vegetable names as tags
        foreach (string validName in validVegetableNames)
        {
            if (piece.CompareTag(validName))
            {
                return true;
            }
        }
        return false;
    }

    public void CalculateDish()
    {
        // Check if the current dish in the serving area is the same as the one triggering the calculations
        if (CompletedDishArea.currentDish == transform.parent.gameObject)
        {
            float baseScore = 100;
            float dishScore = 100;
            int cucumberCount = 0;
            int tomatoCount = 0;
            int lettuceCount = 0;
            float totalPieceSize = 0f;

            foreach (var piece in piecesInsideBowl)
            {
                float pieceSize = CalculatePieceSize(piece);
                totalPieceSize += pieceSize;

                if (pieceSize > thresholdSize)
                {
                    // Deduct points for oversized pieces
                    float oversizedDeduction = Mathf.Clamp01((pieceSize - thresholdSize) / thresholdSize) * oversizedPieceDeduction;
                    dishScore -= (baseScore * oversizedDeduction);
                }

                // Count the components
                if (piece.CompareTag("Cucumber")) cucumberCount++;
                else if (piece.CompareTag("Tomato")) tomatoCount++;
                else if (piece.CompareTag("Lettuce")) lettuceCount++;
            }

            // Calculate the ratio of each component
            float cucumberRatio = piecesInsideBowl.Count > 0 ? (float)cucumberCount / piecesInsideBowl.Count : 0f;
            float tomatoRatio = piecesInsideBowl.Count > 0 ? (float)tomatoCount / piecesInsideBowl.Count : 0f;
            float lettuceRatio = piecesInsideBowl.Count > 0 ? (float)lettuceCount / piecesInsideBowl.Count : 0f;

            // Deduct points for missing components
            if (cucumberRatio == 0f)
            {
                dishScore -= missingComponentDeduction;
            }

            if (tomatoRatio == 0f)
            {
                dishScore -= missingComponentDeduction;
            }

            if (lettuceRatio == 0f)
            {
                dishScore -= missingComponentDeduction;
            }

            // Calculate unused space
            float unusedSpaceRatio = 1 - (totalPieceSize / totalAvailableSpace);

            // Initialize unusedSpaceDeduction with a low value for a casual game
            float unusedSpaceDeduction = Mathf.Clamp01(unusedSpaceRatio) * 0.5f; // Adjust the value as needed

            // Deduct points based on the unused space ratio
            dishScore = dishScore * unusedSpaceDeduction;

            if (invalidIngredients.Count > 0)
            {
                dishScore = dishScore * 0.5f;
            }
            /*
            Debug.Log("totalPieceSize: " + totalPieceSize);
            Debug.Log("totalAvailableSpace: " + totalAvailableSpace);
            Debug.Log("unusedSpaceRatio: " + unusedSpaceRatio);
            Debug.Log("unusedSpaceDeduction: " + unusedSpaceDeduction);
            */

            // Update the game manager with the calculated score
            DishScoreManager.Instance.UpdateScore(dishScore);
        }
    }


}
