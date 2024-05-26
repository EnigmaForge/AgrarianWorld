using System;
using Modules.Core.GameUpdater;
using UnityEngine;
using Zenject;

namespace Modules.GameMenu {
    public class PauseMenuController : IInitializable, IDisposable {
        private readonly PauseMenuWindow _pauseMenuWindow;
        private readonly IGameUpdater _gameUpdater;

        internal PauseMenuController(PauseMenuWindow pauseMenuWindow, IGameUpdater gameUpdater) {
            _pauseMenuWindow = pauseMenuWindow;
            _gameUpdater = gameUpdater;
        }

        public void Initialize() =>
            _gameUpdater.OnUpdate += SwitchPauseMenuVisibility;

        public void Dispose() =>
            _gameUpdater.OnUpdate -= SwitchPauseMenuVisibility;

        private void SwitchPauseMenuVisibility() {
            if (Input.GetKeyDown(KeyCode.Escape))
                _pauseMenuWindow.SetActive(!_pauseMenuWindow.isActiveAndEnabled);
        }
    }
}