using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_DogWashing : MonoBehaviour
{
    private MeshFilter meshFilter;
    private Mesh mesh;
    private Color[] colors;
    private float wetnessAmount = 0;
    private float trimmedAmount = 0;

    Color wetColor = new Color(0.3f, 0.15f, 0.035f); // Tumma ruskea m‰r‰lle alueelle, chancing value can adjust color
    Color trimColor = new Color(1.0f, 0.8f, 0.86f);
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();


    void Start()
    {
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        if (skinnedMeshRenderer != null)
        {
            Mesh mesh = skinnedMeshRenderer.sharedMesh;

            if (mesh.colors == null || mesh.colors.Length > 0)
            {
                colors = new Color[mesh.vertexCount];

                // Alusta vertex colorit kuiviksi (valkoiseksi)
                for (int i = 0; i < colors.Length; i++)
                {
                    Debug.Log(" Color placed White");
                    colors[i] = Color.white; // Aseta alkuv‰ri, esim. punaiseksi
                }

                mesh.colors = colors;
            }
        }
        else
        {
            Debug.LogError("SkinnedMeshRenderer component not found!");
        }
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Water")) // Aseta vesi-partikkelille "Water"-tagi
        {
            ParticleSystem ps = other.GetComponent<ParticleSystem>();

            if (ps != null)
            {
                int numCollisions = ps.GetCollisionEvents(gameObject, collisionEvents);
                //Debug.Log("Number of Collisions: " + numCollisions);
                for (int i = 0; i < numCollisions; i++)
                {
                    PaintVertex(collisionEvents[i].intersection);
                }
            }
        }
        if (other.CompareTag("Invisible")) // Aseta vesi-partikkelille "Water"-tagi
        {
            ParticleSystem ps = other.GetComponent<ParticleSystem>();

            if (ps != null)
            {
                int numCollisions = ps.GetCollisionEvents(gameObject, collisionEvents);
                //Debug.Log("Number of Collisions: " + numCollisions);
                for (int i = 0; i < numCollisions; i++)
                {
                    PaintTrimVertex(collisionEvents[i].intersection);
                }
            }
        }
    }

    void PaintVertex(Vector3 hitPoint)
    {
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        if (skinnedMeshRenderer == null) return;

        Mesh mesh = skinnedMeshRenderer.sharedMesh; // K‰ytet‰‰n sharedMesh
        Vector3[] vertices = mesh.vertices;
        Color[] colors = mesh.colors;

        if (colors.Length == 0)
        {
            colors = new Color[vertices.Length];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = Color.white; // Alusta kuiva v‰ri
            }
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            float randomFactor = Random.Range(0.75f, 1f);
            Vector3 worldPos = transform.TransformPoint(vertices[i]);
            if (Vector3.Distance(hitPoint, worldPos) < .09f) // Osumiss‰de
            {
                colors[i] = Color.Lerp(colors[i], wetColor * randomFactor, .15f);
                //Debug.Log("Vertex " + i + " changed to blue");
                wetnessAmount = wetnessAmount + i / 1000000f;
            }
        }

        mesh.colors = colors; // P‰ivit‰ v‰rit
        skinnedMeshRenderer.sharedMesh = mesh; // Pakota p‰ivitys
        //Debug.Log(wetnesAmount);
    }
    void PaintTrimVertex(Vector3 hitPoint)
    {
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        if (skinnedMeshRenderer == null) return;

        Mesh mesh = skinnedMeshRenderer.sharedMesh; // K‰ytet‰‰n sharedMesh
        Vector3[] vertices = mesh.vertices;
        Color[] colors = mesh.colors;

        if (colors.Length == 0)
        {
            colors = new Color[vertices.Length];
            for (int i = 0; i < colors.Length; i++)
            {
                colors[i] = Color.white; // Alusta kuiva v‰ri
            }
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            float randomFactor = Random.Range(0.75f, 1f);
            Vector3 worldPos = transform.TransformPoint(vertices[i]);
            if (Vector3.Distance(hitPoint, worldPos) < .09f) // Osumiss‰de
            {
                colors[i] = Color.Lerp(colors[i], trimColor * randomFactor, .15f);
                //Debug.Log("Vertex " + i + " changed to blue");
                trimmedAmount = trimmedAmount + i / 1000000f;
            }
        }

        mesh.colors = colors; // P‰ivit‰ v‰rit
        skinnedMeshRenderer.sharedMesh = mesh; // Pakota p‰ivitys
        //Debug.Log(wetnesAmount);
    }
}
