using System;

namespace TimerModule {
    public interface ITimer {
        public float Duration { get; set; }
        public float LeftTime { get; set; }

        public event Action OnStart;
        public event Action OnPause;
        public event Action OnStop;
        
        public void Start();
        public void Pause();
        public void Stop();
        public void Cleanup();
    }
}