using Unity.Entities;
using Random = Unity.Mathematics.Random;

public struct ECSRandomComponent : IComponentData
{
    public Random Random;
}