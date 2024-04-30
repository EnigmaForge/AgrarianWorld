using Modules.GameMenu;
using Zenject;

namespace Modules.Core.Installers {
    public class UIWindowsInstaller : Installer<UIWindowsInstaller> {
        public override void InstallBindings() {
            Container.Bind<GameMenuWindow>()
                     .FromComponentInHierarchy()
                     .AsSingle();
        }
    }
}