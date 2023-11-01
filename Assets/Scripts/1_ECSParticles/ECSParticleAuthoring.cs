using UnityEngine;
using Unity.Entities;
using Unity.Rendering;

public class ECSParticleAuthoring : MonoBehaviour { }

public class ECSParticleCubeBaker : Baker<ECSParticleAuthoring>
{
    public override void Bake(ECSParticleAuthoring authoring)
    {
        // Get the entity we'll be adding our component to..
        Entity e = GetEntity(authoring, TransformUsageFlags.Dynamic);

        // Add a base colour override
        AddComponent(e, new URPMaterialPropertyBaseColor());
    }
}