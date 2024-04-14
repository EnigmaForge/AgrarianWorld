using System;

namespace Modules.SavingSystem {
    [Serializable]
    public class SavingDataHolder {
        public string Key;
        public string Data;

        public SavingDataHolder(string key, string data) {
            Key = key;
            Data = data;
        }
    }
}