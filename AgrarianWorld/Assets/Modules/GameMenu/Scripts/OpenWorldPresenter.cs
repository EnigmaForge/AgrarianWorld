using Modules.Core.FiniteStateMachine.GameStateMachine;
using Modules.ViewsModule;

namespace Modules.GameMenu {
    public class OpenWorldPresenter : Presenter<OpenWorldView> {
        private readonly OpenWorldWindow _openWorldWindow;
        private readonly AddWorldWindow _addWorldWindow;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly WorldsListModel _worldsListModel;

        public OpenWorldPresenter(WorldsListModel worldsListModel, OpenWorldWindow openWorldWindow, AddWorldWindow addWorldWindow, IGameStateMachine gameStateMachine) {
            _openWorldWindow = openWorldWindow;
            _addWorldWindow = addWorldWindow;
            _gameStateMachine = gameStateMachine;
            _worldsListModel = worldsListModel;
        }
        
        public override void Initialize() {
            View.OnClickCloseWindowButton += CloseWindow;
            View.OnClickOpenWorldButton += OpenWorld;
            View.OnClickShowAddWorldWindowButton += ShowAddWorldPopup;
            View.OnClickRemoveWorldButton += RemoveWorld;
            _worldsListModel.OnSelectedWorldChanged += UpdateInteractables;
            
            UpdateInteractables(_worldsListModel.SelectedWorld);
        }

        public override void Dispose() {
            View.OnClickCloseWindowButton -= CloseWindow;
            View.OnClickOpenWorldButton -= OpenWorld;
            View.OnClickShowAddWorldWindowButton -= ShowAddWorldPopup;
            View.OnClickRemoveWorldButton -= RemoveWorld;
            _worldsListModel.OnSelectedWorldChanged -= UpdateInteractables;
        }

        private void CloseWindow() =>
            _openWorldWindow.SetActive(false);

        private void OpenWorld() =>
            _gameStateMachine.ChangeState<GameState>();

        private void ShowAddWorldPopup() =>
            _addWorldWindow.SetActive(true);

        private void RemoveWorld() {
            WorldData worldData = _worldsListModel.SelectedWorld;
            
            if (worldData != null)
                _worldsListModel.RemoveWorld(worldData);
        }

        private void UpdateInteractables(WorldData worldData) {
            bool worldDataNotNull = worldData != null;
            
            View.SetInteractableOpenButton(worldDataNotNull);
            View.SetInteractableRemoveButton(worldDataNotNull);
        }
    }
}