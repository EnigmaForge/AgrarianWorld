using System;
using System.Collections.Generic;
using System.Linq;

namespace Modules.GameMenu {
    public class WorldsListModel {
        private readonly WorldsListHolder _worldsListHolder = new();
        private WorldData _selectedWorld;

        public WorldData SelectedWorld {
            get =>
                _selectedWorld;
            set {
                _selectedWorld = value;
                OnSelectedWorldChanged?.Invoke(value);
            }
        }

        public event Action<WorldData> OnSelectedWorldChanged;
        public event Action OnWorldsListChanged;

        public void AddWorld(WorldData worldData) {
            if (_worldsListHolder.Worlds.Any(data => data.WorldName == worldData.WorldName))
                throw new Exception("World already exist in worlds list");
            
            _worldsListHolder.Worlds.Add(worldData);
            OnWorldsListChanged?.Invoke();
        }
        
        public void RemoveWorld(WorldData worldData) {
            if (_worldsListHolder.Worlds.Any(data => data.WorldName == worldData.WorldName) is false)
                throw new Exception($"Cannot find world with name: {worldData.WorldName}");

            SelectedWorld = null;
            _worldsListHolder.Worlds.Remove(worldData);
            OnWorldsListChanged?.Invoke();
        }
        
        public WorldData GetWorld(string worldName) =>
            _worldsListHolder.Worlds.FirstOrDefault(worldData => worldData.WorldName == worldName);

        public List<WorldData> GetWorlds() =>
            new(_worldsListHolder.Worlds);
    }
}