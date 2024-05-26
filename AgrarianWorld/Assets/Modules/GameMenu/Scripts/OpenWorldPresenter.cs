using System;
using Modules.Core.FiniteStateMachine.GameStateMachine;
using Modules.Core.Global.Enums;
using Modules.SavingSystem;
using Modules.ViewsModule;
using Random = UnityEngine.Random;

namespace Modules.GameMenu {
    public class OpenWorldPresenter : Presenter<OpenWorldView> {
        private const string WORLDS_LIST_SAVES_KEY = "WorldsList";
        private const int MIN_SEED_VALUE = 0;
        private const int MAX_SEED_VALUE = 999999;
        private readonly OpenWorldWindow _openWorldWindow;
        private readonly AddWorldWindow _addWorldWindow;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly WorldsListModel _worldsListModel;
        private readonly AddWindowInitialValuesModel _addWindowInitialValuesModel;
        private readonly IDataStorageService _dataStorageService;

        public OpenWorldPresenter(WorldsListModel worldsListModel, OpenWorldWindow openWorldWindow, AddWorldWindow addWorldWindow, 
                                  IGameStateMachine gameStateMachine, AddWindowInitialValuesModel addWindowInitialValuesModel, IDataStorageService dataStorageService) {
            _openWorldWindow = openWorldWindow;
            _addWorldWindow = addWorldWindow;
            _gameStateMachine = gameStateMachine;
            _worldsListModel = worldsListModel;
            _addWindowInitialValuesModel = addWindowInitialValuesModel;
            _dataStorageService = dataStorageService;
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

        private void OpenWorld() {
            WorldData worldData = _worldsListModel.GetWorld(_worldsListModel.SelectedWorld.WorldName);
            worldData.LastOpenDate = DateTime.Now.ToString("dd.MM.yyyy");
            WorldsListHolder worldsListHolder = new WorldsListHolder {
                Worlds = _worldsListModel.GetWorlds()
            };
            _dataStorageService.Save(WORLDS_LIST_SAVES_KEY, worldsListHolder, SaveGroups.Worlds.ToString());
            
            _gameStateMachine.ChangeState<GameState>();
        }

        private void ShowAddWorldPopup() {
            _addWindowInitialValuesModel.WorldName = "New World";
            _addWindowInitialValuesModel.Seed = GetSeed();
            _addWindowInitialValuesModel.WorldType = WorldType.Flat;

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