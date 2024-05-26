using Modules.ViewsModule;
using UnityEngine;

namespace Modules.GameMenu {
    public class WorldItemPresenter : Presenter<WorldItemView> {
        private readonly WorldsListModel _worldsListModel;
        private readonly WorldData _worldData;

        public WorldItemPresenter(WorldItemView view, WorldsListModel worldsListModel, WorldData worldData) {
            View = view;
            _worldsListModel = worldsListModel;
            _worldData = worldData;
        }

        public override void Initialize() {
            View.SetWorldName(_worldData.WorldName);
            View.SetLastOpenDate(_worldData.LastOpenDate);
            View.SetWorldImage(null);
            _worldsListModel.OnSelectedWorldChanged += ChangeSelection;
            View.OnClickOpenWorldButton += SelectWorld;
        }

        private void ChangeSelection(WorldData worldData) =>
            View.SetSelected(_worldData == worldData);

        public override void Dispose() {
            _worldsListModel.OnSelectedWorldChanged -= ChangeSelection;
            if (View == null)
                return;
            
            View.OnClickOpenWorldButton -= SelectWorld;
            if (View.gameObject != null)
                Object.Destroy(View.gameObject);
        }

        private void SelectWorld() =>
            _worldsListModel.SelectedWorld = _worldData;
    }
}