using System.Collections.Generic;
using UnityEngine;

public class AC_ShampooParticleCollision : MonoBehaviour
{
    public GameObject foamEffectPrefab;        // Foam prefab to spawn (only on first hit)
    public float foamLifetime = 2f;            // Auto-destroy foam after this time
    public string spongeTag = "Sponge";        // Tag your sponge GameObject with this

    private ParticleSystem ps;
    private List<ParticleCollisionEvent> collisionEvents;
    private bool foamTriggered = false;       // To track if foam has been triggered

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        collisionEvents = new List<ParticleCollisionEvent>();
    }

    void OnParticleCollision(GameObject other)
    {
        // Check if the collision is with the sponge
        if (!other.CompareTag(spongeTag)) return;

        // If foam has already been triggered, do nothing
        if (foamTriggered) return;

        collisionEvents.Clear();
        int numEvents = ps.GetCollisionEvents(other, collisionEvents);

        for (int i = 0; i < numEvents; i++)
        {
            Vector3 hitPos = collisionEvents[i].intersection;

            // Trigger foam only on the first hit
            if (foamEffectPrefab != null)
            {
                GameObject foam = Instantiate(foamEffectPrefab, hitPos, Quaternion.identity);
                Destroy(foam, foamLifetime); // Auto-destroy foam after X seconds
            }

            // Mark foam as triggered, so no more foam will spawn
            foamTriggered = true;
        }
    }
}
