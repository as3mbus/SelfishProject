using System;
using Unity.Entities;
using UnityEngine;

[RequiresEntityConversion]
public class PlayerAxisInput : MonoBehaviour, IConvertGameObjectToEntity
{
    [SerializeField]
    public Vector2 InputVector;
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
    {
        var data = new AxisInput { InputVector = InputVector };
        dstManager.AddComponentData(entity, data);
    }
}
