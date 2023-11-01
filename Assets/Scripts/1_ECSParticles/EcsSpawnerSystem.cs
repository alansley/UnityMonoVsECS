using System;
using System.Linq;
using UnityEngine;
using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Unity.Transforms;
using Random = UnityEngine.Random;

[BurstCompile]
public partial struct EcsSpawnerSystem : ISystem
{
    private NativeArray<Entity> instances;

    private bool _createdStuff;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        // CAREFUL: We can't use this `OnCreate` like a `Start` method to spawn all our entities - which is odd. Try it
        // if you like, any `SystemAPI.Query<RefRO<EcsSpawnerComponent>>` or such returns nothing from the query.

        _createdStuff = false;
    }

    // NOTE: Since Entities v1.0.14 we no longer have to implement every function (even if empty).
    // SEE: https://docs.unity3d.com/Packages/com.unity.entities@1.0/manual/upgrade-guide.html#update-isystem
    //[BurstCompile]
    //public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        if (!_createdStuff)
        {

            // Find our spawner and call our method to spawn all the entities on it.
            // Note: We aren't modifying the `EcsSpawnComponent` data so we can use `RefRO` in this instance (we'd need to
            // use `RefRW` if we wanted to adjust values).
            // Also: This will find ALL spawners - at present we just have a single spawner, but this seems to be the way to
            // do things in DOTS/ECS so we'll just go with it, I guess. If we try to query outside of a `ForEach` we get the
            // following error:
            // error SGFE001: Invocations of `SystemAPI.Query<T>() are currently supported only if they take place inside `foreach` statements.
            foreach (RefRO<EcsSpawnerComponent> spawnerComponent in SystemAPI.Query<RefRO<EcsSpawnerComponent>>())
            {
                Debug.Log("Wang! OnUpdate!");
                SpawnAllEntities(ref state, spawnerComponent);
            }

            _createdStuff = true;
        }

        // Stupidest way of doing things ever - have an update method that runs constantly but we have to kill it
        // ourselves if we want it to execute just once. WTF...
        //state.Enabled = false;
    }

    private void SpawnAllEntities(ref SystemState state, RefRO<EcsSpawnerComponent> spawnerComponent)
    {
        int spawnCount = spawnerComponent.ValueRO.InitialSpawnCount;

        // While we could spawn-and-set each entity in turn inside a loop, we'll choose to spawn ALL our entities at
        // once here and then go through setting details on them in the loop below.
        var allSpawnedEntities =  state.EntityManager.Instantiate(spawnerComponent.ValueRO.PrefabEntity, spawnCount, Allocator.Temp);

        // Because I've decided to use the exact same prefab in the DOTS/ECS spawner as the MonoBehaviour once, we need
        // to add a `URPMaterialPropertyBaseColor` to allow us to easily override each entity's material colour
        // (although we could use a prefab with an instance of this already on it, rather than adding an instance here,
        // if we wanted.
        //state.EntityManager.AddComponent<URPMaterialPropertyBaseColor>(allSpawnedEntities);

        foreach (var entity in allSpawnedEntities)
        {
            // Get a random position..
            float3 randomPositionWS = new float3
            {
                x = Random.Range(spawnerComponent.ValueRO.SpawnRangeX.x, spawnerComponent.ValueRO.SpawnRangeX.y),
                y = Random.Range(spawnerComponent.ValueRO.SpawnRangeY.x, spawnerComponent.ValueRO.SpawnRangeY.y),
                z = Random.Range(spawnerComponent.ValueRO.SpawnRangeZ.x, spawnerComponent.ValueRO.SpawnRangeZ.y)
            };

            // ..and assign it to the entity.
            // Note: LocalPosition.FromPosition returns a Transform initialized with the given position and default rotation & scale
            state.EntityManager.SetComponentData(entity, LocalTransform.FromPosition(randomPositionWS));

            // Generate a random colour and set it
            var randomColour = GetRandomColour();
            var urpMaterialPropertyBaseColor = new URPMaterialPropertyBaseColor() { Value = randomColour };
            //var urpMaterialPropertyBaseColor = state.EntityManager.GetComponentObject<URPMaterialPropertyBaseColor>(entity);
            //urpMaterialPropertyBaseColor.Value = randomColour;
            state.EntityManager.SetComponentData(entity, urpMaterialPropertyBaseColor);


        }
    }

    private void ProcessSpawner(ref SystemState state, RefRW<EcsSpawnerComponent> spawnerComponent)
    {
        // Spawn a new entity using the `PrefabEntity` in the `EcsSpawnerComponent` (set via baking in the `EcsSpawnerAuthoring` class)
        Entity newEntity = state.EntityManager.Instantiate(spawnerComponent.ValueRO.PrefabEntity);

        // Get a random position..
        float3 randomPosition = new float3
        {
            x = Random.Range(spawnerComponent.ValueRO.SpawnRangeX.x, spawnerComponent.ValueRO.SpawnRangeX.y),
            y = Random.Range(spawnerComponent.ValueRO.SpawnRangeY.x, spawnerComponent.ValueRO.SpawnRangeY.y),
            z = Random.Range(spawnerComponent.ValueRO.SpawnRangeZ.x, spawnerComponent.ValueRO.SpawnRangeZ.y)
        };

        // ..and assign it to the entity.
        // Note: LocalPosition.FromPosition returns a Transform initialized with the given position.
        state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(randomPosition));

        /*

        // If the next spawn time has passed.
        if (spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
        {
            // Spawns a new entity and positions it at the spawner.
            Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
            // LocalPosition.FromPosition returns a Transform initialized with the given position.
            state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(spawner.ValueRO.SpawnPosition));

            // Resets the next spawn time.
            spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;
        }
        */
    }












    /*
    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var startTime = DateTime.UtcNow;

        // Get the random number generator in read/write mode because we mutate state with each new random number
        var rng = SystemAPI.GetSingletonRW<ECSRandomComponent>();

        // Get the spawner data in read-only mode
        var particleSpawnerComponent = SystemAPI.GetSingleton<EcsSpawnerComponent>();

        var spawnRangeX = particleSpawnerComponent.SpawnRangeX;
        var spawnRangeY = particleSpawnerComponent.SpawnRangeY;
        var spawnRangeZ = particleSpawnerComponent.SpawnRangeZ;

        var prefab = SystemAPI.GetSingleton<EcsSpawnerComponent>().Prefab;

        instances = state.EntityManager.Instantiate(prefab, particleSpawnerComponent.InitialSpawnCount, Allocator.Temp);

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
        Debug.Log($"Total spawn time for {particleSpawnerComponent.InitialSpawnCount} ECS entity particles: {spawnDurationMS}ms.");

        // Disable this system after we've ran it to spawn the entities
        state.Enabled = false;
    }
    */

    [BurstCompile]
    private float4 GetRandomColour()
    {
        float r = Random.value;
        float g = Random.value;
        float b = Random.value;
        float a = 1f;
        return new float4(r, g, b, a);
    }

}