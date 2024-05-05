using System.Collections.Generic;
using Modules.Core.Global.Enums;
using Modules.SavingSystem;
using Modules.ViewsModule;

namespace Modules.GameMenu {
    public class WorldsListPresenter : Presenter<WorldsListView> {
        private const string WORLDS_LIST_SAVES_KEY = "WorldsList";
        private readonly WorldsListModel _worldsListModel;
        private readonly IDataStorageService _dataStorageService;

        public WorldsListPresenter(WorldsListModel worldsListModel, IDataStorageService dataStorageService) {
            _worldsListModel = worldsListModel;
            _dataStorageService = dataStorageService;
        }

        public override void Initialize() {
            View.OnClickItem += SelectWorld;
            _worldsListModel.OnWorldsListChanged += UpdateWorldsList;
            InitializeWorldsList();
        }

        private void InitializeWorldsList() {
            WorldsListHolder worldsListHolder = _dataStorageService.Load<WorldsListHolder>(WORLDS_LIST_SAVES_KEY, SaveGroups.Worlds.ToString());
            
            foreach (WorldData worldData in worldsListHolder.Worlds)
                _worldsListModel.AddWorld(worldData);

            UpdateWorldsList();
        }

        public override void Dispose() {
            View.OnClickItem -= SelectWorld;
            _worldsListModel.OnWorldsListChanged -= UpdateWorldsList;
        }

        private void SelectWorld(int itemIndex) {
            List<WorldData> worlds = _worldsListModel.GetWorlds();
            _worldsListModel.SelectedWorld = worlds[itemIndex];
        }

        private void UpdateWorldsList() {
            WorldsListHolder worldsListHolder = new WorldsListHolder() {
                Worlds = _worldsListModel.GetWorlds()
            };
            
            _dataStorageService.Save(WORLDS_LIST_SAVES_KEY, worldsListHolder, SaveGroups.Worlds.ToString());
            View.UpdateList(worldsListHolder.Worlds);
        }
    }
}