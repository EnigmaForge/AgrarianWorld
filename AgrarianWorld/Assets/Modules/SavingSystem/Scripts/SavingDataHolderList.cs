using System;
using System.Collections.Generic;

namespace Modules.SavingSystem {
    [Serializable]
    public class SavingDataHolderList {
        public List<SavingDataHolder> Items = new();
    }
}