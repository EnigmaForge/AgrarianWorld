using Zenject;

namespace Modules.Core.Bootstraps {
    public class InitialSceneBootstrap : BootstrapBehaviour {
        protected override void Bootstrap() {
            InitializeContext();
            LoadFirstScene();
        }
        
        private void InitializeContext() =>
            ProjectContext.Instance.EnsureIsInitialized();

        private void LoadFirstScene() {
            
        }
    }
}
