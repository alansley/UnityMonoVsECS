using Unity.Entities;
using Unity.Mathematics;

/// <summary>
/// The component data for our ECS spawner defines the prefab we'll spawn, how many instances of the prefab we'll spawn,
/// and the position range of where we'll place each spawned entity.
/// </summary>
public struct EcsSpawnerComponent : IComponentData
{
    public Entity PrefabEntity;
    public int    InitialSpawnCount;
    public float2 SpawnRangeX;
    public float2 SpawnRangeY;
    public float2 SpawnRangeZ;

    // Values for how many entities to spawn when we ask for few/many/lots (typically 1000/10,000/100,000 in this example)
    public int FewSpawnCount;
    public int ManySpawnCount;
    public int LotsSpawnCount;
}