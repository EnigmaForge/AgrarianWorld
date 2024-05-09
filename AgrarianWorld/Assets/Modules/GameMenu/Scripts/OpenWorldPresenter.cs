using Modules.Core.FiniteStateMachine.GameStateMachine;
using Modules.ViewsModule;
using UnityEngine;

namespace Modules.GameMenu {
    public class OpenWorldPresenter : Presenter<OpenWorldView> {
        private const int MIN_SEED_VALUE = 0;
        private const int MAX_SEED_VALUE = 999999;
        private readonly OpenWorldWindow _openWorldWindow;
        private readonly AddWorldWindow _addWorldWindow;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly WorldsListModel _worldsListModel;
        private readonly AddWindowInitialValuesModel _addWindowInitialValuesModel;

        public OpenWorldPresenter(WorldsListModel worldsListModel, OpenWorldWindow openWorldWindow, AddWorldWindow addWorldWindow, IGameStateMachine gameStateMachine, AddWindowInitialValuesModel addWindowInitialValuesModel) {
            _openWorldWindow = openWorldWindow;
            _addWorldWindow = addWorldWindow;
            _gameStateMachine = gameStateMachine;
            _worldsListModel = worldsListModel;
            _addWindowInitialValuesModel = addWindowInitialValuesModel;
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

        private void ShowAddWorldPopup() {
            _addWindowInitialValuesModel.WorldName = "New World";
            _addWindowInitialValuesModel.Seed = GetSeed();
            _addWindowInitialValuesModel.WorldType = WorldType.Default;

            AddWorldPopupPresenter addWorldPopupPresenter = _addWorldWindow.GetPresenter<AddWorldPopupPresenter>();
            addWorldPopupPresenter?.SetInitialValues();

            _addWorldWindow.SetActive(true);
        }

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
        
        private int GetSeed() =>
            Random.Range(MIN_SEED_VALUE, MAX_SEED_VALUE + 1);
    }
}