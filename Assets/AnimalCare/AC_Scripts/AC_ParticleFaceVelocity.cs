using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class AC_ParticleFaceVelocity : MonoBehaviour
{
    ParticleSystem ps;
    ParticleSystem.Particle[] particles;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[ps.main.maxParticles];
    }

    void LateUpdate()
    {
        int count = ps.GetParticles(particles);

        for (int i = 0; i < count; i++)
        {
            Vector3 velocity = particles[i].velocity;
            if (velocity.sqrMagnitude > 0.001f)
            {
                float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
                particles[i].rotation = -angle; // flip if needed
            }
        }

        ps.SetParticles(particles, count);
    }
}
