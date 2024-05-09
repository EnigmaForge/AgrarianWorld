using System.Collections.Generic;
using UnityEngine;

namespace Modules.GameMenu {
    public interface IWorldsListItemContainer {
        public void UpdateList(List<WorldData> worldsData, Transform itemsRoot);
    }
}