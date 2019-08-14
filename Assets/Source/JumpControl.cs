using System;
using UnityEngine;

namespace as3mbus.Selfish.Source
{
    [Serializable]
    public class JumpControl
    {
        [SerializeField] private JumpModel _model;
        private short _airJumpCount;

        public JumpModel Model => _model;
        public bool CanJump => (_airJumpCount < _model.AirJumpLimit || OnLand);
        public bool OnLand => _groundDetection.OnGround;
        private GroundDetectionControl _groundDetection;

        public GroundDetectionControl GroundDetection
        {
            get => _groundDetection;
            set
            {
                if (_groundDetection)
                    _groundDetection.OnStateChanges -= OnGroundDetectEvent;
                if (value)
                    value.OnStateChanges += OnGroundDetectEvent;
                _groundDetection = value;
            }
        }

        private void OnGroundDetectEvent(bool onGround)
        {
            if (onGround) _airJumpCount = 0;
        }

        public void JumpCall()
        {
            if (!OnLand) _airJumpCount++;
        }
    }
}