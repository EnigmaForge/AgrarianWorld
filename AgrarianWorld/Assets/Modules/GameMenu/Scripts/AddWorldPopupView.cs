using System;
using Modules.ViewsModule;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.GameMenu {
    public class AddWorldPopupView : ViewBehaviour {
        [SerializeField] private Button _closeWindowButton;
        [SerializeField] private Button _createWorldButton;
        [SerializeField] private Button _cancelButton;

        public event Action OnClickCloseWindowButton;
        public event Action OnClickCreateWorldButton;

        private void OnEnable() {
            _closeWindowButton.onClick.AddListener(OnClickCloseWindowInvoke);
            _cancelButton.onClick.AddListener(OnClickCloseWindowInvoke);
            _createWorldButton.onClick.AddListener(OnClickCreateWorldInvoke);
        }

        private void OnDisable() {
            _closeWindowButton.onClick.RemoveListener(OnClickCloseWindowInvoke);
            _cancelButton.onClick.RemoveListener(OnClickCloseWindowInvoke);
            _createWorldButton.onClick.RemoveListener(OnClickCreateWorldInvoke);
        }

        private void OnClickCloseWindowInvoke() =>
            OnClickCloseWindowButton?.Invoke();

        private void OnClickCreateWorldInvoke() =>
            OnClickCreateWorldButton?.Invoke();
    }
}