using System;

namespace GameUpdater {
    public interface IUpdater {
        public event Action OnUpdate;
        public event Action OnFixedUpdate;
        public event Action OnLateUpdate;
    }
}