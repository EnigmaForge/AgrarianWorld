using Modules.ViewsModule;

namespace Modules.GameMenu {
    public class GameMenuWindow : WindowBehaviour {
        protected override void SetupPresenters() {
            Container.CreatePresenter<NavigationButtonsPresenter, NavigationButtonsView>();
        }
    }
}