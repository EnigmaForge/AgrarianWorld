using Modules.Core.FiniteStateMachine.GameStateMachine;
using Modules.ViewsModule;
using UnityEngine;

namespace Modules.GameMenu {
    public class PauseMenuPopupPresenter : Presenter<PauseMenuPopupView> {
        private readonly PauseMenuWindow _pauseMenuWindow;
        private readonly IGameStateMachine _gameStateMachine;

        public PauseMenuPopupPresenter(PauseMenuWindow pauseMenuWindow, IGameStateMachine gameStateMachine) {
            _pauseMenuWindow = pauseMenuWindow;
            _gameStateMachine = gameStateMachine;
        }

        public override void Initialize() {
            View.OnClickResumeButton += ClosePauseMenu;
            View.OnClickOpenMenuButton += OpenMenu;
            View.OnClickExitButton += ExitGame;
        }

        public override void Dispose() {
            View.OnClickResumeButton -= ClosePauseMenu;
            View.OnClickOpenMenuButton -= OpenMenu;
            View.OnClickExitButton -= ExitGame;
        }

        private void ClosePauseMenu() =>
            _pauseMenuWindow.SetActive(false);

        private void OpenMenu() =>
            _gameStateMachine.ChangeState<MenuState>();

        private void ExitGame() {
            #if UNITY_EDITOR
            if (UnityEditor.EditorApplication.isPlaying)
                UnityEditor.EditorApplication.isPlaying = false;
            return;
            #endif
            
            Application.Quit();
        }
    }
}