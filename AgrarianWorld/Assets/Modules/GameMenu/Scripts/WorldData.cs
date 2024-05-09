using System;

namespace Modules.GameMenu {
    [Serializable]
    public class WorldData {
        public string WorldName;
        public int Seed;
        public WorldType WorldType;
        public string LastOpenDate;
        
        public WorldData(string worldName, int seed, WorldType worldType, string lastOpenDate) {
            WorldName = worldName;
            Seed = seed;
            LastOpenDate = lastOpenDate;
            WorldType = worldType;
        }
    }
}