using UnityEngine;
using Unity.Entities;
using Unity.Rendering;

public class ECSParticleAuthoring : MonoBehaviour { }

public class ECSParticleCubeBaker : Baker<ECSParticleAuthoring>
{
    public override void Bake(ECSParticleAuthoring authoring)
    {
        // Create an entity
        Entity entity = GetEntity(TransformUsageFlags.None);

        // Add a base colour override component
        AddComponent(entity, new URPMaterialPropertyBaseColor());

        // And add a tag to it so we can easily find all our particles via a query
        AddComponent(entity, new ECSParticleComponentTag());
    }
}