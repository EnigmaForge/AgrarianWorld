using Modules.ViewsModule;

namespace Modules.GameMenu {
    public class OpenWorldPresenter : Presenter<OpenWorldView> {
        private readonly OpenWorldWindow _openWorldWindow;
        private readonly AddWorldWindow _addWorldWindow;

        public OpenWorldPresenter(OpenWorldWindow openWorldWindow, AddWorldWindow addWorldWindow) {
            _openWorldWindow = openWorldWindow;
            _addWorldWindow = addWorldWindow;
        }
        
        public override void Initialize() {
            View.OnClickCloseWindowButton += CloseWindow;
            View.OnClickShowAddWorldWindowButton += ShowAddWorldPopup;
        }

        public override void Dispose() {
            View.OnClickCloseWindowButton -= CloseWindow;
            View.OnClickShowAddWorldWindowButton -= ShowAddWorldPopup;
        }

        private void CloseWindow() =>
            _openWorldWindow.SetActive(false);

        private void ShowAddWorldPopup() =>
            _addWorldWindow.SetActive(true);
    }
}