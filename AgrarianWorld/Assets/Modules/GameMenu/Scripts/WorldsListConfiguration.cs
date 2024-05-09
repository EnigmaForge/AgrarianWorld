using UnityEngine;

namespace Modules.GameMenu {
    [CreateAssetMenu(fileName = nameof(WorldsListConfiguration) + "_Default", menuName = "Configurations/GameMenu/" + nameof(WorldsListConfiguration))]
    public class WorldsListConfiguration : ScriptableObject {
        [field: SerializeField] public WorldItemView WorldItemPrefab { get; private set; }
    }
}