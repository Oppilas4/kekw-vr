using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AC_DogWashing : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private Mesh mesh;
    private Color[] colors;
    private Vector3[] vertices;

    private Color wetColor = new Color(0.3f, 0.15f, 0.035f); // Tumma ruskea m‰rk‰ v‰ri
    private Color dryColor = Color.white; // Kuiva v‰ri
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private float wetnessAmount = 0f;
    private const float hitRadiusSqr = 0.002f * 0.002f; //hit area size
    private const float wetnessThreshold = 2f; // M‰‰r‰, jolla koko koira muuttuu m‰r‰ksi

    void Start()
    {
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        if (skinnedMeshRenderer == null)
        {
            Debug.LogError("SkinnedMeshRenderer not found!");
            return;
        }

        mesh = skinnedMeshRenderer.sharedMesh;
        vertices = mesh.vertices;

        colors = (mesh.colors != null && mesh.colors.Length == vertices.Length)
            ? mesh.colors
            : new Color[vertices.Length];

        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = dryColor; // Aluksi kuiva v‰ri
        }

        mesh.colors = colors;
    }

    void OnParticleCollision(GameObject other)
    {
        if (!other.CompareTag("Water")) return;

        ParticleSystem ps = other.GetComponent<ParticleSystem>();
        if (ps == null) return;

        int numCollisions = ps.GetCollisionEvents(gameObject, collisionEvents);
        for (int i = 0; i < numCollisions; i++)
        {
            PaintVertex(collisionEvents[i].intersection);
        }

        // Varmistetaan, ett‰ v‰ri p‰ivittyy vain kerran, kun m‰rkyys ylitt‰‰ rajan
        if (wetnessAmount >= wetnessThreshold)
        {
            SetFullWetColor(); // Asetetaan koko koira m‰r‰ksi
        }

        mesh.colors = colors; // P‰ivitet‰‰n v‰rit
        Debug.Log($"Wetness: {wetnessAmount:F4}");
    }

    void PaintVertex(Vector3 hitPoint)
    {
        Vector3 localHitPoint = transform.InverseTransformPoint(hitPoint);

        for (int i = 0; i < vertices.Length; i++)
        {
            float distSqr = (vertices[i] - localHitPoint).sqrMagnitude;
            if (distSqr < hitRadiusSqr)
            {
                colors[i] = Color.Lerp(colors[i], wetColor, 0.5f);
                wetnessAmount += 1f / vertices.Length; // Kasvata wetnessAmountia, kun osumia tulee
            }
        }
    }

    void SetFullWetColor()
    {
        // Kun m‰rkyys ylitt‰‰ rajan, koko koira saa m‰r‰n v‰rin
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.Lerp(colors[i], wetColor, 0.05f);
        }
        Debug.Log("The dog is fully wet!");
    }

}
