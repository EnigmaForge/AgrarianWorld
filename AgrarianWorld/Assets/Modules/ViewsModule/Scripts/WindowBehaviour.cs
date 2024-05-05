using UnityEngine;
using Zenject;

namespace Modules.ViewsModule {
    [RequireComponent(typeof(Canvas))]
    public abstract class WindowBehaviour : WindowBase {
        protected Canvas Canvas;
        protected PresentersContainer Container;
        private ViewBehaviour[] _views;
        private DiContainer _container;

        [Inject]
        private void InjectDependencies(DiContainer container) =>
            _container = container;

        private void Awake() {
            InitializeComponents();
            SetupPresenters();
        }

        private void InitializeComponents() {
            Canvas = GetComponent<Canvas>();
            ViewBehaviour[] views = GetViews();
            Container = new PresentersContainer(views, _container);
        }

        private ViewBehaviour[] GetViews() {
            WindowBehaviour[] windows = GetComponentsInChildren<WindowBehaviour>();
            foreach (WindowBehaviour window in windows)
                window.SetActive(false);
            
            ViewBehaviour[] views = GetComponentsInChildren<ViewBehaviour>();
            
            foreach (WindowBehaviour window in windows)
                window.SetActive(true);

            return views;
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