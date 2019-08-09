using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
public struct Movement : IComponentData
{
    public float3 PositionValue
    { get => new float3(MoveVal.x, 0, MoveVal.y) * SpeedMultiplier; }
    public float2 MoveVal;
    public int SpeedMultiplier;
}