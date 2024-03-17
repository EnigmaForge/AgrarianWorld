using System;
using UnityEngine;

namespace Modules.DebugMenu {
    public class DebugMenuManager : MonoBehaviour {
        private readonly string _pathToItems = "DebugMenuItems";
        [SerializeField] private GameObject _itemsContainer;

        private void Awake() =>
            InitializeItems();

        private void InitializeItems() {
            DebugMenuItemBehaviour[] debugMenuItems = Resources.LoadAll<DebugMenuItemBehaviour>(_pathToItems);

            Transform containerTransform = _itemsContainer.transform;
            foreach (DebugMenuItemBehaviour item in debugMenuItems)
                Instantiate(item, containerTransform);
        }
    }
}