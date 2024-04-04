using UnityEngine;

public class MeshGroupRenderer : MonoBehaviour
{
    private const string COLOR = "_Color";
    private const string COLOREND = "_ColorEnd";

    public MeshFilter meshFilter;
    public MeshRenderer meshRenderer;
    private MeshGroup _meshGroup;
    private Material _lastAddedMaterial;

    public bool Add(Transform t, Mesh mesh, Material material)
    {
        Color color = material.GetColor(COLOR);
        Color colorEnd = material.GetColor(COLOREND);
        if (_meshGroup == null)
        {
            _meshGroup = new MeshGroup(material.name, color, colorEnd);
        }
        _meshGroup.meshes.Add(mesh);
        _meshGroup.transforms.Add(t);
        _lastAddedMaterial = material;
        return true;
    }

    public void CombineAndRender()
    {
        if (_meshGroup == null) return;
        Mesh mesh = CombineMeshes(_meshGroup);
        meshFilter.mesh = mesh;
        meshRenderer.material = _lastAddedMaterial;
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
            if (group.transforms[i] == null) continue;
            Destroy(group.transforms[i].gameObject);
        }

        return mesh;
    }
}
