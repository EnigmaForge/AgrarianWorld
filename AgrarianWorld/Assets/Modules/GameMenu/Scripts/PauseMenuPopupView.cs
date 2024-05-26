using System;
using Modules.ViewsModule;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.GameMenu {
    public class PauseMenuPopupView : ViewBehaviour {
        [SerializeField] private Button _resumeButton;
        [SerializeField] private Button _openMenuButton;
        [SerializeField] private Button _exitButton;
        
        public event Action OnClickResumeButton;
        public event Action OnClickOpenMenuButton;
        public event Action OnClickExitButton;

        private void OnEnable() {
            _resumeButton.onClick.AddListener(OnClickResumeButtonInvoke);
            _openMenuButton.onClick.AddListener(OnClickOpenMenuButtonInvoke);
            _exitButton.onClick.AddListener(OnClickExitButtonInvoke);
        }

        private void OnDisable() {
            _resumeButton.onClick.RemoveListener(OnClickResumeButtonInvoke);
            _openMenuButton.onClick.RemoveListener(OnClickOpenMenuButtonInvoke);
            _exitButton.onClick.RemoveListener(OnClickExitButtonInvoke);
        }

        private void OnClickResumeButtonInvoke() =>
            OnClickResumeButton?.Invoke();

        private void OnClickOpenMenuButtonInvoke() =>
            OnClickOpenMenuButton?.Invoke();

        private void OnClickExitButtonInvoke() =>
            OnClickExitButton?.Invoke();
    }
}