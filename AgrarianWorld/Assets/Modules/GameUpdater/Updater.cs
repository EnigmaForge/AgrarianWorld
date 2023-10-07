using System;
using UnityEngine;

namespace GameUpdater
{
    public class Updater : MonoBehaviour, IUpdater
    {
        public event Action OnUpdate;
        public event Action OnFixedUpdate;
        public event Action OnLateUpdate;

        public void Update() =>
            UpdateAction();

        public void FixedUpdate() =>
            FixedUpdateAction();

        public void LateUpdate() =>
            LateUpdateAction();

        private void OnDestroy() => 
            Cleanup();

        private void UpdateAction() =>
            OnUpdate?.Invoke();

        private void FixedUpdateAction() =>
            OnFixedUpdate?.Invoke();

        private void LateUpdateAction() =>
            OnLateUpdate?.Invoke();

        private void Cleanup()
        {
            OnUpdate = null;
            OnFixedUpdate = null;
            OnLateUpdate = null;
        }
    }
}
