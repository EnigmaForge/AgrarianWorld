using Modules.ViewsModule;

namespace Modules.GameMenu {
    public class PauseMenuWindow : WindowBehaviour {
        protected override void SetupPresenters() {
            Container.CreatePresenter<PauseMenuPopupPresenter, PauseMenuPopupView>();
        }
    }
}