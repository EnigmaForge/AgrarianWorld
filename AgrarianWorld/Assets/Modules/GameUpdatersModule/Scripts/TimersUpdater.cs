using System;
using UnityEngine;

namespace GameUpdatersModule {
    public class TimersUpdater : MonoBehaviour, ITimersUpdater {
        public event Action OnUpdate;

        private void Update() =>
            OnUpdate?.Invoke();
    }
}