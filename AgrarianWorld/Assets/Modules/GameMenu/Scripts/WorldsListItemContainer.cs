using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Modules.GameMenu {
    public class WorldsListItemContainer : IWorldsListItemContainer, IDisposable {
        private readonly List<WorldItemPresenter> _worldItems = new();
        private WorldsListModel _worldsListModel;
        private WorldsListConfiguration _worldsListConfiguration;
        private DiContainer _diContainer;

        [Inject]
        private void InjectDependencies(WorldsListModel worldsListModel, WorldsListConfiguration worldsListConfiguration, DiContainer diContainer) {
            _worldsListModel = worldsListModel;
            _worldsListConfiguration = worldsListConfiguration;
            _diContainer = diContainer;
        }

        public void UpdateList(List<WorldData> worldsData, Transform itemsRoot) {
            foreach (WorldItemPresenter worldItem in _worldItems.ToList()) {
                worldItem.Dispose();
                _worldItems.Remove(worldItem);
            }

            foreach (WorldData worldData in worldsData) {
                WorldItemView view = _diContainer.InstantiatePrefab(_worldsListConfiguration.WorldItemPrefab, itemsRoot).GetComponent<WorldItemView>();
                WorldItemPresenter presenter = new WorldItemPresenter(view, _worldsListModel, worldData);
                presenter.Initialize();
                _worldItems.Add(presenter);
            }
        }

        public void Dispose() {
            foreach (WorldItemPresenter worldItemPresenter in _worldItems)
                worldItemPresenter.Dispose();
        }
    }
}