using Zenject;

namespace Modules.Core.FiniteStateMachine.GameStateMachine.Installers {
    public class GameStateMachineInstaller : Installer<GameStateMachineInstaller> {
        public override void InstallBindings() {
            Container.BindInterfacesAndSelfTo<MenuState>()
                     .AsSingle();
            
            Container.BindInterfacesAndSelfTo<GameState>()
                     .AsSingle();
            
            Container.BindInterfacesTo<GameStateMachine>()
                     .AsSingle()
                     .NonLazy();
        }
    }
}