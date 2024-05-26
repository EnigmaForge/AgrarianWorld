using System;

namespace Modules.Core.GameUpdater {
    public interface IGameUpdater {
        public event Action OnUpdate;
        public event Action OnFixedUpdate;
        public event Action OnLateUpdate;
    }
}