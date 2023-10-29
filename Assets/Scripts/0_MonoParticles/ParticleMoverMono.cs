using UnityEngine;

public class ParticleMoverMono : MonoBehaviour
{
    private ParticleSpawnerMono _particleSpawnerMono;

    /// <summary>
    /// Unity Start hook.
    /// </summary>
    void Start()
    {
        _particleSpawnerMono = ParticleSpawnerMono.Instance; // Keep the ref rather than finding it each frame
    }

    // Update is called once per frame
    void Update()
    {
        // Bail early if we're not moving the particles
        if (!_particleSpawnerMono.MoveParticles) return;

        // Otherwise move the particle by the spawner-specified movement speed and wrap it around as req'd
        var tempPos = this.transform.position;
        tempPos.z -= _particleSpawnerMono.ParticleMovementSpeed;
        if (tempPos.z < _particleSpawnerMono.SpawnRangeZ.x) tempPos.z = _particleSpawnerMono.SpawnRangeZ.y;
        this.transform.position = tempPos;
    }
}