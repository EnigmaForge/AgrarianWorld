using Modules.GameMenu;
using Zenject;

namespace Modules.Core.Installers {
    public class DataInstaller : Installer<DataInstaller> {
        public override void InstallBindings() {
            Container.Bind<WorldsListModel>()
                     .AsSingle();
        }
    }
}