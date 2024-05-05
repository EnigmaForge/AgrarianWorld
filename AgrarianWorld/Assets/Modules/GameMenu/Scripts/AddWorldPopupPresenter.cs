using System;
using System.Collections.Generic;
using System.Linq;
using Modules.Core.FiniteStateMachine.GameStateMachine;
using Modules.Core.Global.Enums;
using Modules.SavingSystem;
using Modules.ViewsModule;

namespace Modules.GameMenu {
    public class AddWorldPopupPresenter : Presenter<AddWorldPopupView> {
        private const string WORLDS_LIST_SAVES_KEY = "WorldsList";
        private readonly AddWorldWindow _addWorldWindow;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly WorldsListModel _worldsListModel;
        private readonly IDataStorageService _dataStorageService;
        private WorldDataHolder _worldDataHolder;

        public AddWorldPopupPresenter(AddWorldWindow addWorldWindow, IGameStateMachine gameStateMachine, WorldsListModel worldsListModel, IDataStorageService dataStorageService) {
            _addWorldWindow = addWorldWindow;
            _gameStateMachine = gameStateMachine;
            _worldsListModel = worldsListModel;
            _dataStorageService = dataStorageService;
            _worldDataHolder = new WorldDataHolder();
        }
        
        public override void Initialize() {
            View.OnClickCloseWindowButton += CloseWindow;
            View.OnClickCreateWorldButton += CreateWorld;
            View.OnChangedWorldName += UpdateWorldName;
            View.OnChangedSeed += UpdateSeed;
            View.OnChangedWorldType += UpdateWorldType;

            InitializeWorldTypesDropdown();
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
            SaveWorlds();
            _gameStateMachine.ChangeState<GameState>();
        }

        private void AddNewWorld() {
            WorldData newWorld = new WorldData(_worldDataHolder.WorldName, _worldDataHolder.Seed, _worldDataHolder.WorldType, GetCurrentDay());
            _worldsListModel.AddWorld(newWorld);
        }

        private void SaveWorlds() {
            WorldsListHolder worldsListHolder = new WorldsListHolder {
                Worlds = _worldsListModel.GetWorlds()
            };

            _dataStorageService.Save(WORLDS_LIST_SAVES_KEY, worldsListHolder, SaveGroups.Worlds.ToString());
        }

        private void UpdateWorldName(string worldName) =>
            _worldDataHolder.WorldName = worldName;

        private void UpdateSeed(string seed) =>
            _worldDataHolder.Seed = seed;

        private void UpdateWorldType(string worldType) =>
            _worldDataHolder.WorldType = Enum.Parse<WorldType>(worldType);

        private string GetCurrentDay() =>
            DateTime.Now.ToString("dd.MM.yyyy");
    }
}