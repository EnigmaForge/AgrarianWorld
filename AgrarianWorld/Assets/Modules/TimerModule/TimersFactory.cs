using System;
using GameUpdater;

namespace TimerModule {
    public class TimersFactory : ITimersFactory {
        private readonly IUpdater _updater;

        public TimersFactory(IUpdater updater) =>
            _updater = updater;

        public ITimer Create(TimerConfiguration configuration) {
            switch (configuration.TimerType) {
                case TimerType.Simple:
                    SimpleTimer simpleTimer = new SimpleTimer(_updater) {
                        Duration = configuration.Duration
                    };

                    simpleTimer.OnStart += configuration.OnStart;
                    simpleTimer.OnPause += configuration.OnPause;
                    simpleTimer.OnStop += configuration.OnStop;

                    return simpleTimer;
                case TimerType.Unscaled:
                    UnscaledTimer unscaledTimer = new UnscaledTimer(_updater) {
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