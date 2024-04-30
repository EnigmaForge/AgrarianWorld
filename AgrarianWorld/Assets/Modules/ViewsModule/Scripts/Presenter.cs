using System;

namespace Modules.ViewsModule {
    public abstract class Presenter<TView> : PresenterBase, IDisposable where TView : ViewBehaviour {
        protected TView View;

        internal override void SetView(ViewBehaviour view) =>
            View = view as TView;

        public override void Initialize() { }
        public override void Dispose() { }
    }
}