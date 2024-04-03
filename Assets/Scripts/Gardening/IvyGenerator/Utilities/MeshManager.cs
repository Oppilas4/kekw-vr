using System.Collections.Generic;
using UnityEngine;

public class MeshGroup
{
    public string materialName;
    public Color color;
    public Color colorEnd;
    public List<Mesh> meshes;
    public List<Transform> transforms;

    public MeshGroup(string materialName, Color materialColor, Color materialColorEnd)
    {
        this.materialName = materialName;
        this.color = materialColor;
        this.colorEnd = materialColorEnd;
        this.meshes = new List<Mesh>();
        this.transforms = new List<Transform>();
    }
}

// public class MeshChunk {
//     public Mesh mesh;
//     public Transform transform;

//     public MeshChunk(Mesh mesh, Transform transform) {
//         this.mesh = mesh;
//         this.transform = transform;
//     }
// }

public class MeshManager : Singleton<MeshManager>
{
    Dictionary<string, MeshGroupRenderer> meshGroupRenderers;
    GameObject meshParent;

    public void AddMesh(Transform t, Mesh mesh, Material material)
    {
        if (meshParent == null)
        {
            meshParent = new GameObject("meshParent");
        }

        if (meshGroupRenderers == null)
        {
            meshGroupRenderers = new Dictionary<string, MeshGroupRenderer>();
        }

        if (meshGroupRenderers.ContainsKey(material.name))
        {
            meshGroupRenderers[material.name].Add(t, mesh, material);
        }
        else
        {
            GameObject render = new GameObject("meshGroup - " + material.name);
            Debug.Log("new object:" + material.name);
            render.transform.SetParent(meshParent.transform);

            MeshFilter mFilter = render.AddComponent<MeshFilter>();
            MeshRenderer mRenderer = render.AddComponent<MeshRenderer>();

            MeshGroupRenderer groupRenderer = render.AddComponent<MeshGroupRenderer>();
            groupRenderer.meshFilter = mFilter;
            groupRenderer.meshRenderer = mRenderer;
            groupRenderer.Add(t, mesh, material);
            meshGroupRenderers.Add(material.name, groupRenderer);
        }

    }

    public void CombineAll()
    {
        if (meshGroupRenderers != null)
        {
            foreach (var group in meshGroupRenderers)
            {
                group.Value.CombineAndRender();
            }
            meshGroupRenderers.Clear();
            Resources.UnloadUnusedAssets();
        }
    }

}
