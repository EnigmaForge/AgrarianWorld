using System;
using GameUpdatersModule;
using UnityEngine;

namespace TimerModule {
    public class DefaultTimer : ITimer {
        private readonly IGameUpdater _gameUpdater;
        private TimerStatus _timerStatus;
        public float Duration { get; set; }
        public float LeftTime { get; set; }

        public event Action OnStart;
        public event Action OnPause;
        public event Action OnStop;
        public event Action OnKill;

        public DefaultTimer(IGameUpdater gameUpdater) =>
            _gameUpdater = gameUpdater;

        public void Start() {
            LeftTime = Duration;
            _gameUpdater.OnUpdate += UpdateTimer;
            OnStart?.Invoke();
        }

        public void Pause() {
            _gameUpdater.OnUpdate -= UpdateTimer;
            OnPause?.Invoke();
        }

        public void Stop() {
            LeftTime = 0;
            _gameUpdater.OnUpdate -= UpdateTimer;
            OnStop?.Invoke();
        }

        public void Kill() {
            Cleanup();
            _timerStatus = TimerStatus.Killed;
            OnKill?.Invoke();
        }

        public TimerStatus GetStatus() =>
            _timerStatus;

        private void Cleanup() {
            if (_timerStatus == TimerStatus.Running)
                _gameUpdater.OnUpdate -= UpdateTimer;

            OnStart = null;
            OnPause = null;
            OnStop = null;
            OnKill = null;
        }

        private void UpdateTimer() {
            LeftTime -= Time.deltaTime;

            if (LeftTime <= 0)
                Stop();
        }
    }
}