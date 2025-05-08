using System.Collections.Generic;
using UnityEngine;

public class AC_ShampooParticleCollision : MonoBehaviour
{
    public GameObject foamEffectPrefab; // Optional: spawn on impact
    public string spongeTag = "Sponge"; // Match the tag on your sponge

    private ParticleSystem ps;
    private List<ParticleCollisionEvent> collisionEvents;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        if (!other.CompareTag(spongeTag)) return;

        // Clear and reuse the list to avoid GC
        collisionEvents.Clear();
        int numEvents = ps.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < numEvents; i++)
        {
            Vector3 hitPos = collisionEvents[i].intersection;

            // Optional: spawn foam or splash effect
            if (foamEffectPrefab != null)
            {
                Instantiate(foamEffectPrefab, hitPos, Quaternion.identity);
            }

            // Note: Particle is automatically destroyed via Lifetime Loss = 1.0 in Collision module
        }
    }
}
