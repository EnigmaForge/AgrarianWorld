using System;

namespace TimerModule {
    public interface ITimersManager {
        public ITimersManager SetDuration(float duration);
        public ITimersManager OnStart(Action callback);
        public ITimersManager OnPause(Action callback);
        public ITimersManager OnStop(Action callback);
        public ITimersManager OnKill(Action callback);
        public ITimersManager CreateTimer(string timerKey, TimerType timerType = TimerType.Default);
        public void StartTimer(string timerKey);
        public void PauseTimer(string timerKey);
        public void StopTimer(string timerKey);
        public void KillTimer(string timerKey);
        public float GetDuration(string timerKey);
        public float GetLeftTime(string timerKey);
        public float GetEclipsedTime(string timerKey);
        public TimerStatus GetTimerStatus(string timerKey);
    }
}