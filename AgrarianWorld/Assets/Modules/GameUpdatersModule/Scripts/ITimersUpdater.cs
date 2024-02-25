using System;

namespace GameUpdatersModule {
    public interface ITimersUpdater {
        public event Action OnUpdate;
    }
}