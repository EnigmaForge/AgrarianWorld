using System.Collections.Generic;
using Modules.ViewsModule;
using UnityEngine;

namespace Modules.GameMenu {
    public class WorldsListView : ViewBehaviour {
        [SerializeField] private Transform _content;
        [SerializeField] private WorldItemView _worldItemPrefab;
        
        public void UpdateList(List<WorldData> worlds) {
            ClearList();
            
            foreach (WorldData worldData in worlds) {
                WorldItemView worldItemView = Instantiate(_worldItemPrefab, _content);
                worldItemView.SetWorldName(worldData.WorldName);
                worldItemView.SetLastOpenDate(worldData.LastOpenDate);
            }
        }

        private void ClearList() {
            foreach (Transform child in _content)
                Destroy(child.gameObject);
        }
    }
}