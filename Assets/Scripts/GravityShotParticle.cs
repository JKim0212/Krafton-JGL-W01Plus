using UnityEngine;

public class GravityShotParticle : MonoBehaviour
{
    public Transform blackHoleCenter;
    public float pullForce = 5f;

    private ParticleSystem particleSystem;
    private ParticleSystem.Particle[] particles;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (particleSystem == null) return;

        int particleCount = particleSystem.particleCount;
        if (particleCount <= 0) return;

        if (particles == null || particles.Length < particleCount)
        {
            particles = new ParticleSystem.Particle[particleCount];
        }

        particleSystem.GetParticles(particles);

        for (int i = 0; i < particleCount; i++)
        {
            Vector3 direction = -particles[i].position.normalized;
            particles[i].velocity = direction * pullForce;
        }

        particleSystem.SetParticles(particles, particleCount);
    }
}
