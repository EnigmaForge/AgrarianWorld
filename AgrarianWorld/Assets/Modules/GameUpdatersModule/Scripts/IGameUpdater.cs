using System;

namespace GameUpdatersModule {
    public interface IGameUpdater {
        public event Action OnUpdate;
        public event Action OnFixedUpdate;
        public event Action OnLateUpdate;
    }
}