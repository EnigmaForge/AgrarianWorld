using System;
using System.Collections.Generic;

namespace TimerModule {
    public class TimersManager : ITimersManager {
        private readonly Dictionary<string, ITimer> _timers = new();
        private readonly ITimersFactory _timersFactory;
        private ITimer _lastCreatedTimer;

        public TimersManager(ITimersFactory timersFactory) =>
            _timersFactory = timersFactory;

        public ITimersManager SetDuration(float duration) {
            _lastCreatedTimer.Duration = duration;
            return this;
        }

        public ITimersManager OnStart(Action callback) {
            _lastCreatedTimer.OnStart += callback;
            return this;
        }

        public ITimersManager OnPause(Action callback) {
            _lastCreatedTimer.OnPause += callback;
            return this;
        }

        public ITimersManager OnStop(Action callback) {
            _lastCreatedTimer.OnStop += callback;
            return this;
        }

        public ITimersManager OnKill(Action callback) {
            _lastCreatedTimer.OnKill += callback;
            return this;
        }

        public ITimersManager CreateTimer(string timerKey, TimerType timerType = TimerType.Default) {
            if (_timers.ContainsKey(timerKey))
                throw new Exception("Timer already created. Create timer with new key");
            
            _lastCreatedTimer = _timersFactory.Create(timerType);
            _timers.Add(timerKey, _lastCreatedTimer);
            return this;
        }

        public void StartTimer(string timerKey) {
            if (_timers.ContainsKey(timerKey) is false)
                throw new Exception("Timer not created");
            
            _timers[timerKey].Start();
        }

        public void PauseTimer(string timerKey) {
            if (_timers.ContainsKey(timerKey) is false)
                throw new Exception("Timer not created");
            
            _timers[timerKey].Pause();
        }

        public void StopTimer(string timerKey) {
            if (_timers.ContainsKey(timerKey) is false)
                throw new Exception("Timer not created");
            
            _timers[timerKey].Stop();
        }

        public void KillTimer(string timerKey) {
            if (_timers.ContainsKey(timerKey) is false)
                throw new Exception("Timer not created");
            
            _timers[timerKey].Kill();
            _timers.Remove(timerKey);
        }

        public float GetDuration(string timerKey) {
            if (_timers.ContainsKey(timerKey) is false)
                throw new Exception("Timer not created");
            
            return _timers[timerKey].Duration;
        }
        
        public float GetLeftTime(string timerKey) {
            if (_timers.ContainsKey(timerKey) is false)
                throw new Exception("Timer not created");
            
            return _timers[timerKey].LeftTime;
        }
        
        public float GetEclipsedTime(string timerKey) {
            if (_timers.ContainsKey(timerKey) is false)
                throw new Exception("Timer not created");
            
            return _timers[timerKey].Duration - _timers[timerKey].LeftTime;
        }

        public TimerStatus GetTimerStatus(string timerKey) {
            if (_timers.ContainsKey(timerKey) is false)
                throw new Exception("Timer not created");
            
            return _timers[timerKey].GetStatus();
        }
    }
}