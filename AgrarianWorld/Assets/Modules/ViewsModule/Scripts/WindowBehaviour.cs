using UnityEngine;

namespace Modules.ViewsModule {
    [RequireComponent(typeof(Canvas))]
    public abstract class WindowBehaviour : WindowBase {
        protected Canvas Canvas;
        protected PresentersContainer Container;
        private ViewBehaviour[] _views;

        private void Awake() {
            SetActive(true);
            InitializeComponents();
            SetupPresenters();
            SetActive(false);
        }

        private void InitializeComponents() {
            Canvas = GetComponent<Canvas>();
            ViewBehaviour[] views = GetComponentsInChildren<ViewBehaviour>();
            Container = new PresentersContainer(views);
        }

        private void OnDestroy() =>
            DisposePresenters();

        private void DisposePresenters() =>
            Container.Clean();

        protected override void SetupPresenters() { }

        public TPresenter GetPresenter<TPresenter>() where TPresenter : PresenterBase =>
            Container.GetPresenter<TPresenter>();

        public void SetActive(bool value) =>
            gameObject.SetActive(value);

        public void SetVisibility(bool visibility) =>
            Canvas.enabled = visibility;
    }
}