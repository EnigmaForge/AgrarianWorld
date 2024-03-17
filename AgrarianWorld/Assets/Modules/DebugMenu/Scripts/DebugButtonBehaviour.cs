using UnityEngine;
using UnityEngine.UI;

namespace Modules.DebugMenu {
    [RequireComponent(typeof(Button))]
    public abstract class DebugButtonBehaviour : DebugMenuItemBehaviour {
        private Button _button;
        
        private void Awake() =>
            _button = GetComponent<Button>();

        private void OnEnable() =>
            _button.onClick.AddListener(OnClick);

        private void OnDisable() =>
            _button.onClick.RemoveListener(OnClick);

        public abstract void OnClick();
    }
}