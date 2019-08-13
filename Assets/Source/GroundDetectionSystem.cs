using System;
using UnityEngine;

namespace as3mbus.Selfish.Source
{
    public abstract class GroundDetectionSystem : MonoBehaviour
    {
        protected abstract bool GroundCheck();
        [SerializeField]
        private bool _onGround;
        public event Action<bool> OnStateChanges;
        public bool OnGround
        {
            get => _onGround;
            private set
            {
                if (value == _onGround) return;
                OnStateChanges?.Invoke(value);
                _onGround = value;
            }
        }
        protected virtual void FixedUpdate()
        {
            OnGround = GroundCheck();
        }

    }
}