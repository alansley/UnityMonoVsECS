using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Entities;
using UnityEngine;

[BurstCompile]
[UpdateAfter(typeof(ECSParticleSpawnerSystem))]
public partial struct ECSParticleMoverSystem : ISystem
{

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        // This adds a condition to the system updating: the system will not
        // update unless at least one entity exists having the given component component.
        state.RequireForUpdate<ECSParticleMovementComponent>();

        //var shouldUpdate = state.EntityManager.Singl
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {



    }
}