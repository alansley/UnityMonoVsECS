using Unity.Entities;
using Unity.Mathematics;

public struct ECSParticleSpawnerComponent : IComponentData
{
    public Entity ParticleEntity;
    public int    NumParticles;
    public float2 SpawnRangeX;
    public float2 SpawnRangeY;
    public float2 SpawnRangeZ;
}