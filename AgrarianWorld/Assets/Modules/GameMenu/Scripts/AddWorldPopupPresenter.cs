using Modules.Core.FiniteStateMachine.GameStateMachine;
using Modules.ViewsModule;

namespace Modules.GameMenu {
    public class AddWorldPopupPresenter : Presenter<AddWorldPopupView> {
        private readonly AddWorldWindow _addWorldWindow;
        private readonly IGameStateMachine _gameStateMachine;

        public AddWorldPopupPresenter(AddWorldWindow addWorldWindow, IGameStateMachine gameStateMachine) {
            _addWorldWindow = addWorldWindow;
            _gameStateMachine = gameStateMachine;
        }
        
        public override void Initialize() {
            View.OnClickCloseWindowButton += CloseWindow;
            View.OnClickCreateWorldButton += CreateWorld;
        }

        public override void Dispose() {
            View.OnClickCloseWindowButton -= CloseWindow;
            View.OnClickCreateWorldButton -= CreateWorld;
        }

        private void CloseWindow() =>
            _addWorldWindow.SetActive(false);

        private void CreateWorld() =>
            _gameStateMachine.ChangeState<GameState>();
    }
}