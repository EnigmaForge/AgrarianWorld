using Modules.ViewsModule;
using UnityEngine;

namespace Modules.GameMenu {
    public class MenuNavigationButtonsPresenter : Presenter<MenuNavigationButtonsView> {
        private readonly OpenWorldWindow _openWorldWindow;

        private MenuNavigationButtonsPresenter(OpenWorldWindow openWorldWindow) =>
            _openWorldWindow = openWorldWindow;

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
            _openWorldWindow.SetActive(true);

        private void OnClickSettings() =>
            Debug.LogError("NO SETTINGS!");
 
        private void OnClickQuit() =>
            Application.Quit();
    }
}