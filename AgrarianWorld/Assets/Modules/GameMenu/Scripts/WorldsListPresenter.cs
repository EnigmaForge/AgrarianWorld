using Modules.Core.Global.Enums;
using Modules.SavingSystem;
using Modules.ViewsModule;

namespace Modules.GameMenu {
    public class WorldsListPresenter : Presenter<WorldsListView> {
        private const string WORLDS_LIST_SAVES_KEY = "WorldsList";
        private readonly WorldsListModel _worldsListModel;
        private readonly IDataStorageService _dataStorageService;
        private readonly IWorldsListItemContainer _worldsListItemContainer;

        public WorldsListPresenter(WorldsListModel worldsListModel, IDataStorageService dataStorageService, IWorldsListItemContainer worldsListItemContainer) {
            _worldsListModel = worldsListModel;
            _dataStorageService = dataStorageService;
            _worldsListItemContainer = worldsListItemContainer;
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

        private void UpdateWorldsList() {
            WorldsListHolder worldsListHolder = new WorldsListHolder() {
                Worlds = _worldsListModel.GetWorlds()
            };
            
            _dataStorageService.Save(WORLDS_LIST_SAVES_KEY, worldsListHolder, SaveGroups.Worlds.ToString());
            View.ShowNoCreatedWorldsText(worldsListHolder.Worlds.Count == 0);
            _worldsListItemContainer.UpdateList(worldsListHolder.Worlds, View.GetItemsRoot());
        }
    }
}