using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AC_DogWashing : MonoBehaviour
{
    private SkinnedMeshRenderer skinnedMeshRenderer;
    private Mesh mesh;
    private Color[] colors;
    private Vector3[] vertices;

    public Material[] dogMaterials;

    private Color wetColor = new Color(0.3882f, 0.2941f, 0.2314f); // Tumma ruskea m‰rk‰ v‰ri
    private Color trimColor = Color.white; // trimmed v‰ri
    private Color dryColor = new Color(0.773f, 0.502f, 0.294f); // Kuiva v‰ri
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>();

    private float wetnessAmount = 0f;
    private const float hitRadiusSqr = 0.0015f * 0.0015f; //hit area size
    private const float wetnessThreshold = 2f; // M‰‰r‰, jolla koko koira muuttuu m‰r‰ksi
    private const float dryThreshold = 0.1f;   // Kuivuusraja

    private float trimAmount = 0f;
    private const float trimThreshold = 2f; // M‰‰r‰, jolla koko koira muuttuu m‰r‰ksi

    public GameObject dripping;
    ParticleSystem waterDripping;

    public AC_Soap soap;
    public AC_DogMovement dog;
    public AC_ChecklistManager checklistManager;

    public bool wet = false;
    public bool wet1 = false;
    public bool wet2 = false;
    public bool notTrimmed = true;

    private Material mat;

    void Start()
    {
        waterDripping = dripping.GetComponent<ParticleSystem>();
        skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer>();
        mat = skinnedMeshRenderer.material;
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
        Debug.Log("Playing Start");
    }
    void OnEnable()
    {
        // Randomize dog material
        if (skinnedMeshRenderer != null && dogMaterials.Length > 0)
        {
            Material randomMat = dogMaterials[Random.Range(0, dogMaterials.Length)];
            skinnedMeshRenderer.material = randomMat;
        }
        mat = skinnedMeshRenderer.material;
        trimAmount = 0f;
        SetDryColorImmediately();
    }
    void OnParticleCollision(GameObject other)
    {
        ParticleSystem ps = other.GetComponent<ParticleSystem>();
        if (ps == null) return;

        int numCollisions = ps.GetCollisionEvents(gameObject, collisionEvents);
        for (int i = 0; i < numCollisions; i++)
        {
            Vector3 hitPoint = collisionEvents[i].intersection;

            if (other.CompareTag("Water"))
            {
                PaintVertex(hitPoint, "Water"); // Kastelee
            }
            else if (other.CompareTag("Dryer"))
            {
                PaintVertex(hitPoint, "Dryer"); // Kuivattaa
            }
            else if (other.CompareTag("Trimmer"))
            {
                notTrimmed = false;
                PaintVertex(hitPoint, "Trimmer"); // Kuivattaa
            }
        }

        if (wetnessAmount >= wetnessThreshold)
        {
            SetFullWetColor(); // T‰ysin m‰rk‰
        }
        else if (wetnessAmount <= dryThreshold && notTrimmed)
        {
            SetFullDryColor(); // T‰ysin kuiva
        }
        else if (trimAmount >= trimThreshold && !notTrimmed)
        {
            SetFullTrimColor(); // T‰ysin kuiva
        }

        mesh.colors = colors;
        Debug.Log($"Wetness: {wetnessAmount:F4}");
        Debug.Log($"TrimAmount: {trimAmount:F4}");
    }

    void PaintVertex(Vector3 hitPoint, string nametag)
    {
        Vector3 localHitPoint = transform.InverseTransformPoint(hitPoint);

        for (int i = 0; i < vertices.Length; i++)
        {
            float distSqr = (vertices[i] - localHitPoint).sqrMagnitude;
            if (distSqr < hitRadiusSqr)
            {
                if (nametag == "Water")
                {
                    colors[i] = Color.Lerp(colors[i], wetColor, 0.2f);
                    wetnessAmount += 1f / vertices.Length;
                }
                else if (nametag == "Dryer")
                {
                    colors[i] = Color.Lerp(colors[i], dryColor, 0.2f);
                    wetnessAmount -= 1f / vertices.Length;
                }
                else if (nametag == "Trimmer")
                {
                    colors[i] = Color.Lerp(colors[i], trimColor, 0.2f);
                    trimAmount += 1f / vertices.Length;
                    notTrimmed = false;
                }
            }
        }

        wetnessAmount = Mathf.Clamp(wetnessAmount, 0f, wetnessThreshold);
        trimAmount = Mathf.Clamp(trimAmount, 0f, trimThreshold);
    }

    void SetFullWetColor()
    {
        // Kun m‰rkyys ylitt‰‰ rajan, koko koira saa m‰r‰n v‰rin
        for (int i = 0; i < colors.Length; i++)
        {
            dripping.SetActive(true);
            waterDripping.Play();
            wet = true;
            if (!wet1)
            {

                wet1 = true;
            }
            else if (!wet2 && soap.foamed)
            {
                dog.AfterShower();
                wet2 = true;
            }
            colors[i] = Color.Lerp(colors[i], wetColor, 0.025f);
        }
        Debug.Log("The dog is fully wet!");
    }
    void SetFullDryColor()
    {
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.Lerp(colors[i], dryColor, 0.05f);
        }
        Debug.Log("The dog is fully dry!");
        if (wet2) checklistManager.CompleteTask(0);
        wet1 = false;
        wet2 = false;
        wet = false;
        soap.foamed = false;
        waterDripping.Stop();
        dripping.SetActive(false);
    }
    void SetFullTrimColor()
    {
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.Lerp(colors[i], trimColor, 0.1f);
        }
        mat.SetTexture("_BumpMap", null); //delete normal from material 
        Debug.Log("The dog is fully trimmed!");
        checklistManager.CompleteTask(1);
        notTrimmed = true;
    }
    public void SetDryColorImmediately()
    {
        if (colors == null || colors.Length == 0 || mesh == null)
            return;

        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = dryColor;
        }

        mesh.colors = colors;
    }
}
