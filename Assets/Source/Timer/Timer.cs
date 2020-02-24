using System;

namespace as3mbus.Selfish.Source
{
    public class Timer
    {
        public event Action OnTimeOut;
        public event Action<float> OnUpdate;
        private float _remainingTime = 0;

        public float RemainingTime
        {
            get => _remainingTime;
            private set
            {
                float previousValue = _remainingTime;
                _remainingTime = Math.Max(0,value);
                if(Math.Abs(previousValue - _remainingTime) < 0) return;
                OnUpdate?.Invoke(previousValue - _remainingTime);
                if (_remainingTime > 0) return;
                IsRunning = false;
                OnTimeOut?.Invoke();
            }
        }

        public float AssignedTime { get; private set; } = 60;
        public bool IsRunning { get; private set; } = false;

        public void Update(float delta)
        {
            if(!IsRunning) throw new Exception("Trying to Update Timer that not running");
            RemainingTime -= delta;    
        }

        public void Start(float time = 60f)
        {
            if(IsRunning) throw new Exception("Trying to Start an Already Running Timer");
            AssignedTime = time;
            RemainingTime = AssignedTime;
            IsRunning = true;
        }

        public void TogglePause(bool toggle)
        {
            string toggleString = toggle ? "Pause" : "Resume";
            string isRunningString = (IsRunning ? "" : "not") + "running";
            if(IsRunning != toggle) throw new Exception($"Trying to {toggleString} a {isRunningString} timer");
            IsRunning = !toggle;
        }
        public static implicit operator bool (Timer timer)
        {
            return timer.IsRunning;
        }
        public static implicit operator float (Timer timer)
        {
            return timer._remainingTime;
        }
    }
}