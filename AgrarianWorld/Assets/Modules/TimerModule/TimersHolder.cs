using System.Collections.Generic;

namespace TimerModule {
    public class TimersHolder {
        public readonly Dictionary<TimerName, ITimer> Timers = new();

        public bool IsEmpty() =>
            Timers.Count == 0;
    }
}