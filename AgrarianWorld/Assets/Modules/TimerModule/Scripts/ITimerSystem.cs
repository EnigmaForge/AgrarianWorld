namespace TimerModule {
    public interface ITimerSystem {
        public void CreateTimer(TimerConfiguration configuration);
        public void StartTimer(TimerName timerName, TimerGroup timerGroup);
        public void PauseTimer(TimerName timerName, TimerGroup timerGroup);
        public void StopTimer(TimerName timerName, TimerGroup timerGroup);
        public void KillTimer(TimerName timerName, TimerGroup timerGroup);
        public void StartTimerGroup(TimerGroup timerGroup);
        public void PauseTimerGroup(TimerGroup timerGroup);
        public void StopTimerGroup(TimerGroup timerGroup);
        public void KillTimerGroup(TimerGroup timerGroup);
    }
}
