using System;
using UnityEngine;

using Random = UnityEngine.Random;

/// <summary>
/// Simple prefab spawner - spawns X-many prefabs at random locations and sets a random material colour on them.
/// </summary>
public class ParticleSpawnerMono : Singleton<ParticleSpawnerMono>
{
    /// <summary>
    /// The prefab we're spawning.
    /// </summary>
    [SerializeField] private GameObject _prefabToSpawn;

    /// <summary>
    /// How many instances to spawn.
    /// </summary>
    [SerializeField] private int _numParticles;

    /// <summary>
    /// RNG seed used for reproducibility between runs.
    /// </summary>
    [SerializeField] private int _rngSeed = 123;

    /// <summary>
    /// World-space spawn position ranges.
    /// </summary>
    [SerializeField] private Vector2 _spawnRangeX;
    [SerializeField] private Vector2 _spawnRangeY;
    [SerializeField] private Vector2 _spawnRangeZ;
    public Vector2 SpawnRangeZ => _spawnRangeZ;

    /// <summary>
    /// Whether or not we should animate / move the spawned prefabs.
    /// </summary>
    [SerializeField] private bool _moveParticles = false;
    public bool MoveParticles => _moveParticles;

    [SerializeField] private float _particleMovementSpeed = 0.1f;
    public float ParticleMovementSpeed => _particleMovementSpeed;

    /// <summary>
    /// Unity Start hook.
    /// </summary>
    void Start()
    {
        // Make a note of our start time and seed the random number generator
        var startTime = DateTime.UtcNow;
        Random.InitState(_rngSeed);

        // Create all the prefabs
        for (int i = 0; i < _numParticles; ++i)
        {
            // Choose a random world-space position
            float x = Random.Range(_spawnRangeX.x, _spawnRangeX.y);
            float y = Random.Range(_spawnRangeY.x, _spawnRangeY.y);
            float z = Random.Range(_spawnRangeZ.x, _spawnRangeZ.y);
            var positionWS = new Vector3(x, y, z);

            // Instantiate the prefab
            var particle = Instantiate(_prefabToSpawn, positionWS, Quaternion.identity);

            // Set a random colour on it
            particle.GetComponent<MeshRenderer>().material.color = GetRandomColour();
        }

        // Print spawn duration (all particles)
        var endTime = DateTime.UtcNow;
        var spawnDurationMS = (endTime - startTime).TotalMilliseconds;
        var friendlyDurationMS = String.Format("{0:.###}", spawnDurationMS);
        Debug.Log($"Total spawn time for {_numParticles} particles: {friendlyDurationMS}ms.");
    }

    /// <summary>
    /// Method to return a new random colour where each RGB components is in the range [0..1] and is fully opaque.
    /// </summary>
    /// <returns>A new random colour where each RGB components is in the range [0..1] and is fully opaque</returns>
    private Color GetRandomColour()
    {
        float r = Random.value;
        float g = Random.value;
        float b = Random.value;
        float a = 1f;
        return new Color(r, g, b, a);
    }
}