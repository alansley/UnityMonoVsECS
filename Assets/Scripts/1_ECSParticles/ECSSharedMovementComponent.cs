using Unity.Entities;
using Unity.Mathematics;

public class ECSParticleMovementComponent : IComponentData
{
    public float3 Velocity = new float3(0f, 0f, -0.1f); // Move along the negative Z-axis
}