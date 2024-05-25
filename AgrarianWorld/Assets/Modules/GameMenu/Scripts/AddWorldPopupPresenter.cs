using System;
using System.Collections.Generic;
using System.Linq;
using Modules.Core.FiniteStateMachine.GameStateMachine;
using Modules.ViewsModule;

namespace Modules.GameMenu {
    public class AddWorldPopupPresenter : Presenter<AddWorldPopupView> {
        private readonly AddWorldWindow _addWorldWindow;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly WorldsListModel _worldsListModel;
        private readonly AddWindowInitialValuesModel _addWindowInitialValuesModel;
        private WorldDataHolder _worldDataHolder;

        public AddWorldPopupPresenter(AddWorldWindow addWorldWindow, IGameStateMachine gameStateMachine, WorldsListModel worldsListModel, AddWindowInitialValuesModel addWindowInitialValuesModel) {
            _addWorldWindow = addWorldWindow;
            _gameStateMachine = gameStateMachine;
            _worldsListModel = worldsListModel;
            _addWindowInitialValuesModel = addWindowInitialValuesModel;
            _worldDataHolder = new WorldDataHolder();
        }
        
        public override void Initialize() {
            View.OnClickCloseWindowButton += CloseWindow;
            View.OnClickCreateWorldButton += CreateWorld;
            View.OnChangedWorldName += UpdateWorldName;
            View.OnChangedSeed += UpdateSeed;
            View.OnChangedWorldType += UpdateWorldType;

            InitializeWorldTypesDropdown();
            SetInitialValues();
        }

        private void InitializeWorldTypesDropdown() {
            List<string> worldTypes = Enum.GetNames(typeof(WorldType)).ToList();
            View.SetDropdownItems(worldTypes);
        }

        public override void Dispose() {
            View.OnClickCloseWindowButton -= CloseWindow;
            View.OnClickCreateWorldButton -= CreateWorld;
            View.OnChangedWorldName -= UpdateWorldName;
            View.OnChangedSeed -= UpdateSeed;
            View.OnChangedWorldType -= UpdateWorldType;
        }

        private void CloseWindow() =>
            _addWorldWindow.SetActive(false);

        private void CreateWorld() {
            AddNewWorld();
            _gameStateMachine.ChangeState<GameState>();
        }

        private void AddNewWorld() {
            WorldData newWorld = new WorldData(_worldDataHolder.WorldName, _worldDataHolder.Seed, _worldDataHolder.WorldType, GetCurrentDay());
            _worldsListModel.AddWorld(newWorld);
            _worldsListModel.SelectedWorld = newWorld;
        }

        private void UpdateWorldName(string worldName) =>
            _worldDataHolder.WorldName = worldName;

        private void UpdateSeed(int seed) =>
            _worldDataHolder.Seed = seed;

        private void UpdateWorldType(string worldType) =>
            _worldDataHolder.WorldType = Enum.Parse<WorldType>(worldType);

        private string GetCurrentDay() =>
            DateTime.Now.ToString("dd.MM.yyyy");

        public void SetInitialValues() {
            View.SetWorldName(_addWindowInitialValuesModel.WorldName);
            View.SetSeedValue(_addWindowInitialValuesModel.Seed);
            View.SetWorldType((int)_addWindowInitialValuesModel.WorldType);
        }
    }
}