using System;
using GameUpdatersModule;

namespace TimerModule {
    public class TimersFactory : ITimersFactory {
        private readonly ITimersUpdater _timersUpdater;

        public TimersFactory(ITimersUpdater timersUpdater) =>
            _timersUpdater = timersUpdater;

        public ITimer Create(TimerType timerType) {
            return timerType switch {
                TimerType.Default => CreateDefaultTimer(),
                TimerType.Unscaled => CreateUnscaledTimer(),
                _ => throw new ArgumentOutOfRangeException(nameof(timerType), timerType, "Timer cannot be created.")
            };
        }

        private ITimer CreateDefaultTimer() =>
            new DefaultTimer(_timersUpdater);

        private ITimer CreateUnscaledTimer() =>
            new UnscaledTimer(_timersUpdater);
    }
}