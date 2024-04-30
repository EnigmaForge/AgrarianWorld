namespace Modules.ViewsModule {
    public abstract class PresenterBase {
        internal abstract void SetView(ViewBehaviour view);
        public abstract void Initialize();
        public abstract void Dispose();
    }
}