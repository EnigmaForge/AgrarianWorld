using System;
using GameUpdater;
using UnityEngine;

namespace TimerModule {
    public class SimpleTimer : ITimer {
        public float Duration { get; set; }
        public float LeftTime { get; set; }
        private readonly IUpdater _updater;

        public event Action OnStart;
        public event Action OnPause;
        public event Action OnStop;

        public SimpleTimer(IUpdater updater) =>
            _updater = updater;

        public void Start() {
            OnStart?.Invoke();
            LeftTime = Duration;
            _updater.OnUpdate += UpdateTimer;
        }

        public void Pause() {
            OnPause?.Invoke();
            _updater.OnUpdate -= UpdateTimer;
        }

        public void Stop() {
            OnStop?.Invoke();
            LeftTime = 0;
            _updater.OnUpdate -= UpdateTimer;
        }

        private void UpdateTimer() {
            LeftTime -= Time.deltaTime;

            if (LeftTime <= 0)
                Stop();
        }

        public void Cleanup() {
            Stop();
            OnStart = null;
            OnPause = null;
            OnStop = null;
        }
    }
}