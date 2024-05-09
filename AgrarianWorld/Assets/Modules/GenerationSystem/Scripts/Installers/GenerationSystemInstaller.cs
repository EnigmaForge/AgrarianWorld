using Zenject;

namespace Modules.GenerationSystem.Installers {
    public class GenerationSystemInstaller : Installer<GenerationSystemInstaller> {
        public override void InstallBindings() {
            Container.Bind<ITerrainGenerator>()
                     .To<RealisticTerrainGenerator>()
                     .AsSingle();
        }
    }
}