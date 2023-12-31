﻿using System;
using UnityEngine;

namespace GameUpdater {
    public class Updater : MonoBehaviour, IUpdater {
        public event Action OnUpdate;
        public event Action OnFixedUpdate;
        public event Action OnLateUpdate;

        public void Update() =>
            OnUpdate?.Invoke();

        public void FixedUpdate() =>
            OnFixedUpdate?.Invoke();

        public void LateUpdate() =>
            OnLateUpdate?.Invoke();

        private void OnDestroy() =>
            Cleanup();

        private void Cleanup() {
            OnUpdate = null;
            OnFixedUpdate = null;
            OnLateUpdate = null;
        }
    }
}