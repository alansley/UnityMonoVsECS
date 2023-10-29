using Unity.Entities;
using Unity.Mathematics;

public struct ECSParticleComponent : IComponentData
{
    public float3 Velocity;
}