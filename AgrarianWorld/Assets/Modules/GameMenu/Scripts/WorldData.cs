namespace Modules.GameMenu {
    public class WorldData {
        public readonly string WorldName;
        public readonly string Seed;
        public readonly string LastOpenDate;
        
        public WorldData(string worldName, string seed, string lastOpenDate) {
            WorldName = worldName;
            Seed = seed;
            LastOpenDate = lastOpenDate;
        }
    }
}