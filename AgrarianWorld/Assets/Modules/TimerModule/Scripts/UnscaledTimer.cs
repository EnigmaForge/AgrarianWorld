using System;
using GameUpdatersModule;
using UnityEngine;

namespace TimerModule {
    public class UnscaledTimer : ITimer {
        public float Duration { get; set; }
        public float LeftTime { get; set; }
        private readonly IGameUpdater _gameUpdater;

        public event Action OnStart;
        public event Action OnPause;
        public event Action OnStop;

        public UnscaledTimer(IGameUpdater gameUpdater) =>
            _gameUpdater = gameUpdater;

        public void Start() {
            OnStart?.Invoke();
            LeftTime = Duration;
            _gameUpdater.OnUpdate += UpdateTimer;
        }

        public void Pause() {
            OnPause?.Invoke();
            _gameUpdater.OnUpdate -= UpdateTimer;
        }

        public void Stop() {
            OnStop?.Invoke();
            LeftTime = 0;
            _gameUpdater.OnUpdate -= UpdateTimer;
        }

        private void UpdateTimer() {
            LeftTime -= Time.unscaledDeltaTime;

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