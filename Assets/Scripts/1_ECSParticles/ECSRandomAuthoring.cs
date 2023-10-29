using Unity.Entities;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class ECSRandomAuthoring : MonoBehaviour
{
    public uint RandomSeed;
}

public class ECSRandomAuthoringBaker : Baker<ECSRandomAuthoring>
{
    public override void Bake(ECSRandomAuthoring authoring)
    {
        // Get the entity we'll be adding our component to..
        Entity e = GetEntity(authoring, TransformUsageFlags.Dynamic);

        // ..then add it!
        AddComponent(e, new ECSRandomComponent()
        {
            Random = Random.CreateFromIndex(authoring.RandomSeed)
        });
    }
}