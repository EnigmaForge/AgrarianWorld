using UnityEngine;
using UnityEngine.UI;

namespace Modules.DebugMenu {
    public class DebugMenuWindowSwitcher : MonoBehaviour {
        [SerializeField] private Toggle _debugMenuToggle;
        [SerializeField] private GameObject _debugMenuContent;
        [SerializeField] private Image _toggleImage;
        [SerializeField] private Sprite _hideIcon;
        [SerializeField] private Sprite _showIcon;

        private void Awake() =>
            UpdateDebugMenuVisibility(_debugMenuToggle.isOn);

        private void OnEnable() =>
            _debugMenuToggle.onValueChanged.AddListener(UpdateDebugMenuVisibility);

        private void OnDisable() =>
            _debugMenuToggle.onValueChanged.RemoveListener(UpdateDebugMenuVisibility);

        private void UpdateDebugMenuVisibility(bool visibility) {
            _debugMenuContent.SetActive(visibility);
            _toggleImage.sprite = visibility ? _hideIcon : _showIcon;
        }
    }
}