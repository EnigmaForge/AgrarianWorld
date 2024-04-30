using Modules.Core.FiniteStateMachine.GameStateMachine;
using Zenject;

namespace Modules.Core.Bootstraps {
    public class InitialSceneBootstrap : BootstrapBehaviour {
        protected override void OnInitialize() =>
            InitializeContext();

        private void Start() =>
            LoadGameMenu();

        private void InitializeContext() =>
            ProjectContext.Instance.EnsureIsInitialized();

        private void LoadGameMenu() {
            IGameStateMachine gameStateMachine = ProjectContext.Instance.Container.Resolve<IGameStateMachine>();
            gameStateMachine.ChangeState<MenuState>(); 
        }
    }
}
