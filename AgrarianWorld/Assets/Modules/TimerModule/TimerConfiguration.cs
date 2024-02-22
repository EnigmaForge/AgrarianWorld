using System;

namespace TimerModule {
    public class TimerConfiguration {
        public TimerName TimerName;
        public TimerGroup TimerGroup;
        public TimerType TimerType;
        public float Duration;
        public Action OnStart;
        public Action OnPause;
        public Action OnStop;
    }
}