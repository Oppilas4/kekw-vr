using UnityEngine;

public class MeshGroupRenderer : MonoBehaviour
{
    const string COLOR = "_Color";
    const string COLOREND = "_ColorEnd";

    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    MeshGroup meshGroup;
    Material lastAddedMaterial;

    public bool Add(Transform t, Mesh mesh, Material material)
    {
        Color color = material.GetColor(COLOR);
        Color colorEnd = material.GetColor(COLOREND);
        if (meshGroup == null)
        {
            meshGroup = new MeshGroup(material.name, color, colorEnd);
        }
        meshGroup.meshes.Add(mesh);
        meshGroup.transforms.Add(t);
        lastAddedMaterial = material;
        return true;
    }

    public void CombineAndRender()
    {
        if (meshGroup != null)
        {
            Mesh mesh = CombineMeshes(meshGroup);
            meshFilter.mesh = mesh;
            meshRenderer.material = lastAddedMaterial;
        }
    }

    private Mesh CombineMeshes(MeshGroup group)
    {
        var combine = new CombineInstance[group.meshes.Count];
        for (int i = 0; i < group.meshes.Count; i++)
        {
            combine[i].mesh = group.meshes[i];
            combine[i].transform = group.transforms[i].localToWorldMatrix;
        }

        var mesh = new Mesh { indexFormat = UnityEngine.Rendering.IndexFormat.UInt32 };
        mesh.CombineMeshes(combine, true);
        mesh.Optimize();

        for (int i = 0; i < group.meshes.Count; i++)
        {
            if (group.transforms[i] != null)
            {
                Destroy(group.transforms[i].gameObject);
            }
        }

        return mesh;
    }
}
