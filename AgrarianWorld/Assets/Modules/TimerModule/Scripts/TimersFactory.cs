using System;
using GameUpdatersModule;

namespace TimerModule {
    public class TimersFactory : ITimersFactory {
        private readonly IGameUpdater _gameUpdater;

        public TimersFactory(IGameUpdater gameUpdater) =>
            _gameUpdater = gameUpdater;

        public ITimer Create(TimerConfiguration configuration) {
            switch (configuration.TimerType) {
                case TimerType.Simple:
                    SimpleTimer simpleTimer = new SimpleTimer(_gameUpdater) {
                        Duration = configuration.Duration
                    };

                    simpleTimer.OnStart += configuration.OnStart;
                    simpleTimer.OnPause += configuration.OnPause;
                    simpleTimer.OnStop += configuration.OnStop;

                    return simpleTimer;
                case TimerType.Unscaled:
                    UnscaledTimer unscaledTimer = new UnscaledTimer(_gameUpdater) {
                        Duration = configuration.Duration
                    };

                    unscaledTimer.OnStart += configuration.OnStart;
                    unscaledTimer.OnPause += configuration.OnPause;
                    unscaledTimer.OnStop += configuration.OnStop;

                    return unscaledTimer;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}