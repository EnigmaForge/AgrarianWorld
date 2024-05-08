using System;
using Modules.ViewsModule;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.GameMenu {
    public class MenuNavigationButtonsView : ViewBehaviour {
        [SerializeField] private Button _startGameButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _quitButton;

        public event Action OnClickStartGame;
        public event Action OnClickSettings;
        public event Action OnClickQuit;

        private void OnEnable() {
            _startGameButton.onClick.AddListener(InvokeOnClickStartGame);
            _settingsButton.onClick.AddListener(InvokeOnClickSettings);
            _quitButton.onClick.AddListener(InvokeOnClickQuit);
        }

        private void OnDisable() {
            _startGameButton.onClick.RemoveListener(InvokeOnClickStartGame);
            _settingsButton.onClick.RemoveListener(InvokeOnClickSettings);
            _quitButton.onClick.RemoveListener(InvokeOnClickQuit);
        }

        private void InvokeOnClickStartGame() =>
            OnClickStartGame?.Invoke();
        
        private void InvokeOnClickSettings() =>
            OnClickSettings?.Invoke();
        
        private void InvokeOnClickQuit() =>
            OnClickQuit?.Invoke();
    }
}