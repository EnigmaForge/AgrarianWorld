using UnityEngine;
namespace Modules.DebugMenu {
    public class DebugMenuCanvas : MonoBehaviour {
        private readonly string _pathToItems = "DebugMenuItems";
        [SerializeField] private GameObject _itemsContainer;

        private void Start() =>
            InitializeItems();

        private void InitializeItems() {
            DebugMenuItemBehaviour[] debugMenuItems = Resources.LoadAll<DebugMenuItemBehaviour>(_pathToItems);

            Transform containerTransform = _itemsContainer.transform;
            foreach (DebugMenuItemBehaviour item in debugMenuItems)
                Instantiate(item, containerTransform);
        }
    }
}