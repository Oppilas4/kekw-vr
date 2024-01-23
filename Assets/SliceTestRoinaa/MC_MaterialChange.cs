using UnityEngine;

public class MC_MaterialChange : MonoBehaviour
{
    public Material newMaterial; // The new material to apply

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider's GameObject has the tag "Potato"
        if (other.CompareTag("Potato"))
        {
            // Shoot a ray from the current object's position towards the other object
            Ray ray = new Ray(transform.position, other.transform.position - transform.position);

            // Check if the ray hits something
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the hit object is the Potato
                if (hit.collider.CompareTag("Potato"))
                {
                    // Get the MeshFilter component from the hit object
                    MeshFilter meshFilter = hit.collider.GetComponent<MeshFilter>();

                    if (meshFilter != null)
                    {
                        // Get the mesh from the MeshFilter
                        Mesh mesh = meshFilter.mesh;

                        // Get the triangle indices of the hit face
                        int[] triangleIndices = new int[] { hit.triangleIndex * 3, hit.triangleIndex * 3 + 1, hit.triangleIndex * 3 + 2 };

                        // Assign the new material to the specified face
                        AssignMaterialToFace(mesh, triangleIndices, newMaterial);
                    }
                }
            }
        }
    }

    void AssignMaterialToFace(Mesh mesh, int[] faceVertices, Material material)
    {
        // Create an array of colors (materials) for each vertex in the face
        Color[] colors = new Color[mesh.vertices.Length];

        // Set the new material for the specified face vertices
        foreach (int vertexIndex in faceVertices)
        {
            colors[vertexIndex] = material.color; // Assuming you're changing the color
        }

        // Assign the modified colors array to the mesh
        mesh.colors = colors;
    }
}
