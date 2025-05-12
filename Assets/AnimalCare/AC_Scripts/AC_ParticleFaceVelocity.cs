using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class AC_ParticleFaceVelocity: MonoBehaviour
{
    private ParticleSystem ps;
    private ParticleSystem.Particle[] particles;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[ps.main.maxParticles];
    }

    void LateUpdate()
    {
        int count = ps.GetParticles(particles);
        Quaternion parentRotation = transform.parent.rotation;

        for (int i = 0; i < count; i++)
        {
            particles[i].rotation3D = parentRotation.eulerAngles * Mathf.Deg2Rad;
        }

        ps.SetParticles(particles, count);
    }
}
