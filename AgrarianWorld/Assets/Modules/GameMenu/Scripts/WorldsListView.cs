using System;
using System.Collections.Generic;
using Modules.ViewsModule;
using UnityEngine;

namespace Modules.GameMenu {
    public class WorldsListView : ViewBehaviour {
        [SerializeField] private Transform _content;
        [SerializeField] private WorldItemView _worldItemPrefab;
        private readonly List<WorldItemView> _worldItemViews = new();
        private int _lastSelected = -1;
        public event Action<int> OnClickItem;

        private void OnDestroy() =>
            CleanItems();

        public void UpdateList(List<WorldData> worlds) {
            ClearList();

            for (var index = 0; index < worlds.Count; index++) {
                WorldData worldData = worlds[index];
                WorldItemView worldItemView = Instantiate(_worldItemPrefab, _content);
                worldItemView.SetWorldName(worldData.WorldName);
                worldItemView.SetLastOpenDate(worldData.LastOpenDate);
                worldItemView.SetIndex(index);
                worldItemView.OnClickOpenWorldButton += OnClickItemInvoke;
                _worldItemViews.Add(worldItemView);
            }

            _lastSelected = -1;
        }

        private void OnClickItemInvoke(int index) {
            OnClickItem?.Invoke(index);
            
            if(_lastSelected != -1)
                _worldItemViews[_lastSelected].SetSelected(false);
            _worldItemViews[index].SetSelected(true);
            
            _lastSelected = index;
        }

        private void ClearList() {
            CleanItems();

            foreach (Transform child in _content)
                Destroy(child.gameObject);
        }

        private void CleanItems() {
            foreach (WorldItemView worldItemView in _worldItemViews)
                worldItemView.OnClickOpenWorldButton -= OnClickItemInvoke;
            
            _worldItemViews.Clear();
        }
    }
}