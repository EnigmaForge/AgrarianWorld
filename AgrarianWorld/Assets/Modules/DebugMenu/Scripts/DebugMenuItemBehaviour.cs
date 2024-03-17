using UnityEngine;

namespace Modules.DebugMenu {
    public abstract class DebugMenuItemBehaviour : MonoBehaviour, IDebugMenuItem {
        public void Enable() =>
            enabled = true;

        public void Disable() =>
            enabled = false;
    }
}