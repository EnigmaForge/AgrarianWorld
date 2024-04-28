using UnityEngine;

namespace Modules.ViewsModule {
    [RequireComponent(typeof(Canvas))]
    public abstract class WindowBehaviour : MonoBehaviour {
        protected Canvas Canvas;

        private void Awake() =>
            InitializeComponents();

        private void InitializeComponents() =>
            Canvas = GetComponent<Canvas>();

        public void SetVisibility(bool visibility) =>
            Canvas.enabled = visibility;
    }
}