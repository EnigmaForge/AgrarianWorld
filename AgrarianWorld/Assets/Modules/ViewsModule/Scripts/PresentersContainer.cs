using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace Modules.ViewsModule {
    public class PresentersContainer {
        private readonly List<PresenterBase> _presenters = new();
        private readonly ViewBehaviour[] _views;

        public PresentersContainer(ViewBehaviour[] views) =>
            _views = views;

        public void CreatePresenter<TPresenter, TView>() where TPresenter : PresenterBase where TView : ViewBehaviour {
            PresenterBase presenter = ProjectContext.Instance.Container.Instantiate<TPresenter>();
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