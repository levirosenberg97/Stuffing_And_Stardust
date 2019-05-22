using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(ParticleSystem))]
public class ParticleOrbit : MonoBehaviour
{

    ParticleSystem system;
    ParticleSystem.Particle[] particles;

    public Transform COG;

    public float orbitStrength;
    float timer;

	void Start ()
    {
        if (system == null)
        {
            system = GetComponent<ParticleSystem>();
        }

        if(particles == null || particles.Length < system.main.maxParticles)
        {
            particles = new ParticleSystem.Particle[system.main.maxParticles];
        }

        if (COG == null)
        {
            COG = transform;
        }

        if (system.main.simulationSpace == ParticleSystemSimulationSpace.Local && COG != null)
        {
            Debug.LogWarning("Will only work if particle system space is set to world. Currently using the center of the particle  effect", this);
        }

	}

    private void OnDisable()
    {
        timer = 0;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        
        if(timer >= system.main.startLifetime.constantMax - .5f)
        {
            gameObject.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        int numParticlesAlive = system.GetParticles(particles);

        for(int i = 0; i < numParticlesAlive; i++)
        {
            Vector3 gravitation = new Vector3(0, 0, 0);

            if(system.main.simulationSpace == ParticleSystemSimulationSpace.World)
            {
                gravitation = COG.position - particles[i].position;
            }
            else
            {
                gravitation = Vector3.zero - particles[i].position;
            }

            Vector3 normalized = Vector3.Normalize(gravitation);
            particles[i].position += normalized * orbitStrength ;
        }

        system.SetParticles(particles, numParticlesAlive);
    }
}
