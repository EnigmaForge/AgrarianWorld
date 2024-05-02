using Modules.ViewsModule;

namespace Modules.GameMenu {
    public class AddWorldWindow : WindowBehaviour {
        protected override void SetupPresenters() {
            Container.CreatePresenter<AddWorldPopupPresenter, AddWorldPopupView>();
        }
    }
}