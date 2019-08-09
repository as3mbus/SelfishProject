using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

namespace as3mbus.SelfishProject.Sources
{
    public class MovementComponent : MonoBehaviour, IConvertGameObjectToEntity
    {
        public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversionSystem)
        {
            var data = new Movement { SpeedMultiplier = _speedMultiplier, MoveVal = _movementValue };
            dstManager.AddComponentData(entity, data);
            // throw new System.NotImplementedException();
        }
        private Vector2 _movementValue = Vector2.zero;
        [SerializeField]
        private int _speedMultiplier = 3;
    }


}