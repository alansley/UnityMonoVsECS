using UnityEngine;
using Unity.Entities;

/// <summary>
/// ECS Authoring class for our spawner.
/// </summary>
public class EcsSpawnerAuthoring : MonoBehaviour
{
    [SerializeField] public GameObject ParticlePrefab;
    [SerializeField] public int NumParticles;
    [SerializeField] public Vector2 SpawnRangeX;
    [SerializeField] public Vector2 SpawnRangeY;
    [SerializeField] public Vector2 SpawnRangeZ;

    // Values for how many entities to spawn when we ask for few/many/lots
    [SerializeField] public int FewSpawnCount   = 1000;
    [SerializeField] public int ManySpawnCount  = 10000;
    [SerializeField] public int LotsSpawnCount  = 100000;
}

/// <summary>
/// ECS Baker to create an entity version of our spawner using the above authoring data.
/// </summary>
public class EcsSpawnerBaker : Baker<EcsSpawnerAuthoring>
{
    public override void Bake(EcsSpawnerAuthoring authoring)
    {
        // Get a new entity..
        var entity = GetEntity(TransformUsageFlags.None);

        // ..then add the spawner component to it, setting all our `EcsSpawnerComponent` data as we do so.
        AddComponent(entity, new EcsSpawnerComponent()
        {
            PrefabEntity      = GetEntity(authoring.ParticlePrefab, TransformUsageFlags.Dynamic), // Note: We get a ENTITY version of our prefab!
            InitialSpawnCount = authoring.NumParticles,
            SpawnRangeX       = authoring.SpawnRangeX,
            SpawnRangeY       = authoring.SpawnRangeY,
            SpawnRangeZ       = authoring.SpawnRangeZ,
            FewSpawnCount     = authoring.FewSpawnCount,
            ManySpawnCount    = authoring.ManySpawnCount,
            LotsSpawnCount    = authoring.LotsSpawnCount
        });
    }
}