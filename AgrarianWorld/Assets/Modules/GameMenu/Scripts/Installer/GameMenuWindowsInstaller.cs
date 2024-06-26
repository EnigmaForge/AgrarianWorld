using Zenject;

namespace Modules.GameMenu.Installer {
    public class GameMenuWindowsInstaller : Installer<GameMenuWindowsInstaller> {
        public override void InstallBindings() {
            Container.BindInterfacesTo<WorldsListItemContainer>()
                     .AsSingle();
            
            Container.Bind<OpenWorldWindow>()
                     .FromComponentInHierarchy()
                     .AsSingle();

            Container.Bind<AddWorldWindow>()
                     .FromComponentInHierarchy()
                     .AsSingle();

            Container.Bind<GameMenuWindow>()
                     .FromComponentInHierarchy()
                     .AsSingle();
        }
    }
}