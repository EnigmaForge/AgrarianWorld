using Modules.GameMenu;
using Modules.GenerationSystem;
using Zenject;

namespace Modules.Core.Installers {
    public class DataInstaller : Installer<DataInstaller> {
        public override void InstallBindings() {
            Container.Bind<WorldsListModel>()
                     .AsSingle();

            Container.Bind<AddWindowInitialValuesModel>()
                     .AsSingle();
            
            Container.Bind<TerrainSurfacePointsModel>()
                     .AsSingle();
        }
    }
}