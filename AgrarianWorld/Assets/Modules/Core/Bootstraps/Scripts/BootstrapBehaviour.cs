using UnityEngine;

namespace Modules.Core.Bootstraps {
    public class BootstrapBehaviour : MonoBehaviour {
        private void Awake() =>
            OnInitialize();

        protected virtual void OnInitialize() { }
    }
}