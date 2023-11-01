using Unity.Entities;
using UnityEngine;

/// <summary>
/// ECS Authoring class for our spawner.
/// </summary>
public class ECSParticleSpawnerAuthoring : MonoBehaviour
{
    [SerializeField] public GameObject ParticlePrefab;
    [SerializeField] public int NumParticles;
    [SerializeField] public Vector2 SpawnRangeX;
    [SerializeField] public Vector2 SpawnRangeY;
    [SerializeField] public Vector2 SpawnRangeZ;

    [SerializeField] private int _fewObjectsCount = 1000;
    [SerializeField] private int _manyObjectsCount = 10000;
    [SerializeField] private int _lotsObjectsCount = 100000;
}

/// <summary>
/// ECS Baker that creates new entities from our authoring class.
/// </summary>
public class ECSParticleSpawnerBaker : Baker<ECSParticleSpawnerAuthoring>
{
    public override void Bake(ECSParticleSpawnerAuthoring authoring)
    {
        // Get the entity we'll be adding our components to..
        Entity e = GetEntity(authoring, TransformUsageFlags.Dynamic);

        // ..and an entity for the prefab..
        Entity particlePrefabEntity = GetEntity(authoring.ParticlePrefab, TransformUsageFlags.Dynamic);

        // ..then add all the components.
        AddComponent(e, new ECSParticleSpawnerComponent()
        {
            ParticleEntity = particlePrefabEntity,
            NumParticles   = authoring.NumParticles,
            SpawnRangeX    = authoring.SpawnRangeX,
            SpawnRangeY    = authoring.SpawnRangeY,
            SpawnRangeZ    = authoring.SpawnRangeZ
        });
    }
}