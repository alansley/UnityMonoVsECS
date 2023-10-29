using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;

public class ECSParticleAuthoring : MonoBehaviour
{
    public Vector3 Velocity;
}

public class ECSParticleCubeBaker : Baker<ECSParticleAuthoring>
{
    public override void Bake(ECSParticleAuthoring authoring)
    {
        // Get the entity we'll be adding our component to..
        Entity e = GetEntity(authoring, TransformUsageFlags.Dynamic);

        // Add a velocity component
        AddComponent(e, new ECSParticleComponent()
        {
            Velocity = new float3(0f, 0f, 0.1f)
        });

        // Add a base colour override
        AddComponent(e, new URPMaterialPropertyBaseColor());
    }
}