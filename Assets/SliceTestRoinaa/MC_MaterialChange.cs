using UnityEngine;

public class MC_MaterialChange : MonoBehaviour
{
    public GameObject objectWithRay; // The object shooting the ray
    public Material newMaterial; // The new material to apply
    private float rayLength = 10f; // Length of the ray

    private MeshRenderer _renderer;
    private MeshFilter filter;
    private Mesh mesh;
    private Material[] materials;

    void Start()
    {
        // Ensure the object has a MeshRenderer and MeshFilter
        _renderer = objectWithRay.GetComponent<MeshRenderer>();
        filter = objectWithRay.GetComponent<MeshFilter>();
        mesh = filter.sharedMesh;
        materials = _renderer.materials;
    }

    void Update()
    {
        // Cast a ray from the object's position towards its forward direction
        Ray ray = new Ray(objectWithRay.transform.position, objectWithRay.transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            // Check if the hit object has the "Potato" tag
            if (hit.collider.gameObject.tag == "Potato")
            {
                // Get the submesh index of the hit triangle
                int triangleIndex = hit.triangleIndex;
                int subMeshIndex = GetSubMeshIndex(mesh, triangleIndex);

                // Change the material of the hit submesh
                materials[subMeshIndex] = newMaterial;
                _renderer.materials = materials;
            }
        }
    }

    public static int GetSubMeshIndex(Mesh mesh, int triangleIndex)
    {
        if (!mesh.isReadable) return 0;

        var triangleCounter = 0;
        for (var subMeshIndex = 0; subMeshIndex < mesh.subMeshCount; subMeshIndex++)
        {
            var indexCount = mesh.GetSubMesh(subMeshIndex).indexCount;
            triangleCounter += indexCount / 3;
            if (triangleIndex < triangleCounter) return subMeshIndex;
        }

        return 0;
    }

    void OnDrawGizmos()
    {
        // Draw the ray
        Gizmos.color = Color.red;
        Gizmos.DrawRay(objectWithRay.transform.position, objectWithRay.transform.forward * rayLength);
    }
}
