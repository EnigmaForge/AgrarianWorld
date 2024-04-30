using Modules.ViewsModule;
using UnityEngine;

namespace Modules.GameMenu {
    public class NavigationButtonsPresenter : Presenter<NavigationButtonsView> {
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

        private void OnClickStartGame() {
            Debug.LogError("1");
        }

        private void OnClickSettings() {
            Debug.LogError("2");
        }

        private void OnClickQuit() {
            Debug.LogError("3");
        }
    }
}