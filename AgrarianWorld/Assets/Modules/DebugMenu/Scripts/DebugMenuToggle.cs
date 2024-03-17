using UnityEngine;
using UnityEngine.UI;

namespace Modules.DebugMenu {
    public class DebugMenuToggle : MonoBehaviour {
        [SerializeField] private Toggle _contentToggle;
        [SerializeField] private GameObject _content;
        [SerializeField] private Image _toggleIcon;
        [SerializeField] private Sprite _enableContentSprite;
        [SerializeField] private Sprite _disableContentSprite;
        
        private void OnEnable() {
            UpdateContentVisibility(_contentToggle.isOn);
            _contentToggle.onValueChanged.AddListener(UpdateContentVisibility);
        }

        private void OnDisable() =>
            _contentToggle.onValueChanged.RemoveListener(UpdateContentVisibility);
        
        private void UpdateContentVisibility(bool visibility) {
            _toggleIcon.sprite = visibility ? _enableContentSprite : _disableContentSprite;
            _content.SetActive(visibility);
        }
    }
}