using System;

using UnityEngine;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;

[BurstCompile]
public partial struct ECSParticleSpawnerSystem : ISystem
{
    private NativeArray<Entity> instances;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        // This adds a condition to the system updating: the system will not update unless at least one entity
        // exists having the given component.
        state.RequireForUpdate<ECSParticleSpawnerComponent>();
    }

    // NOTE: Since Entities v1.0.14 we no longer have to implement every function (even if empty).
    // SEE: https://docs.unity3d.com/Packages/com.unity.entities@1.0/manual/upgrade-guide.html#update-isystem
    //[BurstCompile]
    //public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var startTime = DateTime.UtcNow;

        // Get the random number generator in read/write mode because we mutate state with each new random number
        var rng = SystemAPI.GetSingletonRW<ECSRandomComponent>();

        // Get the spawner data in read-only mode
        var particleSpawnerComponent = SystemAPI.GetSingleton<ECSParticleSpawnerComponent>();

        var spawnRangeX = particleSpawnerComponent.SpawnRangeX;
        var spawnRangeY = particleSpawnerComponent.SpawnRangeY;
        var spawnRangeZ = particleSpawnerComponent.SpawnRangeZ;

        var prefab = SystemAPI.GetSingleton<ECSParticleSpawnerComponent>().ParticleEntity;

        instances = state.EntityManager.Instantiate(prefab, particleSpawnerComponent.NumParticles, Allocator.Temp);

        foreach (var instance in instances)
        {
            var localTransform = new LocalTransform()
            {
                Position = new float3(rng.ValueRW.Random.NextFloat(spawnRangeX.x, spawnRangeX.y),
                                      rng.ValueRW.Random.NextFloat(spawnRangeY.x, spawnRangeY.y),
                                      rng.ValueRW.Random.NextFloat(spawnRangeZ.x, spawnRangeZ.y)),
                Rotation = Quaternion.identity,
                Scale    = 1f
            };
            state.EntityManager.SetComponentData(instance, localTransform);

            var colour = GetRandomFloat4Colour(rng);
            var colourMaterialProperty = new URPMaterialPropertyBaseColor() { Value = colour };
            state.EntityManager.SetComponentData(instance, colourMaterialProperty);
        }

        var endTime = DateTime.UtcNow;
        var spawnDurationMS = (endTime - startTime).TotalMilliseconds;
        Debug.Log($"Total spawn time for {particleSpawnerComponent.NumParticles} ECS entity particles: {spawnDurationMS}ms.");

        // Disable this system after we've ran it to spawn the entities
        state.Enabled = false;
    }

    [BurstCompile]
    public float4 GetRandomFloat4Colour(RefRW<ECSRandomComponent> random)
    {
        float r = random.ValueRW.Random.NextFloat();
        float g = random.ValueRW.Random.NextFloat();
        float b = random.ValueRW.Random.NextFloat();
        float a = 1f;
        return new float4(r, g, b, a);
    }
}