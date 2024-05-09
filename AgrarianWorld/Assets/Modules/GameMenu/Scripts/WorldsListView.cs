using Modules.ViewsModule;
using UnityEngine;

namespace Modules.GameMenu {
    public class WorldsListView : ViewBehaviour {
        [SerializeField] private Transform _content;
        [SerializeField] private GameObject _noCreatedWorldsText;

        public Transform GetItemsRoot() =>
            _content;

        public void ShowNoCreatedWorldsText(bool visibility) =>
            _noCreatedWorldsText.SetActive(visibility);
    }
}