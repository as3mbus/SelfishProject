using System;
using UnityEngine;

namespace as3mbus.Selfish.Source
{
    [Serializable]
    public class JumpControl
    {
        public JumpControl(JumpModel model)
        {
            _model = model;
        }

        [SerializeField] private JumpModel _model;
        public JumpModel Model => _model;

        public short AirJumpCount { get; private set; } = 0;

        public bool CanJump => (AirJumpCount < _model.AirJumpLimit || OnGround);
        private bool OnGround => _groundDetection.OnGround;
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
            if (onGround) AirJumpCount = 0;
        }

        public void JumpCall()
        {
            if (!CanJump) return;
            if (!OnGround) AirJumpCount++;
        }
    }
}