using System;
using UnityEngine;

namespace Modules.Core.GameUpdater {
    public class GameUpdater : MonoBehaviour, IGameUpdater {
        public event Action OnUpdate;
        public event Action OnFixedUpdate;
        public event Action OnLateUpdate;

        private void Update() =>
            OnUpdate?.Invoke();

        private void FixedUpdate() =>
            OnFixedUpdate?.Invoke();

        private void LateUpdate() =>
            OnLateUpdate?.Invoke();
    }
}