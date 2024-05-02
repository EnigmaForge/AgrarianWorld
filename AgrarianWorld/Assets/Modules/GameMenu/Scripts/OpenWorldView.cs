using System;
using Modules.ViewsModule;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.GameMenu {
    public class OpenWorldView : ViewBehaviour {
        [SerializeField] private Button _closeWindowButton;
        [SerializeField] private Button _showAddWorldWindowButton;

        public event Action OnClickCloseWindowButton;
        public event Action OnClickShowAddWorldWindowButton;

        private void OnEnable() {
            _closeWindowButton.onClick.AddListener(OnClickCloseWindowInvoke);
            _showAddWorldWindowButton.onClick.AddListener(OnClickShowAddWorldWindowButtonInvoke);
        }

        private void OnDisable() {
            _closeWindowButton.onClick.RemoveListener(OnClickCloseWindowInvoke);
            _showAddWorldWindowButton.onClick.RemoveListener(OnClickShowAddWorldWindowButtonInvoke);
        }

        private void OnClickCloseWindowInvoke() =>
            OnClickCloseWindowButton?.Invoke();
        
        private void OnClickShowAddWorldWindowButtonInvoke() =>
            OnClickShowAddWorldWindowButton?.Invoke();
    }
}