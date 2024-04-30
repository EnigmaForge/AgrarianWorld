using Modules.Core.FiniteStateMachine.GameStateMachine;
using Modules.ViewsModule;
using UnityEngine;
using Zenject;

namespace Modules.GameMenu {
    public class NavigationButtonsPresenter : Presenter<NavigationButtonsView> {
        private readonly IGameStateMachine _gameStateMachine;

        private NavigationButtonsPresenter(IGameStateMachine gameStateMachine) =>
            _gameStateMachine = gameStateMachine;

        public override void Initialize() {
            View.OnClickStartGame += OnClickStartGame;
            View.OnClickSettings += OnClickSettings;
            View.OnClickQuit += OnClickQuit;
        }

        public override void Dispose() {
            View.OnClickStartGame -= OnClickStartGame;
            View.OnClickSettings -= OnClickSettings;
            View.OnClickQuit -= OnClickQuit;
        }

        private void OnClickStartGame() =>
            _gameStateMachine.ChangeState<GameState>();

        private void OnClickSettings() =>
            Debug.LogError("NO SETTINGS!");
 
        private void OnClickQuit() =>
            Application.Quit();
    }
}