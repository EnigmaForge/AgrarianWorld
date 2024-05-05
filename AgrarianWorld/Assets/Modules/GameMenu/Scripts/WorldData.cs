namespace Modules.GameMenu {
    public class WorldData {
        public readonly string WorldName;
        public readonly string Seed;
        public readonly WorldType WorldType;
        public readonly string LastOpenDate;
        
        public WorldData(string worldName, string seed, WorldType worldType, string lastOpenDate) {
            WorldName = worldName;
            Seed = seed;
            LastOpenDate = lastOpenDate;
            WorldType = worldType;
        }
    }
}