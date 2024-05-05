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
            _worldsListModel.OnWorldsListChanged += UpdateWorldsList;
            InitializeWorldsList();
        }

        private void InitializeWorldsList() {
            WorldsListHolder worldsListHolder = _dataStorageService.Load<WorldsListHolder>(WORLDS_LIST_SAVES_KEY, SaveGroups.Worlds.ToString());
            
            foreach (WorldData worldData in worldsListHolder.Worlds)
                _worldsListModel.AddWorld(worldData);

            UpdateWorldsList();
        }

        public override void Dispose() =>
            _worldsListModel.OnWorldsListChanged -= UpdateWorldsList;

        private void UpdateWorldsList() =>
            View.UpdateList(_worldsListModel.GetWorlds());
    }
}