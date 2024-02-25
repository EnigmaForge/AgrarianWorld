using System;
using GameUpdatersModule;
using UnityEngine;

namespace TimerModule {
    public class UnscaledTimer : ITimer {
        private readonly ITimersUpdater _timersUpdater;
        private TimerStatus _timerStatus;
        public float Duration { get; set; }
        public float LeftTime { get; set; }

        public event Action OnStart;
        public event Action OnPause;
        public event Action OnStop;
        public event Action OnKill;

        public UnscaledTimer(ITimersUpdater timersUpdater) =>
            _timersUpdater = timersUpdater;

        public void Start() {
            LeftTime = Duration;
            _timersUpdater.OnUpdate += UpdateTimer;
            OnStart?.Invoke();
            _timerStatus = TimerStatus.Running;
        }

        public void Pause() {
            _timersUpdater.OnUpdate -= UpdateTimer;
            OnPause?.Invoke();
            _timerStatus = TimerStatus.Paused;
        }

        public void Stop() {
            LeftTime = 0;
            _timersUpdater.OnUpdate -= UpdateTimer;
            OnStop?.Invoke();
            _timerStatus = TimerStatus.Stopped;
        }

        public void Kill() {
            Cleanup();
            OnKill?.Invoke();
            _timerStatus = TimerStatus.Killed;
        }

        public TimerStatus GetStatus() =>
            _timerStatus;

        private void Cleanup() {
            if (_timerStatus == TimerStatus.Running)
                _timersUpdater.OnUpdate -= UpdateTimer;

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