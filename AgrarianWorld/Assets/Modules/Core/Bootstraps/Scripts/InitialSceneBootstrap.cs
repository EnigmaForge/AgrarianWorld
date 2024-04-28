using Modules.Core.FiniteStateMachine.GameStateMachine;
using UnityEngine;
using Zenject;

namespace Modules.Core.Bootstraps {
    public class InitialSceneBootstrap : MonoBehaviour {
        private void Start() {
            InitializeContext();
            LoadGameMenu();
        }

        private void InitializeContext() =>
            ProjectContext.Instance.EnsureIsInitialized();

        private void LoadGameMenu() {
            IGameStateMachine gameStateMachine = ProjectContext.Instance.Container.Resolve<IGameStateMachine>();
            gameStateMachine.ChangeState<MenuState>(); 
        }
    }
}
