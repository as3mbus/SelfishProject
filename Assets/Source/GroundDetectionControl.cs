using System;

namespace as3mbus.Selfish.Source
{
    [Serializable]
    public class GroundDetectionControl
    {
        public event Action<bool> OnStateChanges;

        private bool _onGround;

        public bool OnGround
        {
            get => _onGround;
            set
            {
                if (value == _onGround) return;
                OnStateChanges?.Invoke(value);
                _onGround = value;
            }
        }

        public static implicit operator bool(GroundDetectionControl obj)
        {
            return obj != null;
        }
    }
}