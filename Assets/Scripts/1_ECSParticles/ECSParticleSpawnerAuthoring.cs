using Unity.Entities;
using UnityEngine;

/// <summary>
/// ECS Authoring class for our cube spawner.
/// </summary>
public class ECSParticleSpawnerAuthoring : MonoBehaviour
{
    [SerializeField] public GameObject ParticlePrefab;
    [SerializeField] public int NumParticles;
    [SerializeField] public Vector2 SpawnRangeX;
    [SerializeField] public Vector2 SpawnRangeY;
    [SerializeField] public Vector2 SpawnRangeZ;
    [SerializeField] public bool MoveParticles;
}

/// <summary>
/// ECS Baker that creates new entities from our authoring class.
/// </summary>
public class ECSParticleSpawnerBaker : Baker<ECSParticleSpawnerAuthoring>
{
    public override void Bake(ECSParticleSpawnerAuthoring authoring)
    {
        // Get the entity we'll be adding our component to..
        Entity e = GetEntity(authoring, TransformUsageFlags.Dynamic);

        AddComponent(e, new ECSParticleSpawnerComponent()
        {
            ParticleEntity = GetEntity(authoring.ParticlePrefab),
            NumParticles   = authoring.NumParticles,
            SpawnRangeX    = authoring.SpawnRangeX,
            SpawnRangeY    = authoring.SpawnRangeY,
            SpawnRangeZ    = authoring.SpawnRangeZ,
            MoveParticles  = authoring.MoveParticles
        });
    }
}