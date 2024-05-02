using Modules.ViewsModule;

namespace Modules.GameMenu {
    public class OpenWorldWindow : WindowBehaviour {
        protected override void SetupPresenters() {
            Container.CreatePresenter<OpenWorldPresenter, OpenWorldView>();
        }
    }
}