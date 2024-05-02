using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Modules.ViewsModule {
    public class PresentersContainer {
        private readonly List<PresenterBase> _presenters = new();
        private readonly ViewBehaviour[] _views;
        private readonly DiContainer _container;

        public PresentersContainer(ViewBehaviour[] views, DiContainer container) {
            _views = views;
            _container = container;
        }

        public void CreatePresenter<TPresenter, TView>() where TPresenter : PresenterBase where TView : ViewBehaviour {
            PresenterBase presenter = _container.Instantiate<TPresenter>();
            ViewBehaviour view = GetView<TView>();
            
            presenter!.SetView(view);
            presenter.Initialize();
            
            _presenters.Add(presenter);
        }

        public TPresenter GetPresenter<TPresenter>() where TPresenter : PresenterBase =>
            (TPresenter)_presenters.FirstOrDefault(presenter => presenter.GetType() == typeof(TPresenter));

        public void Clean() {
            foreach (PresenterBase presenter in _presenters)
                presenter.Dispose();
        }

        private TView GetView<TView>() where TView : ViewBehaviour =>
            (TView)_views.FirstOrDefault(view => view.GetType() == typeof(TView));
    }
}