using System;
using GameUpdatersModule;

namespace TimerModule {
    public class TimersFactory : ITimersFactory {
        private readonly IGameUpdater _gameUpdater;

        public TimersFactory(IGameUpdater gameUpdater) =>
            _gameUpdater = gameUpdater;

        public ITimer Create(TimerType timerType) {
            return timerType switch {
                TimerType.Default => CreateDefaultTimer(),
                TimerType.Unscaled => CreateUnscaledTimer(),
                _ => throw new ArgumentOutOfRangeException(nameof(timerType), timerType, "Timer cannot be created.")
            };
        }

        private ITimer CreateDefaultTimer() =>
            new DefaultTimer(_gameUpdater);

        private ITimer CreateUnscaledTimer() =>
            new UnscaledTimer(_gameUpdater);
    }
}