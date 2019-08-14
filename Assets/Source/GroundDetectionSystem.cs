using System;
using UnityEngine;

namespace as3mbus.Selfish.Source
{
    public abstract class GroundDetectionSystem : MonoBehaviour
    {
        [SerializeField]
        protected GroundDetectionControl _control;
        public GroundDetectionControl Control => _control;
        protected abstract bool GroundCheck();

        protected virtual void FixedUpdate()
        {
            _control.OnGround = GroundCheck();
        }
    }
}